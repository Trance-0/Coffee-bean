using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    //here is the setting part

    public bool completionCheck;
    public Toggle completionCheckToggle;
    public void CompletionCheckUpdate() {
        completionCheck = completionCheckToggle.isOn;
    }

    public bool enableTimer;
    public Toggle enableTimerToggle;
    public void enableTimerUpdate()
    {
        enableTimer = enableTimerToggle.isOn;
    }

    public bool analyseOCT;
    public Toggle analyseOCTToggle;
    public void analyseOCTUpdate()
    {
        analyseOCT = analyseOCTToggle.isOn;
    }

    public int OCT;
    public bool OCTAuto;
    public InputField OCTValue;
    public Toggle OCTAutoToggle;
    public void OCTAutoUpdate()
    {
        OCTAuto = OCTAutoToggle.isOn;
    }
    public void OCTUpdate()
    {
        OCT= int.Parse(OCTValue.text);
    }

    public Color backgroundColor;

    //here is the main canvas data
    public TimeBlock lastInput;
    public bool sortingByTime;

    public bool isAdvanced;
    public bool isAddNewTaskWindowAwake;

    public List<TimeBlock> sortByTime;
    public List<TimeBlock> sortByPriority;
    public List<TimeBlock> finishedTask;
    public List<TimeBlock> deletedTask;

    public List<Tag> defaultTag;
    public Dictionary<string,Tag> tagDictionary;
   
    public int chainSize;

    public DataSaver ds;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            ds.LoadConfig(this, "config_0");
        }catch (Exception e) {

            }
        chainSize = sortByTime.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnApplicationQuit()
    {
        ds.SaveConfig(this,"config_0");    }
}
