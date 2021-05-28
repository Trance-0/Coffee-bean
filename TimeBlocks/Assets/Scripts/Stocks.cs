using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="tags", menuName ="tags/new Tag")]
public class Tag : ScriptableObject
{
    public double _power;
    public string _tagName;
    public Sprite _image;
    public Tag() {
        _power = 0;
        _tagName = "Untaged";
    }
    public double Power() {
        return _power;
    }
    public bool SetPower(double power) {
        try
        {
            _power = power;
            return true;
        }
        catch (Exception e) {
            Debug.Log(e);
            return false;
        }
    }
    public string TagName()
    {
        return _tagName;
    }
    public bool SetTagName(string tagName)
    {
        try
        {
            _tagName=tagName;
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
    public Sprite Image() {
        return _image;
    }
    public bool SetImage(Sprite image)
    {
        try
        {
            _image=image;
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return false;
        }
    }
}
