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
        MySqlCommand cmd = new MySqlCommand("select name from tb_user", mySqlConnection);
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
                Debug.Log("Adding Task to Server" + a._name);
                command += "INSERT INTO tb_task (name,deadline,user_id,tag_id,estimate_time) VALUES ('" + a._name + "', FROM_UNIXTIME(" + a._deadline + "), '" + userID + "', '" + a._tagId + "', '" + a._estimateTime + "'); ";
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
            MySqlCommand cmd = new MySqlCommand("DELETE FROM tb_task WHERE ID = " + block._taskId.ToString(), mySqlConnection);
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
        MySqlCommand cmd = new MySqlCommand("select name,deadline,tag_id,estimate_time,ID from tb_task where user_id= '" + userID + "'", mySqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            TimeBlock a = new TimeBlock();
            a._name = reader[0].ToString();
            Debug.Log(reader[1].ToString());
            TimeSpan st = Convert.ToDateTime(reader[1].ToString()) - new DateTime(1970, 1, 1, 0, 0, 0);
            a._deadline = Convert.ToInt64(st.TotalSeconds);
            Debug.Log(a._deadline);
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
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("UPDATE tb_user SET enable_timer = " + enableTimer + ", enable_analyze = " + analyseOCT + ", manual_OCT = " + manualOCT + ", OCT_Auto = " + oCTAuto + " WHERE ID = " + userID.ToString() + ";", mySqlConnection);
        cmd.ExecuteNonQuery();
        Debug.Log("Success");

    }

    internal void LoadSettings(out bool enableTimer, out bool analyseOCT, out int manualOCT, out bool oCTAuto) {
        enableTimer = false;
        analyseOCT = false;
        manualOCT = -1;
        oCTAuto = false;
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("select enable_timer, enable_analyze, manual_OCT, OCT_Auto from tb_user WHERE ID = " + userID.ToString() + ";", mySqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
                enableTimer = bool.Parse(reader[0].ToString());
                analyseOCT = bool.Parse(reader[1].ToString());
                manualOCT = int.Parse(reader[2].ToString());
                oCTAuto = bool.Parse(reader[3].ToString());
        }
    }

    internal void SaveOCT(List<double> oCT)
    {
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("UPDATE tb_user SET OCT_1 = " + oCT[0].ToString() + ", OCT_2 = " + oCT[1].ToString() + ", OCT_3 = " + oCT[2].ToString() + ", OCT_4 = " + oCT[3].ToString() + ", OCT_5 = " + oCT[4].ToString() + ", OCT_6 = " + oCT[5].ToString() + ", OCT_7 = " + oCT[6].ToString() + " WHERE ID = " + userID.ToString() + ";", mySqlConnection);
        cmd.ExecuteNonQuery();
        Debug.Log("Success");

    }
    internal List<double> LoadOCT(){
        List<double> OCT = new List<double>();
 mySqlConnection = new MySqlConnection(sql);
    mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("select OCT_1, OCT_2, OCT_3, OCT_4, OCT_5, OCT_6, OCT_7 from tb_user WHERE ID = " + userID.ToString() + ";", mySqlConnection);
    MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            for (int i = 0; i < reader.FieldCount; i++)
                OCT[i] = Double.Parse(reader[i].ToString());
            {
            }
        }
        return OCT;
        }

    internal void SaveStats(double oCTSum, int taskSum, int interruptSum, double oCTMax, int appUseSum, long joinTime)
    {
        throw new NotImplementedException();
    }
    internal void LoadStats(out double oCTSum,out int taskSum, out int interruptSum, out double oCTMax, out int appUseSum, out long joinTime)
    {
        oCTSum = 0;
        taskSum = 0;
        interruptSum = 0;
        oCTMax = 0;
        appUseSum = 0;
        joinTime = 0;

    }

    internal void SaveTags(Dictionary<int, Tag> tagDictionary)
    {
        throw new NotImplementedException();
    }
    internal Dictionary<int, Tag> LoadTags()
    {
        throw new NotImplementedException();
    }
}
