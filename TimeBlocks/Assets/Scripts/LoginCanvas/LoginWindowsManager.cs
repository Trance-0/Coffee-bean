using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginWindowsManager : MonoBehaviour
{
    public GameObject loginWindow;
    public GameObject registerWindow;
    // Start is called before the first frame update
    void Start()
    {
        registerWindow.SetActive(false);
        loginWindow.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openRegister() {
        registerWindow.SetActive(true);
        loginWindow.SetActive(false);
    }
    public void openLogin()
    {
        registerWindow.SetActive(false);
        loginWindow.SetActive(true);
    }
}
