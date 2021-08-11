using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeBlock", menuName = "TimeBlocks/new timeblock")]
public class TimeBlock : ScriptableObject
{
    public string _name;
    public int _year;
    public int _month;
    public int _day;
    public int _chunk;
    public int _timeRequired;
    public string _tag;
    public int _priority;
    public bool _isOver;
    public bool _isFailed;

    public TimeBlock(string name,int year, int month,int day, int chunk,int timeRequired,string tag) {
        SetName(name);
        SetTime(year,month,day,chunk);
        SetTags(tag);
        SetTimeRequired(timeRequired);
        _isOver = false;
        _isFailed = false;
        //update(,tagPriorityList);
    }
    public TimeBlock(string name)
    {
        SetName(name);
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);
        SetTime(today.Year, today.Month,tomorrow.Day,3);
        SetTags("Untaged");
        SetTimeRequired(-1);
        _isOver = false;
        _isFailed = false;
        //update(,tagPriorityList);
    }
    public TimeBlock()
    {
        SetName("Unknown");
        DateTime today = DateTime.Today;
        DateTime tomorrow = today.AddDays(1);
        SetTime(today.Year, today.Month, tomorrow.Day, 3);
        SetTags("Untaged");
        SetTimeRequired(-1);
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
    public int Compare(TimeBlock a) 
    {
        return (_year - a._year) * 8640 + (_month - a._month) * 720 + (_day - a._day) * 24 + (_chunk - a._chunk);
    }
    public bool SetTime(int year, int month, int day, int chunk)
    {
        try
        {
            _year = year;
            _month = month;
            _day = day;
            _chunk = chunk;
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
    public bool update(Dictionary<string,Tag> tagPriorityList) {
        int timeRemaining = (_year - DateTime.Now.Year) * 8640 + (_month - DateTime.Now.Month) *720 + (_day - DateTime.Now.Day) * 24 + (_chunk*6-DateTime.Now.Hour);
        if (timeRemaining<0) { 
            _isFailed = true;
        }
        Tag tempTag;
        tagPriorityList.TryGetValue(_tag,out tempTag);
        return false;
    }
    public void checkFinished() {
        _isOver = true;
    }
}
