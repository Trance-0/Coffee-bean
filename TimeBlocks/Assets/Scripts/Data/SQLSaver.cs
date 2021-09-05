using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQLSaver : MonoBehaviour
{
    public string userID;

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

    public void SignUp(string text, string v, string email)
    {
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("INSERT INTO tb_user (name,email,password) VALUES ('" + text + "', '" + email + "', '" + v + "')", mySqlConnection);
        cmd.ExecuteNonQuery();
        Debug.Log("Success");
    }

    public bool LogIn(string userName, string v)
    {
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("select password from tb_user where name = '" + userName + "'", mySqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            if (reader[0].ToString().CompareTo(v) == 0)
            {
                userID = GetID(userName);
                return true;
            }
        }
        return false;
    }

    private string GetID(string userName)
    {

        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("select ID from tb_user where name = '" + userName + "'", mySqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
                return reader[0].ToString();
        }
        return "";
    }

    public bool CheckUserNameRepeated(string userName)
    {
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
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

    internal void SaveBlocks(List<TimeBlock> blocks)
    {
        String command = "";
        foreach (TimeBlock a in blocks) {
            if (a._taskId == -1) {
                Debug.Log("Adding Task to Server"+a._name);
                command += "INSERT INTO tb_task (name,deadline,user_id,tag_id,estimate_time) VALUES ('" + a._name + "', FROM_UNIXTIME(" + a._deadline+ "), '"  + userID+ "', '" + a._tagId + "', '" + a._estimateTime+ "')";
            }
        }
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand(command, mySqlConnection);
        cmd.ExecuteNonQuery();
        Debug.Log("Success");
    }

    internal void RemoveBlock(TimeBlock block)
    {
        if (block._taskId != -1)
        {
            mySqlConnection = new MySqlConnection(sql);
            mySqlConnection.Open();
            Debug.Log("Conecting to SQL Server");
            MySqlCommand cmd = new MySqlCommand("DELETE FROM table_name WHERE ID = " + block._taskId.ToString(), mySqlConnection);
            cmd.ExecuteNonQuery();
            Debug.Log("Success");
        }
    }

    internal List<TimeBlock> LoadBlocks()
    {
        List<TimeBlock> blocks = new List<TimeBlock>();
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("select name,deadline,tag_id,estimate_time,ID from tb_task where user_id= '" + userID+ "'", mySqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            TimeBlock a = new TimeBlock();
            a._name = reader[0].ToString();
            Debug.Log(reader[1].ToString());
            a._deadline = long.Parse(reader[1].ToString());
            a._tagId = int.Parse(reader[2].ToString());
            a._estimateTime = int.Parse(reader[3].ToString());
            a._taskId = int.Parse(reader[4].ToString());
            blocks.Add(a);
            Debug.Log("Pulling Task from Server" + a._name);
        }
        return blocks;
    }

    internal void SaveSettings(bool enableTimer, bool analyseOCT, int manualOCT, bool oCTAuto)
    {
        throw new NotImplementedException();
    }

    internal void SaveOCT(List<double> oCT)
    {
        throw new NotImplementedException();
    }

    internal void SaveStats(double oCTSum, int taskSum, int interruptSum, double oCTMax, int appUseSum, long joinTime)
    {
        throw new NotImplementedException();
    }

    internal void SaveTags(Dictionary<int, Tag> tagDictionary)
    {
        throw new NotImplementedException();
    }
}
