using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBlock
{
    private string _name;
    private int[] _time;
    private int _timeRequired;
    private string _tag;
    private int _priority;
    private bool _isOver;
    private bool _isFailed;

    public TimeBlock(string name,int[] time,string tag,Dictionary tagPriorityList) {
        SetName(name);
        SetTime(time);
        SetPriority(time,tagPriorityList);
        SetTags(tags);
        _isOver = false;
        _isFailed = false;
    }
    public string Name(){
        return _name;
    }
    public bool SetName(string name)
    {
        try
        {
            _name = name;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public int[] Time()
    {
        return _time;
    }
    public bool SetTime(int[]time)
    {
        try
        {
            _time = time;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public int Priority ()
    {
        return _priority;
    }
    public string Tag() {
        return Tag;
    }
    public bool SetTags(LinkedList<int> tags)
    {
        try
        {
            _tags=tags;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public boolean update(int[] currentTime,Dictionary<string,int> tagPriorityList) {
        int timeRemaining=(currentTime[0]-_time[0])*1030+ (currentTime[1] - _time[1])*30*24*60+ (currentTime[2] - _time[2])*24*60+ (currentTime[3] - _time[3])*60+ (currentTime[4] - _time[4])
        if (timeRemaining<0) { 
            _isFailed = true;
        }
        _priority=
        return false;
    }
    public void checkFinished() {
        _isOver = true;
    }
}
