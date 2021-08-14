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
    public static MySqlConnection mySqlConnection;
    //数据库名称
    public static string database = "timeblocks";
    //数据库IP
    private static string host = "45.77.71.189";
    //用户名
    private static string username = "TimeBlocks";
    //用户密码
    private static string password = "ih54J2K28PwrGfEx";

    public static string sql = string.Format("database={0};server={1};user={2};password={3};port={4}",
    database, host, username, password, "3306");
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
            if (getPassword(_userName.text, SHA256Hash(_password.text)))
            {
                Debug.Log("Yeah");
            }
            else {
                Debug.Log("No~");

            }
        }catch (Exception e) {
            Debug.Log(e);
        }
    }
    public bool getPassword(string userName, string v) {
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("数据库已连接");
        MySqlCommand cmd = new MySqlCommand("select password from tb_user where name = '" + userName + "'", mySqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            //for (int i = 0; i < reader.FieldCount; i++)
            //{
                if (reader[0].ToString().CompareTo(v) == 0)
                    return true;
            //}
        }
        return false;
    }
}
