using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    //here is the setting part

    public int userID;
    public bool completionCheck;
    public bool enableTimer;
  
    public bool analyseOCT;
 
    public int OCT;
    public bool OCTAuto;
 
    public Color backgroundColor;

    //here is the main canvas data
    public TimeBlock lastInput;
    public bool sortingByTime;

    public bool isAdvanced;
    public bool isAddNewTaskWindowAwake;

    public List<TimeBlock> blocks;
    
    public Dictionary<string,Tag> tagDictionary;
   
    public int chainSize;

    // Start is called before the first frame update
    void Start()
    {
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
}
