using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;

public class EmailSender : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void SendMail(string reciever,string body, string subject) 
	{
		SmtpClient mailClient = new SmtpClient("smtp.qq.com");
		mailClient.EnableSsl = true;

        mailClient.Credentials = new NetworkCredential("2795654849@qq.com", "slkvcijmcjrkdgae") as ICredentialsByHost;
       
        MailMessage message = new MailMessage(new MailAddress("2795654849@qq.com"), new MailAddress(reciever));

		message.Body = body;
		message.Subject = subject;
		Debug.Log("Start Send Mail....");
		mailClient.Send(message);
        Debug.Log("Send Mail Successed");
	}
}
