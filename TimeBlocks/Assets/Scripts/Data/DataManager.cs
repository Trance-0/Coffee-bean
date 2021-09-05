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
 
    public Color backgroundColor;

    //here is the main canvas data
    

    public List<TimeBlock> blocks;
    
    public Dictionary<int,Tag> tagDictionary;

    public List<double> OCT;

    //here is stats

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
        tagDictionary.Add(-1,new Tag("Untaged",0,1));
        tagDictionary.Add(1, new Tag("testTag", 1, 9));
        //    ds.LoadConfig(this, "config_0");
        //     chainSize = sortByTime.Count;
        blocks = sQLSaver.LoadBlocks();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
    {
     //   ds.SaveConfig(this,"config_0");
    }

    public void OCTUpDate(double newOCT) {
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
    }
    public void InitializeData(){
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
    public void SaveBlocks() {
        if (configManager.isOnline)
        {
            sQLSaver.SaveBlocks(blocks);
            blocks=sQLSaver.LoadBlocks();
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
    public void SaveSettings()
    {
        if (configManager.isOnline)
        {
            sQLSaver.SaveSettings(enableTimer,analyseOCT,manualOCT,OCTAuto);
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
    
    private double GetTime()
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        DateTime dt = startTime.AddSeconds(joinTime);
        DateTime tt = DateTime.Today;
        TimeSpan deltat = tt - dt;
        return deltat.Days;
    }
}
