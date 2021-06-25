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
	void SendMail(string reciever,string body, string subject) 
	{
		SmtpClient mailClient = new SmtpClient("smtp.qq.com");
		mailClient.EnableSsl = true;
		//Credentials登陆SMTP服务器的身份验证.
		mailClient.Credentials = new NetworkCredential("2795654849@qq.com", "密码");
		//test@qq.com发件人地址、test@tom.com收件人地址
		MailMessage message = new MailMessage(new MailAddress("1213250243@qq.com"), new MailAddress("aladdingame@qq.com"));

		// message.Bcc.Add(new MailAddress("tst@qq.com")); //可以添加多个收件人
		message.Body = "Hello Word!";//邮件内容
		message.Subject = "this is a test";//邮件主题
										   //Attachment 附件
		Console.WriteLine("Start Send Mail....");
		//发送....
		mailClient.Send(message);

		Console.WriteLine("Send Mail Successed");

		Console.ReadLine();
	}
}
