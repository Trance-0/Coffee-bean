using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeBlock", menuName = "TimeBlocks/new timeblock")]
public class TimeBlock : ScriptableObject
{
    public string _name;
    public int[] _time;
    public int _timeRequired;
    public string _tag;
    public int _priority;
    public bool _isOver;
    public bool _isFailed;

    public TimeBlock(string name,int[] time,int timeRequired,string tag,Dictionary<string, int> tagPriorityList) {
        SetName(name);
        SetTime(time);
        SetTags(tag);
        SetTimeRequired(timeRequired);
        _isOver = false;
        _isFailed = false;
        //update(,tagPriorityList);
    }
    public TimeBlock(string name)
    {
        SetName(name);
        int[] nullTime = { 0, 0, 0, 0, 0, 0 };
        SetTime(nullTime);
        SetTags("Untaged");
        SetTimeRequired(-1);
        _isOver = false;
        _isFailed = false;
        //update(,tagPriorityList);
    }
    public TimeBlock(string name, int[] time, string tag, Dictionary<string, int> tagPriorityList)
    {
        SetName(name);
        SetTime(time);
        SetTags(tag);
        _isOver = false;
        _isFailed = false;
        //update(,tagPriorityList);
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
            Debug.Log(e);
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
            Debug.Log(e);
            return false;
        }
    }
    public int TimeRequired()
    {
        return _timeRequired;
    }
    public bool SetTimeRequired(int timeRequired)
    {
        try
        {
            _timeRequired = timeRequired;
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
    public int Priority ()
    {
        return _priority;
    }
    public string Tag() {
        return _tag;
    }
    public bool SetTags(string tag)
    {
        try
        {
            _tag=tag;
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
    public bool update(int[] currentTime,Dictionary<string,int> tagPriorityList) {
        int timeRemaining = (currentTime[0] - _time[0]) * 1030 + (currentTime[1] - _time[1]) * 30 * 24 * 60 + (currentTime[2] - _time[2]) * 24 * 60 + (currentTime[3] - _time[3]) * 60 + (currentTime[4] - _time[4]);
        if (timeRemaining<0) { 
            _isFailed = true;
        }
        tagPriorityList.TryGetValue(_tag,out _priority);
        return false;
    }
    public void checkFinished() {
        _isOver = true;
    }
}
