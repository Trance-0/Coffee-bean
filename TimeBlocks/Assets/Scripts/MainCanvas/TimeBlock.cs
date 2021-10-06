using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeBlock", menuName = "TimeBlocks/new timeblock")]
public class TimeBlock : ScriptableObject
{
    public string _name;
    public long _deadline;
    public int _estimateTime;
    public int _tagId;
    public int _taskId;

    public TimeBlock(string name,int year, int month,int day, int chunk,int timeRequired,int tagId) {
        _name=name;
         _deadline=SetTime(year,month,day,chunk);
        _tagId=tagId;
        _estimateTime=timeRequired;
        _taskId = -1;
    }
    public TimeBlock(string name)
    {
          _name=name;
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);
         _deadline=SetTime(today.Year, today.Month,tomorrow.Day,3);
        _tagId=-1;
        _estimateTime=-1;
        _taskId = -1;
    }
    public TimeBlock()
    {
          _name="Unkown";
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);
        _deadline=SetTime(today.Year, today.Month,tomorrow.Day,3);
        _tagId=-1;
        _estimateTime=-1;
    }
    public long SetTime(int year, int month,int day,int chunk){
        //Debug.Log(year+" "+month + " " +day + " " +chunk*6+5);
        TimeSpan st = new DateTime(year, month, day, chunk*6+5, 59, 59) - new DateTime(1970, 1, 1, 0, 0, 0);
        return Convert.ToInt64(st.TotalSeconds);
    }

    public long GetPriority(Dictionary<int,Tag> tagPriorityList) {
        TimeSpan st = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
        return (Convert.ToInt64(st.TotalSeconds)-_deadline)*tagPriorityList[_tagId]._power;
    }
    public DateTime ConvertDeadlineToDateTime() {
        return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(_deadline);
    }
}
