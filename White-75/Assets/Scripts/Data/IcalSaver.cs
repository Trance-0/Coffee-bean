using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class IcalSaver : MonoBehaviour
{
    public string dir;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        dir = Application.persistentDataPath;
        //dir = "/storage/emulated/0/Android/data/com.Trance0.iCalendarApp/files";
        //dir = "/storage/emulated/0/Android/data/com.DefaultCompany.TimeBlocks/files";
#endif
#if UNITY_PC
        dir = Application.dataPath;
#endif
#if UNITY_EDITOR
        dir = Application.dataPath;
#endif
        dir = dir + "/USER_DATA.ics";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool LoadData(DataManager dataManager) {
        try
        {
            if (LocalRead().Count == 0)
            {
                dataManager.userName = "Undefined";
                dataManager.password = "Unknown";
                dataManager.email = "Undefined@colorfulnumbers.com";
                dataManager.userId = -1;
                SaveData(dataManager);
            }
            try
            {
                List<string> reader = LocalRead();
                int index = reader.IndexOf("BEGIN:SAVE")+1;
                dataManager.userName = reader[index++].Split(':')[1];
                dataManager.email = reader[index++].Split(':')[1];
                dataManager.password = reader[index++].Split(':')[1];
                dataManager.userId= int.Parse(reader[index++].Split(':')[1]);
                string temp = reader[index++];
                Debug.Log(string.Format("Reading line: {0}, data set to {1}",temp,"color"));
                dataManager.color = float.Parse(temp.Split(':')[1]);
                dataManager.defaultTagIndex = int.Parse(reader[index++].Split(':')[1]);
                dataManager.defaultDeadline = TimeSpan.FromMinutes(double.Parse(reader[index++].Split(':')[1]));
                for (int i = 0; i < 7; i++)
                {
                    TimeBlock tempBlock = Instantiate<TimeBlock>(ScriptableObject.CreateInstance<TimeBlock>());
                    tempBlock._name = reader[index++].Split(':')[1];
                    tempBlock._deadline = long.Parse(reader[index++].Split(':')[1]);
                    tempBlock._estimateTime = int.Parse(reader[index++].Split(':')[1]);
                    tempBlock._tagId = int.Parse(reader[index++].Split(':')[1]);
                    dataManager.blocks[i] = tempBlock;
                }
                for (int i = 0; i < 7; i++)
                {
                    Tag tempTag = Instantiate<Tag>(ScriptableObject.CreateInstance<Tag>());
                    tempTag._name = reader[index++].Split(':')[1];
                    tempTag._imageId = int.Parse(reader[index++].Split(':')[1]);
                    tempTag._power = int.Parse(reader[index++].Split(':')[1]);
                    dataManager.tags[i] = tempTag;
                }
                dataManager.interruptions = reader[index++].Split(':')[1];
                dataManager.concentrationTimeSum = double.Parse(reader[index++].Split(':')[1]);
                dataManager.taskFinishedCount = int.Parse(reader[index++].Split(':')[1]);
                dataManager.taskFailedCount = int.Parse(reader[index++].Split(':')[1]);
                dataManager.joinTime = ConvertTimeStampToDateTime(int.Parse(reader[index++].Split(':')[1]));
                for (int i = 0; i < 7; i++)
                {
                    dataManager.taskFailedReasons[i] = reader[index++].Split(':')[1];
                }
                for (int i = 0; i < 7; i++)
                {
                    dataManager.concentrationTime[i] = double.Parse(reader[index++].Split(':')[1]);
                }
                dataManager.longestConcentrationTime = double.Parse(reader[index++].Split(':')[1]);
                for (int i = 0; i < 12; i++)
                {
                    dataManager.concentrationTimeDistribution[i] = double.Parse(reader[index++].Split(':')[1]);
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return false;
            }
        }
        catch (Exception e) {
            Debug.Log(e);
            return false;
        }
    }

    public bool checkSaveTime(DataManager dataManager)
    {
        try
        {
            List<string> reader = LocalRead();
            foreach(string i in reader) {
                if (i.Contains("LASTSAVETIME")) {
                       dataManager.lastSaveTimeOnLocal= ConvertTimeStampToDateTime(int.Parse(i.Split(':')[1]));
                    return true;
                }
            }
        }
        catch (Exception e) {
            Debug.Log(e);
        }
        return false;
    }

    public bool SaveData(DataManager dataManager)
    {
        try
        {
            List<string> data = new List<string>();
            data.Add("BEGIN:VCALENDAR");
            data.Add (string.Format("PRODID:-//{0}//{1} {2}//EN", Application.companyName, Application.productName, Application.version));
            data.Add("VERSION:2.0");
            data.Add("CALSCALE:GREGORIAN");
            data.Add("METHOD:PUBLISH");
            data.Add(string.Format("X-WR-CALNAME:{0}",Application.productName));
            data.Add("X-WR-TIMEZONE:UTC");
            foreach (TimeBlock a in dataManager.blocks)
            {
                if (a._name.CompareTo("Unknown")==0) {
                    continue;
                }
                data.Add("BEGIN:VEVENT");
                DateTime deadline = a.ConvertDeadlineToDateTime();
                data.Add(string.Format("DTSTART;VALUE=DATE:{0}{1}{2}", deadline.Year, deadline.Month, deadline.Day));
                data.Add(string.Format("DTEND;VALUE=DATE:{0}{1}{2}", deadline.Year, deadline.Month, deadline.Day + 1));
                data.Add(string.Format("DTSTAMP:{0}{1}{2}T{3}{4}{5}Z", deadline.Year, deadline.Month, deadline.Day, deadline.Hour, deadline.Minute, deadline.Second));
                data.Add(string.Format("UID:{0}@{1}.com", a.GetHashCode(), Application.productName));
                data.Add(string.Format("CREATED:{0}{1}{2}T{3}{4}{5}Z", dataManager.joinTime.Year, dataManager.joinTime.Month, dataManager.joinTime.Day, dataManager.joinTime.Hour, dataManager.joinTime.Minute, dataManager.joinTime.Second));
                data.Add(string.Format("DESCRIPTION:{0}", a.ToString()));
                data.Add(string.Format("LAST-MODIFIED:{0}{1}{2}T{3}{4}{5}Z", dataManager.joinTime.Year, dataManager.joinTime.Month, dataManager.joinTime.Day, dataManager.joinTime.Hour, dataManager.joinTime.Minute, dataManager.joinTime.Second));
                data.Add("LOCATION:");
                data.Add("SEQUENCE:0");
                data.Add("STATUS:CONFIRMED");
                data.Add(string.Format("SUMMARY:{0}", a._name));
                data.Add("TRANSP:TRANSPARENT");
                data.Add("END:VEVENT");
            }
                data.Add("END:VCALENDAR");
                data.Add("BEGIN:SAVE");
                data.Add(string.Format("USERNAME:{0}", dataManager.userName));
                data.Add(string.Format("EMAIL:{0}", dataManager.email));
                data.Add(string.Format("PASSWORD:{0}", dataManager.password));
                data.Add(string.Format("USERID:{0}", dataManager.userId));
                data.Add(string.Format("COLOR:{0}", dataManager.color));
                data.Add(string.Format("DEFAULTTAGINDEX:{0}", dataManager.defaultTagIndex));
                data.Add(string.Format("DEFAULTDEADLINE:{0}", dataManager.defaultDeadline.TotalMinutes));
            for (int i = 0; i < 7; i++)
            {
                TimeBlock tempBlock = dataManager.blocks[i];
                data.Add(string.Format("TASK{0}NAME:{1}",i,tempBlock._name));
                data.Add(string.Format("TASK{0}DEADLINE:{1}",i,tempBlock._deadline)); 
                data.Add(string.Format("TASK{0}ESTIMATETIME:{1}",i,tempBlock._estimateTime));
                data.Add(string.Format("TASK{0}TAGID:{1}",i,tempBlock._tagId));
            }
            for (int i = 0; i < 7; i++)
            {
                Tag tempTag = dataManager.tags[i];
                data.Add(string.Format("TAG{0}NAME:{1}",i,tempTag._name));
                data.Add(string.Format("TAG{0}IMAGEID:{1}",i,tempTag._imageId));
                data.Add(string.Format("TAG{0}POWER:{1}",i,tempTag._power));
            }
            data.Add(string.Format("INTERRUPTIONS:{0}", dataManager.interruptions));
            data.Add(string.Format("CONCENTRATIONTIMESUM:{0}",dataManager.concentrationTimeSum));
            data.Add(string.Format("TASKFINISHEDCOUNT:{0}",dataManager.taskFinishedCount));
            data.Add(string.Format("TASKFAILEDCOUNT:{0}", dataManager.taskFailedCount));
            data.Add(string.Format("JOINTIME:{0}",ConvertDateTimeToTimeStamp(dataManager.joinTime)));
            for (int i = 0; i < 7; i++)
            {
                string reasons = dataManager.taskFailedReasons[i];
                data.Add( string.Format("TASKFAILEDREASON{0}:{1}",i,reasons));
            }
            for (int i = 0; i < 7; i++)
            {
                double concentrationTime = dataManager.concentrationTime[i];
                data.Add(string.Format("CONCENTRATIONTIME{0}:{1}", i, concentrationTime));
            }
            data.Add(string.Format("LONGESTCONCENTRATIONTIME:{0}", dataManager.longestConcentrationTime));
            for (int i = 0; i < 12; i++)
            {
                double concentrationTimeDistribution = dataManager.concentrationTimeDistribution[i];
                data.Add(string.Format("CONCENTRATIONTIMEDISTRIBUTION{0}:{1}", i, concentrationTimeDistribution));
            }
            dataManager.lastSaveTimeOnLocal = DateTime.Now;
            data.Add(string.Format("LASTSAVETIME:{0}", ConvertDateTimeToTimeStamp(dataManager.lastSaveTimeOnLocal)));
            data.Add("END:SAVE");
            LocalWrite(data);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }

    public bool LocalWrite(List<string> data) {
        try
        {
            StreamWriter sw;
            if (File.Exists(dir)) {
                File.Delete(dir);
            }
            Debug.Log("Connecting to local...");
            string message = "";
            sw = File.CreateText(dir);
            foreach (string i in data) {
                message += i+"\n";
                sw.WriteLine(i);
            }
            Debug.Log(string.Format("Writing message {0} to local file",message));
            sw.Close();
            sw.Dispose();
            return true;
        }
        catch (Exception e) {
            Debug.Log(e);
            return false;
        }
    }
    public List<string> LocalRead() {
        try
        {
            StreamReader reader;
            List<string> results = new List<string>();
            Debug.Log("Connecting to local...");
            string message = "";
            if (File.Exists(dir))
            {
                FileStream fs = new FileStream(dir, FileMode.Open);
                reader = new StreamReader(fs);
                string line;
                while ((line = reader.ReadLine()) != null) {
                    results.Add(line);
                    message += line + "\n";
                }
                reader.Close();
                reader.Dispose();
                fs.Close();
                fs.Dispose();
            }
            Debug.Log(string.Format("Reading message {0} from local file", message));
            Debug.Log("Success");
            return results;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }

    private string GetTzAbbreviation(string timeZoneName)
    {
        string output = string.Empty;

        string[] timeZoneWords = timeZoneName.Split(' ');
        foreach (string timeZoneWord in timeZoneWords)
        {
            if (timeZoneWord[0] != '(')
            {
                output += timeZoneWord[0];
            }
            else
            {
                output += timeZoneWord;
            }
        }
        return output;
    }
    private int ConvertDateTimeToTimeStamp(DateTime a) {
        return (int)Math.Ceiling((a - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds);
    }
    private DateTime ConvertTimeStampToDateTime(int a) {
        return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(a);
    }
}
