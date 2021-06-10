using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public TimeBlock lastInput;
    public bool conpletionCheck;
    public bool enableTimer;
    public bool analyseOptimalConcentrationTime;
    public Color backgroundColor;
    public bool sortingByTime;

    public bool isAdvanced;
    public bool isAddNewTaskWindowAwake;
    public List<TimeBlock> sortByTime;
    public List<TimeBlock> sortByPriority;
    public List<TimeBlock> finishedTask;
    public List<TimeBlock> deletedTask;
    public Dictionary<string,Tag> tagDictionary;
   
    public int chainSize;
    // Start is called before the first frame update
    void Start()
    {
        sortByTime = new List<TimeBlock>();
        sortByPriority = new List<TimeBlock>();
        chainSize = sortByTime.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
