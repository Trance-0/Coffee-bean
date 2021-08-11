using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public InputField _userName;
    public InputField _email;
    public InputField _verify;
    public InputField _password1;
    public InputField _password2;
    public GameObject _send;
    public GameObject _sendText;
    public GameObject _emailSender;
    private string _finalmail;
    private string _code;
    private bool _activated;
    public float cd;
    public static string[] variable = { "1","2","3","4","5","6","7","8","9","0","R","C"};
    // Start is called before the first frame update
    void Start()
    {
        _activated = true;
    }
    public void SendCode() {
        try{
            _code = "";
            cd = 10f;
            _activated = false;
            _send.GetComponent<Button>().interactable = false ;
            System.Random r = new System.Random();
            for (int i=0;i<6;i++) {
                _code += variable[r.Next(variable.Length)];
            }
             _finalmail=_email.text;
            Debug.Log("Your verification code for TimeBlocks is " + _code + " ."+ "TimeBlocks Verification code");
           _emailSender.GetComponent<EmailSender>().SendMail(_finalmail,"Your verification code for TimeBlocks is "+_code +" .","TimeBlocks Verification code");
        }
        catch (System.Exception e) {
            Debug.Log(e);
        }
    }
    public void Verify() {
        try
        {
            //add item to data base
            //add item to data base
            //add item to data base
            //add item to data base
            //add item to data base
            Debug.Log(_userName.text + "" + _email.text + "" + _verify.text + "" + _password1.text + "" + SHA256Hash(_password1.text));
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }
    }
    public static string SHA256Hash(string input)
    {
        byte[] InputBytes = Encoding.Default.GetBytes(input);
        SHA256Managed sha256 = new SHA256Managed();
        byte[] OutputBytes = sha256.ComputeHash(InputBytes);
        return System.Convert.ToBase64String(OutputBytes);
    }
    // Update is called once per frame
    void Update()
    {
        if (cd>0) {
            cd -= Time.deltaTime;
            _sendText.GetComponent<Text>().text = cd.ToString("f0");
        }
        if (!_activated&cd<=0) {
            _send.GetComponent<Button>().interactable = true ;
            _sendText.GetComponent<Text>().text = "Send";
            _activated = true;
        }
    }
}
