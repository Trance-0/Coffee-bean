using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQLSample : MonoBehaviour
{
    public static MySqlConnection mySqlConnection;
    //数据库名称
    public static string database = "mysql";
    //数据库IP
    private static string host = "45.77.71.189";
    //用户名
    private static string username = "TimeBlock";
    //用户密码
    private static string password = "iwnP4thRr57kLCSW";

    public static string sql = string.Format("database={0};server={1};user={2};password={3};port={4}",
    database, host, username, password, "3306");
    // Start is called before the first frame update
    void Start()
    {
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("数据库已连接");
        MySqlCommand cmd = new MySqlCommand("select * from user_info", mySqlConnection);
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
