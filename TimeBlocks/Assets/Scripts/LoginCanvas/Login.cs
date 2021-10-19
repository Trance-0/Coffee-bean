using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField userName;
    public InputField password;
    public ErrorWindow errorWindow;
    public CanvasManager canvasManager;
    public DataManager dataManager;

    public SQLSaver sqlSaver;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static string SHA256Hash(string input)
    {
        byte[] InputBytes = Encoding.Default.GetBytes(input);
        SHA256Managed sha256 = new SHA256Managed();
        byte[] OutputBytes = sha256.ComputeHash(InputBytes);
        return System.Convert.ToBase64String(OutputBytes);
    }
    public void checkPassword() {
        try{
            //access data base to verify
            if (sqlSaver.Login(userName.text, SHA256Hash(password.text)))
            {
                dataManager.InitializeData();
                //ensure that the user will not login without data initialized.
                canvasManager.ChangeCanvas(0);
            }
            else {
                errorWindow.Warning("Password or username is not correct.");
            }
        }catch (Exception e) {
            Debug.Log(e);
        }
    }
   
}
