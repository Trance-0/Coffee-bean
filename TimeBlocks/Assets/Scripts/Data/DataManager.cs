using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    //here is the setting part

    public int userID;

    public bool enableTimer;
    public bool analyseOCT;
 
    public int manualOCT;
    public bool OCTAuto;
 
    public Color backgroundColor;

    //here is the main canvas data
    

    public List<TimeBlock> blocks;
    
    public Dictionary<int,Tag> tagDictionary;

    public List<int> OCT;

    public int OCTSum;
    public int taskSum;
    public int interruptSum;
    public int OCTMax;
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
    {
     //   ds.SaveConfig(this,"config_0");
    }

    public void OCTUpDate(int newOCT) {
    }
    public void InitializeData(){
        Debug.Log("Date initialized");
    }
    public void Save() {
        Debug.Log("Data Saved");
    }
}
