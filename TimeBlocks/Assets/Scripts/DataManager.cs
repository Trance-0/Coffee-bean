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
    public PriorityQueue<TimeBlock> sortByTime;
    public PriorityQueue<TimeBlock> sortByPriority;
    public Dictionary<string,Tag> tagDictionary;
    public class timeSort : Comparer<TimeBlock>
    {
        // Compares by Length, Height, and Width.
        public override int Compare(TimeBlock x, TimeBlock y)
        {
            return x.Compare(y);
        }
    }
    public class prioritySort : Comparer<TimeBlock>
    {
        // Compares by Length, Height, and Width.
        public override int Compare(TimeBlock x, TimeBlock y)
        {
            return x.Priority()-y.Priority();
        }
    }
    public int chainSize;
    public bool sortingByTime;
    // Start is called before the first frame update
    void Start()
    {
            sortByTime = new PriorityQueue<TimeBlock>(new timeSort());
            sortByPriority = new PriorityQueue<TimeBlock>(new prioritySort());
        chainSize = sortByTime.Count();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
