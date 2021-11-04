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

    public TimeBlock(string name,int year, int month,int day, int chunk,int timeRequired,int tagId) {
        _name=name;
         _deadline=SetTime(year,month,day,chunk);
        _tagId=tagId;
        _estimateTime=timeRequired;
    }
    public TimeBlock(string name)
    {
          _name=name;
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);
         _deadline=SetTime(today.Year, today.Month,tomorrow.Day,3);
        _tagId=-1;
        _estimateTime=0;
    }
    public TimeBlock()
    {
          _name="Unkown";
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);
        _deadline=SetTime(today.Year, today.Month,tomorrow.Day,3);
        _tagId=-1;
        _estimateTime=0;
    }
    public long SetTime(int year, int month,int day,int chunk){
        //Debug.Log(year+" "+month + " " +day + " " +chunk*6+5);
        TimeSpan st = new DateTime(year, month, day, chunk*6+5, 59, 59) - new DateTime(1970, 1, 1, 0, 0, 0);
        return Convert.ToInt64(st.TotalSeconds);
    }

    public long GetPriority(Dictionary<int,Tag> tagPriorityList) {
        TimeSpan st = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
        if (tagPriorityList.ContainsKey(_tagId))
        {
            long priority = (Convert.ToInt64(st.TotalSeconds) - _deadline) * tagPriorityList[_tagId]._power;
            return priority;
        }
        else {
            Debug.Log(string.Format("Tag id not found, the tag id is {0}, but the tag dicitonary don't have the key like this.",_tagId));
            Debug.Log(tagDicionaryToString(tagPriorityList));
        }
        return -1;
    }
    //testing method, remove if possible.
    public string tagDicionaryToString(Dictionary<int, Tag> tagDictionary)
    {
        String result = "";
        foreach (KeyValuePair<int, Tag> a in tagDictionary)
        {
            result += a.ToString();
            result += "\n";
        }
        return result;
    }
    public DateTime ConvertDeadlineToDateTime() {
        return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(_deadline);
    }
    public override string ToString()
    {
        return string.Format("TimeBlock: name={0}, deadline={1}, estimate_time={2}min, tag_id={3}, task_id", _name, ConvertDeadlineToDateTime(), _estimateTime, _tagId,_taskId);
    }
}
