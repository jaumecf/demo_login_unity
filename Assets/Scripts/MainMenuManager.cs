using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject loginPanel;
    public GameObject registerPanel;
    void Start()
    {
        mainMenuPanel.SetActive(true);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void enableLogin()
    {
        loginPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        registerPanel.SetActive(false);
    }

    public void enableRegister()
    {
        registerPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        loginPanel.SetActive(false);
    }

    public void backToMainMenu()
    {
        mainMenuPanel.SetActive(true);
        loginPanel.SetActive(false);
        registerPanel.SetActive(false);
    }
}
