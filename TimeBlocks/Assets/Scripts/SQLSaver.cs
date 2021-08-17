using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQLSaver : MonoBehaviour
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
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
    }
    void Save() {
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("数据库已连接");
        MySqlCommand cmd = new MySqlCommand("select * from tb_user_info", mySqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Debug.Log(reader[i].ToString());
            }
        }
        Debug.Log(reader);
        mySqlConnection.Close();
        Debug.Log("数据库关闭");
    }
    public void SignUp(string text, string v, string email)
    {
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("数据库已连接");
        MySqlCommand cmd = new MySqlCommand("INSERT INTO tbuser (name,email,password) VALUES ('" + text + "', '" + email + "', '" + v + "')", mySqlConnection);
        cmd.ExecuteNonQuery();
        Debug.Log("Success");
    }
    public bool PasswordCheck(string userName, string v)
    {
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
    public bool CheckUserNameRepeated(string userName)
    {
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("数据库已连接");
        MySqlCommand cmd = new MySqlCommand("select name from tbuser", mySqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
            {
                if (reader[i].ToString().CompareTo(userName) == 0)
                    return false;
            }
        }
        return true;
    }
}
