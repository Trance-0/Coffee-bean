using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SQLSaver : MonoBehaviour
{

    public static MySqlConnection mySqlConnection;
    //database name
    public static string database = "timeblocks";
    //database IP
    private static string host = "45.77.71.189";
    //database user name
    private static string username = "TimeBlocks";
    //database password
    private static string password = "ih54J2K28PwrGfEx";
    //login code
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
    //Sign up for a new user
    
    //Check password
    public bool Login(string userName, string v)
    {
        //get password
        bool result = false;
        Debug.Log("Function name: Login, connecting to server.");
        string command=string.Format("SELECT password from USER_DATA WHERE user_name = '{0}'",userName);
        List<string> reader=ServerRead(command);
            if (reader[0].ToString().CompareTo(v)==0)
            {
                result = true;
            }
        Debug.Log("Success");
        return result;
    }
    //get user id in server by username
    private string GetID(string userName)
    {
        string userId = "";
        Debug.Log("Function name: GetID, connecting to server.");
        string command=string.Format("SELECT user_id FROM USER_DATA WHERE user_name = '{0}'",userName);
        List<string> reader = ServerRead(command);
        userId = reader[0].ToString();
        Debug.Log("Success");
        return userId;
    }
    //check whether the username have been used
    public bool CheckUserNameRepeated(string userName)
    {
        bool result = true;
        Debug.Log("Function name: CheckUserNameRepeated, connecting to server.");
        string command="SELECT user_name FROM USER_DATA";
        List<string>reader = ServerRead(command);
        foreach (string i in reader)
        {
            if (i.CompareTo(userName) == 0)
            {
                result = false;
            }
        }
        Debug.Log("Success");
        return result;
    }

    public bool CreateAccount(string text, string v, string email, DataManager dataManager) {
        try
        {
            string command = string.Format("INSERT INTO USER_DATA (user_name) VALUES ( '{0}','{1}','{2}' )",text,v,email);
            ServerWrite(command);
            dataManager.userName = text;
            dataManager.password = v;
            dataManager.email = email;
           dataManager.userId= GetID(text);
            dataManager.InitializeData();
            Push(dataManager);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
    public bool DeleteAccount(DataManager dataManager)
    {
        try
        {
            string command = string.Format("DELETE FROM USER_DATA WHERE user_id = {0}", dataManager.userId);
            ServerWrite(command);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
    //Save all
    public bool Push(DataManager dataManager)
    {
        try
        {
            string command = string.Format("UPDATE USER_DATA SET user_name = '{0}',email = '{1}', password = '{2}', color = {3}, default_tag_index  = {4},default_deadline = {5}",
                dataManager.userName, dataManager.email, dataManager.password,dataManager.color, dataManager.defaultTagIndex, dataManager.defaultDeadline.TotalSeconds
           );
            for (int i = 0; i < 7; i++)
            {
                TimeBlock tempBlock = dataManager.blocks[i];
                command += string.Format(", task_{0}_name = '{1}', task_{0}_deadline =  FROM_UNIXTIME({2}), task_{0}_estimate_time = {3}, task_{0}_tag_id = {4}", i, tempBlock._name, tempBlock._deadline, tempBlock._estimateTime, tempBlock._tagId);
            }
            for (int i = 1; i < 7; i++)
            {
                Tag tempTag = dataManager.tags[i];
                command += string.Format(", tag_{0}_name = '{1}', tag_{0}_image_id = {2}, tag_{0}_power = {3}", i, tempTag._name, tempTag._imageId, tempTag._power);
            }
            command += string.Format(", interruptions = '{0}'", dataManager.interruptions);
            command += string.Format(", concentration_time_sum = {0} , task_finished_count = {1}, task_failed_count = {2} , join_time = FROM_UNIXTIME({3})",dataManager.concentrationTimeSum,dataManager.taskFinishedCount,dataManager.taskFailedCount , GetTimestamp(dataManager.joinTime));

            for (int i = 0; i < 7; i++)
            {
                String reasons = dataManager.taskFailedReasons[i];
                command += string.Format(", task_failed_reason_{0} = '{1}'",i,reasons);
            }
            for (int i = 0; i < 7; i++)
            {
                double concentrationTime = dataManager.concentrationTime[i];
                command += string.Format(", concentration_time_{0} = {1}", i, concentrationTime);
            }
            command += string.Format(", longest_concentration_time = {0}", dataManager.longestConcentrationTime);
            for (int i = 0; i < 12; i++)
            {
                double concentrationTimeDistribution = dataManager.concentrationTimeDistribution[i];
                command += string.Format(", concentration_time_distribution_{0} = {1}", i, concentrationTimeDistribution);
            }
            command += string.Format(" WHERE user_id = {0}", dataManager.userId);
            ServerWrite(command);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
    // Load all;
    public bool Pull(DataManager dataManager)
    {
        try
        {
            string command = "SELECT user_name, email ,password , color, default_tag_index, default_deadline";
            for (int i = 0; i < 7; i++)
            {
                command += string.Format(", task_{0}_name, task_{0}_deadline, task_{0}_estimate_time, task_{0}_tag_id", i);
            }
            for (int i = 1; i < 7; i++)
            {
                command += string.Format(", tag_{0}_name, tag_{0}_image_id, tag_{0}_power", i);
            }
            command += ", interruptions";
            command += ", concentration_time_sum, task_finished_count, task_failed_count , join_time";
            for (int i = 0; i < 7; i++)
            {
                command += string.Format(", task_failed_reason_{0}", i);
            }
            for (int i = 0; i < 7; i++)
            {
                command += string.Format(", concentration_time_{0}", i);
            }
            command +=", longest_concentration_time";
            for (int i = 0; i < 12; i++)
            {
                command += string.Format(", concentration_time_distribution_{0}", i);
            }
            command += string.Format(" FROM USER_DATA WHERE user_id = {0}",dataManager.userId);
            List<string> reader = ServerRead(command);
            int index = 0;
            dataManager.userName = reader[index].ToString();
            dataManager.email = reader[index++].ToString();
            dataManager.password= reader[index++].ToString();
            dataManager.color = float.Parse(reader[index++].ToString());
            dataManager.defaultTagIndex = int.Parse(reader[index++].ToString());
            dataManager.defaultDeadline = TimeSpan.FromMinutes(double.Parse(reader[index++].ToString()));
            for (int i=0;i<7;i++)
            {
                TimeBlock tempBlock = new TimeBlock();
               tempBlock._name= reader[index++].ToString();
                tempBlock._deadline= long.Parse(reader[index++].ToString());
                tempBlock._estimateTime = int.Parse(reader[index++].ToString());
              tempBlock._tagId=int.Parse(reader[index++].ToString());
                dataManager.blocks[i]=tempBlock;
            }
            for (int i = 1; i < 7; i++)
            {
                Tag tempTag = new Tag();
                tempTag._name = reader[index++].ToString();
                tempTag._imageId=int.Parse( reader[index++].ToString());
                tempTag._power = int.Parse(reader[index++].ToString());
                dataManager.tags[i]=tempTag;
            }
            dataManager.interruptions = reader[index++].ToString();
            dataManager.concentrationTimeSum = double.Parse(reader[index++].ToString());
            dataManager.taskFinishedCount = int.Parse(reader[index++].ToString());
            dataManager.taskFailedCount =int.Parse(reader[index++].ToString());

            for (int i = 0; i < 7; i++)
            {
                dataManager.taskFailedReasons[i] = reader[index++].ToString();
            }
            for (int i = 0; i < 7; i++)
            {
                dataManager.concentrationTime[i]= double.Parse(reader[index++].ToString());
            }
            dataManager.longestConcentrationTime = double.Parse(reader[index++].ToString());
            for (int i = 0; i < 12; i++)
            {
               dataManager.concentrationTimeDistribution[i]=double.Parse(reader[index++].ToString());
            }
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    //Get timestamp
    public long GetTimestamp()
    {
        //Debug.Log(year+" "+month + " " +day + " " +chunk*6+5);
        TimeSpan st = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0);
        return Convert.ToInt64(st.TotalSeconds);
    }
    public long GetTimestamp(DateTime a)
    {
        //Debug.Log(year+" "+month + " " +day + " " +chunk*6+5);
        TimeSpan st = a - new DateTime(1970, 1, 1, 0, 0, 0);
        return Convert.ToInt64(st.TotalSeconds);
    }
    private bool ServerWrite(string command)
    {
        try
        {
            mySqlConnection = new MySqlConnection(sql);
            mySqlConnection.Open();
            Debug.Log("Connecting to server...");
            MySqlCommand cmd = new MySqlCommand(command, mySqlConnection);
            cmd.ExecuteNonQuery();
            mySqlConnection.Close();
            Debug.Log("Success");
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(string.Format("Error when executing {0}, error message : {1}", command, e));
            return false;
        }
    }
    private List<string> ServerRead(string command)
    {
        List<string> results = new List<string>();
        try
        {
            mySqlConnection = new MySqlConnection(sql);
            mySqlConnection.Open();
            Debug.Log("Connecting to server...");
            MySqlDataReader reader;
            MySqlCommand cmd = new MySqlCommand(command, mySqlConnection);
            reader = cmd.ExecuteReader();
            cmd.ExecuteNonQuery();
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    results.Add(reader[i].ToString());
                }
            }
            reader.Close();
            reader.Dispose();
            mySqlConnection.Close();
            mySqlConnection.Dispose();
            Debug.Log("Success");
            return results;
        }
        catch (Exception e)
        {
            Debug.Log(string.Format("Error when executing {0}, error message : {1}", command, e));
            return null;
        }
    }
}
