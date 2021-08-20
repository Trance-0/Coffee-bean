using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeBlock", menuName = "TimeBlocks/new timeblock")]
public class TimeBlock : ScriptableObject
{
    public string _name;
    public long _timeStamp;
    public int _timeRequired;
    public int _tagId;

    public TimeBlock(string name,int year, int month,int day, int chunk,int timeRequired,int tagId) {
        _name=name;
         _timeStamp=SetTime(year,month,day,chunk);
        _tagId=tagId;
        _timeRequired=timeRequired;
    }
    public TimeBlock(string name)
    {
          _name=name;
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);
         _timeStamp=SetTime(today.Year, today.Month,tomorrow.Day,3);
        _tagId=-1;
        _timeRequired=-1;
    }
    public TimeBlock()
    {
          _name="Unkown";
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);
        _timeStamp=SetTime(today.Year, today.Month,tomorrow.Day,3);
        _tagId=-1;
        _timeRequired=-1;
    }
    public long SetTime(int year, int month,int day,int chunk){
       
        return 0;
    }

    public long GetPriority(Dictionary<int,Tag> tagPriorityList) {
       return 0;
    }
}
