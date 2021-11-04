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
    public JsonSaver jsonSaver;
    public SQLSaver sQLSaver;

    // user data
    public string userName;
    public string email;
    public float color;
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


    // Start is called before the first frame update
    void Start()
    {
        tags = new Tag[8];
        Tag defaultTag = new Tag();
        tags[0]=defaultTag;
        Debug.Log(tagDicionaryToString());
        //    ds.LoadConfig(this, "config_0");
        //     chainSize = sortByTime.Count;
        //DateTime a=DateTime.Now+new TimeSpan(0,0,20);
        //NotificationManager.SendNotification("TimeBlocks","Data Initialized",0,a.Hour,a.Minute,a.Second);
        focusManager.postNotification("test message");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
    {
     SaveAll();
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
  
        concentrationTimeSum += record;
        //daily record
        concentrationTime[0] = record;
        //two day record
        concentrationTime[1] = (concentrationTime[1]*2+ record) /3;
        //week record
        concentrationTime[2] = (concentrationTime[2]*6 + record) / 7;
        //month record
        concentrationTime[3] = (concentrationTime[3]*30 + record) / 31;
        //season record
        concentrationTime[4] = (concentrationTime[3] * 90 + record) / 91;
        //annual record
        concentrationTime[5] = (concentrationTime[3] * 365 + record) / 366;
        //total record
        concentrationTime[6] = (concentrationTime[3] *  GetTime()-1+ record) / GetTime();
        SaveData();
    }

    private double GetTime()
    {
       return Convert.ToDouble(DateTime.Now.Subtract(joinTime).TotalDays);
    }

    public void LoadData(){
        if (configManager.isOffline||sQLSaver.Pull(this))
        {
            jsonSaver.LoadData();
        }
    }
    public void SaveData() {
        if (configManager.isOffline || sQLSaver.Push(this))
        {
            jsonSaver.SaveData();
        }
    }
}
