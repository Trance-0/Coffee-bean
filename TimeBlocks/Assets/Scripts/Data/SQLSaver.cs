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
        LoadBlocks();
    }

    internal void SaveBlock(TimeBlock block)
    {
                Debug.Log("Adding Task to Server" + block._name);
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("INSERT INTO tb_task (name,deadline,user_id,tag_id,estimate_time) VALUES ('" + block._name + "', FROM_UNIXTIME(" + block._deadline + "), '" + userID + "', '" + block._tagId + "', '" + block._estimateTime + "'); ", mySqlConnection);
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

    internal void SaveSettings(bool enableTimer, bool analyseOCT, int manualOCT, bool oCTAuto,int defaultTagId)
    {
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("UPDATE tb_user SET enable_timer = " + enableTimer + ", enable_analyze = " + analyseOCT + ", manual_OCT = " + manualOCT + ", OCT_Auto = " + oCTAuto + ", default_tag_ID = " + defaultTagId + " WHERE ID = " + userID.ToString() + ";", mySqlConnection);
        cmd.ExecuteNonQuery();
        Debug.Log("Success");

    }

    internal void LoadSettings(out bool enableTimer, out bool analyseOCT, out int manualOCT, out bool oCTAuto,out int defaultTagId) {
        enableTimer = false;
        analyseOCT = false;
        manualOCT = -1;
        oCTAuto = false;
        defaultTagId = 0;
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("select enable_timer, enable_analyze, manual_OCT, OCT_Auto,default_tag_ID from tb_user WHERE ID = " + userID.ToString() + ";", mySqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Debug.Log("reader[0]" + reader[0].ToString());
            enableTimer = Convert.ToBoolean(int.Parse(reader[0].ToString()));
            analyseOCT = Convert.ToBoolean(int.Parse(reader[1].ToString())); 
                manualOCT = int.Parse(reader[2].ToString());
                oCTAuto = Convert.ToBoolean(int.Parse(reader[3].ToString()));
            defaultTagId= int.Parse(reader[4].ToString());
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
            for (int i = 0; i < 7; i++)
                OCT.Add(Double.Parse(reader[i].ToString()));
            {
            }
        }
        return OCT;
        }

    internal void SaveStats(double oCTSum, int taskSum, int interruptSum, double oCTMax, int appUseSum, long joinTime)
    {
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("UPDATE tb_user SET OCT_sum = " + oCTSum + ", task_sum = " + taskSum + ",interrupt_sum = " + interruptSum + ", OCT_max = " + oCTMax + ", app_use_sum = " + appUseSum + ", join_time =   FROM_UNIXTIME(" +joinTime + ")" + ", last_login_time =   FROM_UNIXTIME(" + GetTimestamp() + ") WHERE ID = " + userID.ToString() + ";", mySqlConnection);
        cmd.ExecuteNonQuery();
        Debug.Log("Success");
    }
    internal void LoadStats(out double oCTSum,out int taskSum, out int interruptSum, out double oCTMax, out int appUseSum, out long joinTime)
    {
        oCTSum = 0;
        taskSum = 0;
        interruptSum = 0;
        oCTMax = 0;
        appUseSum = 0;
        joinTime = 0;
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("select OCT_sum ,task_sum ,interrupt_sum ,OCT_max ,app_use_sum ,join_time from tb_user WHERE ID = " + userID.ToString() + ";", mySqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            oCTSum = double.Parse(reader[0].ToString());
            taskSum = int.Parse(reader[1].ToString());
            interruptSum = int.Parse(reader[2].ToString());
            oCTMax = double.Parse(reader[3].ToString());
            appUseSum = int.Parse(reader[4].ToString());
            TimeSpan st = Convert.ToDateTime(reader[5].ToString()) - new DateTime(1970, 1, 1, 0, 0, 0);
            joinTime = Convert.ToInt64(st.TotalSeconds);
        }
    }

    internal void SaveTags(Dictionary<int, Tag> tagDictionary)
    {
        String command = "";
        foreach (KeyValuePair<int, Tag> i in tagDictionary)
        {
            Tag a = i.Value;
            if (a._tagId < 0)
            {
                command += "INSERT INTO tb_tag (name,user_id,image_id,power) VALUES ('" + a._name + "'," + userID + "," + a._imageId + "," + a._power + ");";
            }
            else
            {
                command += "UPDATE tb_tag SET name = " + a._name + ", power = " + a._power + ",image_id = " + a._imageId + " WHERE ID = " + a._tagId + ";";
            }
        }
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand(command, mySqlConnection);
        cmd.ExecuteNonQuery();
        Debug.Log("Success");
    }
    internal Dictionary<int, Tag> LoadTags()
    {
        Dictionary<int, Tag> tags = new Dictionary<int, Tag>();
        mySqlConnection = new MySqlConnection(sql);
        mySqlConnection.Open();
        Debug.Log("Conecting to SQL Server");
        MySqlCommand cmd = new MySqlCommand("select name,image_id,power,ID from tb_tag where user_id= '" + userID + "'", mySqlConnection);
        MySqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            String name = reader[0].ToString();
            int imageId = int.Parse(reader[1].ToString());
            int power = int.Parse(reader[2].ToString());
            int tagId = int.Parse(reader[3].ToString());
            Tag a=new Tag(name, imageId, power);
            a._tagId = tagId;
            tags.Add(tagId,a);
            Debug.Log("Pulling Tag from Server" + a._name);
        }
        if (tags.Count<7) {
            int count = 1;
            while (tags.Count<7) {
                Tag a = new Tag("New tag "+count.ToString(), 0, 1);
                a._tagId = -count;
                tags.Add(-count,a);
                count++;
            }
            SaveTags(tags);
            return null;
        }
        return tags;
    }
    public long GetTimestamp()
    {
        //Debug.Log(year+" "+month + " " +day + " " +chunk*6+5);
        TimeSpan st = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
        return Convert.ToInt64(st.TotalSeconds);
    }
}
