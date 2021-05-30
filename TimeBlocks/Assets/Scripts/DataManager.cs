using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour
{
    public TimeBlock lastInput;
    public bool isAdvanced;
    public bool isAddNewTaskWindowAwake;
    public List<TimeBlock> sortByTime;
    public List<TimeBlock> sortByPriority;
    public List<TimeBlock> finishedTask;
    public List<TimeBlock> deletedTask;
    public Dictionary<string,Tag> tagDictionary;
   
    public int chainSize;
    public bool sortingByTime;
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
