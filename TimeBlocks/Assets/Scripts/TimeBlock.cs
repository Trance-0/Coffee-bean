using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBlock
{
    private string _name;
    private int[] _time;
    private int _priority;
    private LinkedList<int>_tags;
    private bool _isOver;
    private bool _isFailed;

    public TimeBlock(string name,int[] time,int priority,LinkedList<int> tags) {
        SetName(name);
        SetTime(time);
        SetPriority(priority);
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
    public bool SetPriority(int priority) 
    {
        try
        {
            _priority=priority;
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
    public LinkedList<int> Tags() {
        return Tags;
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
    public boolean update(int[] currentTime) {
        for (int i=0;i<6;i++) {
            if (currentTime[i]<_time[i]) { 
                reuturn true;
            }
            _isFailed = true;
            return false;
        }
    }
    public void checkFinished() {
        _isOver = true;
    }
}
