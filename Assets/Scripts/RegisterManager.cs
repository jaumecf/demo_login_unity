using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Newtonsoft.Json;
using TMPro;
using UnityEngine.Networking;


public class RegisterManager : MonoBehaviour
{
    EventSystem eventSystem;
    public NetworkingDataScriptableObject loginDataSO;
    public GameObject mainMenuManager;
    public Selectable firstInput;
    public Button registerButton, backButton;
    public TMP_InputField nomInput, emailInput, passwordInput;
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
                registerButton.onClick.Invoke();
            }
        }
    }

    public void register()
    {
        Debug.Log("Register...");
        UnityWebRequest httpRequest = new UnityWebRequest();
        httpRequest.method = UnityWebRequest.kHttpVerbPOST;
        httpRequest.url = loginDataSO.apiUrl + "/Auth/Register";
        httpRequest.SetRequestHeader("Content-Type", "application/json");
        httpRequest.SetRequestHeader("Accept", "application/json");

        RegisterUserDTO registerUserDto = new RegisterUserDTO();
        registerUserDto.Nom = nomInput.text;
        registerUserDto.Email = emailInput.text;
        registerUserDto.Password = passwordInput.text;

        string jsonData = JsonConvert.SerializeObject(registerUserDto);
        byte[] dataToSend = Encoding.UTF8.GetBytes(jsonData);
        httpRequest.uploadHandler = new UploadHandlerRaw(dataToSend);

        httpRequest.downloadHandler = new DownloadHandlerBuffer();
        
        httpRequest.SendWebRequest();

        while (!httpRequest.isDone)
        {
            ;
        }

        if (httpRequest.result == UnityWebRequest.Result.ConnectionError ||
            httpRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error: " + httpRequest.error);
            return;
        }
        
        Debug.Log(httpRequest.result.ToString());
        
        string jsonResponse = httpRequest.downloadHandler.text;

        UserDTO registeredUser = JsonConvert.DeserializeObject<UserDTO>(jsonResponse);
        
        Debug.Log("Creat usuari: " + registeredUser.Id + " " + registeredUser.Nom 
                  + " " + registeredUser.Email);
        mainMenuManager.GetComponent<MainMenuManager>().enableClassification();
    }
}