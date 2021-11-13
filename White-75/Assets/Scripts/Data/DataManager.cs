using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    //here is the inital config
    public FocusManager focusManager;
    public ConfigManager configManager;
    //public JsonSaver jsonSaver;
    public IcalSaver icalSaver;
    public SQLSaver sQLSaver;

    // user data
    public string userName;
    public string email;
    public string password;
    public int userId;

    //backgroundColor needs to get value from color in start.
    public float color;
    public Color backgroundColor;
    public int defaultTagIndex;
    public TimeSpan defaultDeadline;

    // task data
    public TimeBlock[] blocks;
    // null task
    public TimeBlock nullTask;

    // tag data
    public Tag[] tags;

    // remaining task data
    public string interruptions;

    // statistics
    public double concentrationTimeSum;
    public int taskFinishedCount;
    public int taskFailedCount;
    public DateTime joinTime;
    public string[] taskFailedReasons;
    public double[] concentrationTime;
    public double longestConcentrationTime;
    public double[] concentrationTimeDistribution;
    public DateTime lastSaveTimeOnServer;
    public DateTime lastSaveTimeOnLocal;

    // Start is called before the first frame update
    void Start()
    {
        nullTask = Instantiate<TimeBlock>(ScriptableObject.CreateInstance<TimeBlock>());
        InitializeData();
        Tag defaultTag = Instantiate<Tag>(ScriptableObject.CreateInstance<Tag>());
        tags[0]=defaultTag;
        Debug.Log(tagDicionaryToString());
        //    ds.LoadConfig(this, "config_0");
        //     chainSize = sortByTime.Count;
        //DateTime a=DateTime.Now+new TimeSpan(0,0,20);
        //NotificationManager.SendNotification("TimeBlocks","Data Initialized",0,a.Hour,a.Minute,a.Second);
        focusManager.postNotification("test message");
    }

   public void InitializeData()
    {
        color = 0.36f;
        backgroundColor = Color.HSVToRGB(color,0.5f,1f);
        defaultTagIndex = 0;
        defaultDeadline = TimeSpan.FromDays(1);
        blocks = new TimeBlock[7];
        for (int i = 0; i < 7; i++)
        {
            blocks[i] = nullTask;
        }
        tags = new Tag[7];
        for (int i = 0; i < 7; i++)
        {
            Tag tempTag = Instantiate<Tag>(ScriptableObject.CreateInstance<Tag>());
            tempTag._name = string.Format("New Tag {0}",i);
            tags[i] = tempTag;
        }
        interruptions = "";
        concentrationTimeSum = 0;
        taskFinishedCount =0;
        taskFailedCount =0;
        joinTime = DateTime.Now;
        taskFailedReasons = new string[7];
        for (int i = 0; i < 7; i++)
        {
            taskFailedReasons[i] = "";
        }
        concentrationTime = new double[7];
        for (int i = 0; i < 7; i++)
        {
            concentrationTime[i] = 0;
        }

        longestConcentrationTime = 0;
        concentrationTimeDistribution = new double[12];
        for (int i = 0; i < 12; i++)
        {
            concentrationTimeDistribution[i] =0;
        }
    }

    public bool AddBlock(TimeBlock a)
    {
        for (int i =0;i<7;i++) {
            if (blocks[i]._name=="Unknown")
            {
                blocks[i] = a;
                return true;
            }
        }
        Debug.Log("Add block failed.");
        return false;
    }

    public bool RemoveBlock(TimeBlock a)
    {
        for (int i = 0; i < 7; i++)
        {
            if (blocks[i].IsSame(a))
            {
                blocks[i] = nullTask;
                taskFailedCount++;
                return true;
            }
        }
        Debug.Log("Remove block failed.");
        return false;
    }

    // Update is called once per frame
    void Update()
    {
    }
    public string tagDicionaryToString() {
        String result = "";
        foreach (Tag a in tags){
            result += a.ToString() ;
            result += "\n";
        }
        return result;
    }

    public void ConcentrationTimeUpDate(double record) {
        Debug.Log(string.Format("Concentration Time updateted with new record {0} min", record));
        if (record>longestConcentrationTime) {
            longestConcentrationTime= record;
        }
        //daily record
        if (DateTime.Now.Subtract(lastSaveTimeOnLocal).TotalSeconds<new TimeSpan(1,0,0,0).TotalSeconds) {
            concentrationTime[0] += record;
        }
        else {
            Debug.Log("Daily record reset.");
            concentrationTime[0] = record;
        }
        //two day record
        concentrationTime[1] = (concentrationTime[1]*2+ record) /3;
        //week record
        concentrationTime[2] = (concentrationTime[2]*6 + record) / 7;
        //month record
        concentrationTime[3] = (concentrationTime[3]*30 + record) / 31;
        //season record
        concentrationTime[4] = (concentrationTime[4] * 90 + record) / 91;
        //annual record
        concentrationTime[5] = (concentrationTime[5] * 365 + record) / 366;
        //total record
        concentrationTime[6] = concentrationTimeSum/taskFinishedCount;
    }

    private double GetTime()
    {
       return Convert.ToDouble(DateTime.Now.Subtract(joinTime).TotalDays);
    }
    // if you want to check where the function is called, you can change the name of the function for a short period time.
    // Don't forget to change it back again!
    public void LoadData(){
        icalSaver.LoadData(this);
        if (!configManager.isOffline)
        {
           sQLSaver.Pull(this);
        }
        backgroundColor = Color.HSVToRGB(color, 0.5f, 1f);
    }

    public void SaveData() {
        icalSaver.SaveData(this);
        if (!configManager.isOffline)
        {
            sQLSaver.Push(this);
        }
    }
    public void OnApplicationQuit()
    {
        SaveData();
    }
}
