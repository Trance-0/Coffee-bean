using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginWindowsManager : MonoBehaviour
{
    public GameObject _loginWindow;
    public GameObject _registerWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void openRegister() {
        _registerWindow.SetActive(true);
        _loginWindow.SetActive(false);
    }
    public void openLogin()
    {
        _registerWindow.SetActive(false);
        _loginWindow.SetActive(true);
    }
}
