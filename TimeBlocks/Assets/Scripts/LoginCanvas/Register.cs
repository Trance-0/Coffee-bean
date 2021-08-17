using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public InputField userName;
    public InputField email;
    public InputField verify;
    public InputField password1;
    public InputField password2;
    public GameObject send;
    public GameObject sendText;
    private string finalmail;
    private string code;
    private bool activated;
    public float cd;
    public static string[] variable = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "R", "C" };

    public SQLSaver sqlSaver;
    public EmailSender emailSender;
    public ErrorWindow errorWindow;
    // Start is called before the first frame update
    void Start()
    {
        activated = true;
    }
    public void SendCode() {
        try {
            code = "";
            cd = 10f;
            activated = false;
            send.GetComponent<Button>().interactable = false;
            System.Random r = new System.Random();
            for (int i = 0; i < 6; i++) {
                code += variable[r.Next(variable.Length)];
            }
            finalmail = email.text;
            Debug.Log("Your verification code for TimeBlocks is " + code + " ." + "TimeBlocks Verification code");
            emailSender.SendMail(finalmail, "Your verification code for TimeBlocks is " + code + " .", "TimeBlocks Verification code");
        }
        catch (System.Exception e) {
            Debug.Log(e);
        }
    }
    public void Signup() {
        try
        {
            Debug.Log(userName.text + "" + email.text + "" + verify.text + "" + password1.text + "" + SHA256Hash(password1.text));
            if (userName.text != null && email.text != null && finalmail == email.text && verify.text == code && password1.text == password2.text) {
                if (sqlSaver.CheckUserNameRepeated(userName.text)) {
                    sqlSaver.SignUp(userName.text, SHA256Hash(password1.text),email.text);
                }
                else {
                   errorWindow.Warning("User name was being used");
                }
            }
            else {
                errorWindow.Warning("InputField is not satisfied.");
            }
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
        if (cd > 0) {
            cd -= Time.deltaTime;
            sendText.GetComponent<Text>().text = cd.ToString("f0");
        }
        if (!activated & cd <= 0) {
            send.GetComponent<Button>().interactable = true;
            sendText.GetComponent<Text>().text = "Send";
            activated = true;
        }
    }
  
}
