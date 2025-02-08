using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginManager : MonoBehaviour
{
    EventSystem eventSystem;
    public Selectable firstInput;
    public Button loginButton, backButton;
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
    }
}
