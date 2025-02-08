using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class LoginManager : MonoBehaviour
{
    EventSystem eventSystem;
    public NetworkingDataScriptableObject loginDataSO;
    public Selectable firstInput;
    public Button loginButton, backButton;
    public TMP_InputField emailInput, passwordInput;
    public TMP_Text errorText;

    private User usuari;
    void Start()
    {
        eventSystem = EventSystem.current;
        firstInput.Select();
    }

    // Update is called once per frame
    void Update()
    {
        Selectable selected = eventSystem.currentSelectedGameObject.GetComponent<Selectable>();
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            selected = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (selected != null)
            {
                selected.Select();
                //Debug.Log(selected.name);
            }
        }else if (Input.GetKeyDown(KeyCode.Tab))
        {
            selected = eventSystem.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (selected != null)
            {
                selected.Select();
                //Debug.Log(selected.name);
            }
        }else if (Input.GetKeyDown(KeyCode.Return))
        {
            if (selected.name.Equals("Back Button"))
            {
                backButton.onClick.Invoke();
            }else if (selected.name.Equals("Login Button"))
            {
                loginButton.onClick.Invoke();
            }
        }
    }

    public void login()
    {
        Debug.Log("Login...");
        StartCoroutine(TryLogin());
    }
    private IEnumerator TryLogin()
    {
        if (usuari == null)
        {
            UnityWebRequest httpClient = new UnityWebRequest(loginDataSO.apiUrl + "/Auth/Login", UnityWebRequest.kHttpVerbPOST);

            RegisterUserDTO loginDataUsuari = new RegisterUserDTO();
            //loginDataUsuari.Nom = "jaumecf"; // IMPORTANT! Can NOT be null!
            loginDataUsuari.Email = emailInput.text;
            loginDataUsuari.Password = passwordInput.text;
            
            string jsonData = JsonConvert.SerializeObject(loginDataUsuari);
            byte[] dataToSend = Encoding.UTF8.GetBytes(jsonData);
            
            httpClient.uploadHandler = new UploadHandlerRaw(dataToSend);
            httpClient.downloadHandler = new DownloadHandlerBuffer();
    
            httpClient.SetRequestHeader("Content-Type", "application/json");
            httpClient.SetRequestHeader("Accept", "*/*");
    
            yield return httpClient.SendWebRequest();
    
            if (httpClient.result == UnityWebRequest.Result.ConnectionError || httpClient.result == UnityWebRequest.Result.ProtocolError)
            {
                throw new Exception("Login: " + httpClient.error);
            }
    
            string jsonResponse = httpClient.downloadHandler.text;
            
            //authTokenDto = JsonUtility.FromJson<AuthTokenDto>(jsonResponse);
            AuthTokenDto authTokenDto = JsonConvert.DeserializeObject<AuthTokenDto>(jsonResponse);
            loginDataSO.token = authTokenDto.token;
            Debug.Log(authTokenDto.token);
            httpClient.Dispose();
        }
        
    }
    
}
