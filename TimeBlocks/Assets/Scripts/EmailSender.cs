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
		mailClient.Credentials = new NetworkCredential("2795654849@qq.com", "密码")
		//test@qq.com发件人地址、test@tom.com收件人地址、这个邮箱要开启SMTP服务，具体时间要到2021.7.10.空邮箱，不要用来干别的事情
		MailMessage message = new MailMessage(new MailAddress("2795654849@qq.com"), new MailAddress(reciever));

		// message.Bcc.Add(new MailAddress("tst@qq.com")); //可以添加多个收件人
		message.Body = body;//邮件内容
		message.Subject = subject;//邮件主题
										   //Attachment 附件
		Console.WriteLine("Start Send Mail....");
		//发送....
		mailClient.Send(message);

		Console.WriteLine("Send Mail Successed");

		Console.ReadLine();
	}
}
