using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField _userName;
    public InputField _password;
    

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
            if (getPassword(_userName.text,SHA256Hash(_password.text))) {
            }
        }catch (Exception e) {
            Debug.Log(e);
        }
    }
    public bool getPassword(string userName, string v) {
        return false;
    }
}
