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
    public GameObject _emailSender;
    public string _finalmail;
    public string _code;
    public static string[] variable = { };
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void SendCode() {
        try{
            System.Random r = new System.Random();
            for (int i=0;i<6;i++) {
                _code += variable[r.Next(variable.Length)];
            }
            _email.text = _finalmail;
           _emailSender.GetComponent<EmailSender>().SendMail(_finalmail,"Your verification code for TimeBlocks is "+_code +" .","TimeBlocks Verification code");
        }
        catch (System.Exception e) {
            Debug.Log(e);
        }
    }
    public void Verify() {
        if (_password1==_password2&&_code==_verify.text&& _email.text == _finalmail&&_userName.text!=null) {
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
        
    }
}
