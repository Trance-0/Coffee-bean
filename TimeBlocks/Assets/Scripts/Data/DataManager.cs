using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    //here is the inital config
    
    public ConfigManager configManager;
    public JsonSaver jsonSaver;
    public SQLSaver sQLSaver;

    //here is the setting part

    public bool enableTimer;
    public bool analyseOCT;
 
    public int manualOCT;
    public bool OCTAuto;
    public int defaultTagId;
 
    public Color backgroundColor;

    //here is the main canvas data
    

    public List<TimeBlock> blocks;

    public Dictionary<int, Tag> tagDictionary;

    public List<double> OCT;

    //here is statistics

    public double OCTSum;
    public int taskSum;
    public int interruptSum;
    public double OCTMax;
    public int AppUseSum;
    public long joinTime;
   
    // Start is called before the first frame update
    void Start()
    {
       tagDictionary = new Dictionary<int, Tag>();
        Tag defaultTag = new Tag();
        defaultTag._tagId = 0;
        tagDictionary.Add(0,defaultTag);
        Debug.Log(tagDicionaryToString());
        //    ds.LoadConfig(this, "config_0");
        //     chainSize = sortByTime.Count;
        DateTime a=DateTime.Now+new TimeSpan(0,0,20);
        NotificationManager.SendNotification("TimeBlocks","Data Initialized",0,a.Hour,a.Minute,a.Second);
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
        foreach (KeyValuePair<int,Tag> a in tagDictionary){
            result += a.ToString() ;
            result += "\n";
        }
        return result;
    }
    public void OCTUpDate(double newOCT) {
        Debug.Log(string.Format("OCT up dateted with new record {0} min",newOCT));
        if (newOCT>OCTMax) {
            OCTMax = newOCT;
        }
        OCTSum += newOCT;
        OCT[0] = newOCT;
        OCT[1] = (OCT[1]*2+newOCT)/3;
        OCT[2] = (OCT[2]*6 + newOCT) / 7;
        OCT[3] = (OCT[3]*30 + newOCT) / 31;
        OCT[4] = (OCT[3] * 90 + newOCT) / 91;
        OCT[5] = (OCT[3] * 365 + newOCT) / 366;
        OCT[6] = (OCT[3] *  GetTime()-1+ newOCT) / GetTime();
        SaveOCT();
    }
    public void InitializeData(){
        LoadTags();
        LoadBlocks();
        LoadOCT();
        LoadSettings();
       LoadStats();
        if (!tagDictionary.ContainsKey(defaultTagId)) {
            defaultTagId = tagDictionary.GetEnumerator().Current.Key;
        }
        Debug.Log("Data initialized");
    }
    public void SaveAll() {
        SaveBlocks();
        SaveTags();
        SaveOCT();
        SaveSettings();
        SaveStats();
        Debug.Log("Data Saved");
    }
    public void RemoveBlock(TimeBlock a) {
        blocks.Remove(a);
        if (configManager.isOnline)
        {
            sQLSaver.RemoveBlock(a);
        }
        else {
            Debug.Log("Json saver not implemented");
        }
    }
    public void SaveBlock(TimeBlock a)
    {
        blocks.Remove(a);
        if (configManager.isOnline)
        {
            sQLSaver.SaveBlock(a);
        }
        else
        {
            Debug.Log("Json saver not implemented");
        }
    }
    public void SaveBlocks() {
        if (configManager.isOnline)
        {
            sQLSaver.SaveBlocks(blocks);
        }
        else
        {
            Debug.Log("Json saver not implemented");
        }
    }
    public void LoadBlocks() {
        if (configManager.isOnline)
        {
            blocks = sQLSaver.LoadBlocks();
        }
        else
        {
            Debug.Log("Json saver not implemented");
        }
    }
    public void SaveTags()
    {
        if (configManager.isOnline)
        {
            sQLSaver.SaveTags(tagDictionary);
        }
        else
        {
            Debug.Log("Json saver not implemented");
        }
    }
    //if the user is new, the method would be called twice
    public void LoadTags()
    {
        if (configManager.isOnline)
        {
           tagDictionary=sQLSaver.LoadTags();
            if (tagDictionary.Count == 1) {
                tagDictionary = sQLSaver.LoadTags();
            }
        }
        else
        {
            Debug.Log("Json saver not implemented");
        }
    }
    public void SaveOCT()
    {
        if (configManager.isOnline)
        {
            sQLSaver.SaveOCT(OCT);
        }
        else
        {
            Debug.Log("Json saver not implemented");
        }
    }

    public void LoadOCT()
    {
        if (configManager.isOnline)
        {
            OCT=sQLSaver.LoadOCT();
        }
        else
        {
            Debug.Log("Json saver not implemented");
        }
    }

    public void SaveSettings()
    {
        if (configManager.isOnline)
        {
            sQLSaver.SaveSettings(enableTimer,analyseOCT,manualOCT,OCTAuto,defaultTagId);
        }
        else
        {
            Debug.Log("Json saver not implemented");
        }
    }

    public void LoadSettings()
    {
        if (configManager.isOnline)
        {
            sQLSaver.LoadSettings(out enableTimer, out analyseOCT, out manualOCT,out OCTAuto,out defaultTagId);
        }
        else
        {
            Debug.Log("Json saver not implemented");
        }
    }

    public void SaveStats()
    {
        if (configManager.isOnline)
        {
            sQLSaver.SaveStats(OCTSum,taskSum,interruptSum,OCTMax,AppUseSum,joinTime);
        }
        else
        {
            Debug.Log("Json saver not implemented");
        }
    }

    public void LoadStats()
    {
        if (configManager.isOnline)
        {
            sQLSaver.LoadStats(out OCTSum, out taskSum, out interruptSum, out OCTMax, out AppUseSum, out joinTime);
        }
        else
        {
            Debug.Log("Json saver not implemented");
        }
    }

    private double GetTime()
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        DateTime dt = startTime.AddSeconds(joinTime);
        DateTime tt = DateTime.Today;
        TimeSpan deltat = tt - dt;
        return deltat.Days;
    }
}
