using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ClassificationManager : MonoBehaviour
{
    public NetworkingDataScriptableObject loginDataSO;
    [SerializeField] private Button classificationButton;
    public GameObject mainMenuManager;
    public GameObject listTile;

    public GameObject leaderboardPanel;
    // Start is called before the first frame update
    void Start()
    {
        classificationButton.onClick.AddListener(() => OnGetButtonClicked());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnGetButtonClicked()
    {
        StartCoroutine(GetClassification());
    }

    private IEnumerator GetClassification()
    {
        UnityWebRequest httpRequest = UnityWebRequest.Get(loginDataSO.apiUrl + "/LeaderboardL1/GetClassificationLevel1");
        //www.SetRequestHeader("Content-Type", "application/json");
        httpRequest.SetRequestHeader("Accept", "application/json");
        httpRequest.SetRequestHeader("Authorization", "bearer " + loginDataSO.token);

        yield return httpRequest.SendWebRequest();

        if (httpRequest.result != UnityWebRequest.Result.Success)
        {
            throw new Exception(httpRequest.error);
        }
        
        //Debug.Log(httpRequest.downloadHandler.text);

        var classification = JsonConvert.DeserializeObject<List<GameLevel1Dto>>(httpRequest.downloadHandler.text);
        Debug.Log(classification[0].ToString());

        foreach (var gameL1Data in classification)
        {
            GameObject newLine = Instantiate(listTile, leaderboardPanel.transform);
            newLine.GetComponent<TextMeshProUGUI>().text = gameL1Data.nomUusuari + "\t \t \t" + gameL1Data.segons;
            Debug.Log(gameL1Data.nomUusuari + " " + gameL1Data.segons + "\n");
        }
    }
}
