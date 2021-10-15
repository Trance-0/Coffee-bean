using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="tags", menuName ="tags/new Tag")]
public class Tag : ScriptableObject
{
    public string _name;
    public int _imageId;
    public int _power;
    public int _tagId;
    public Tag(string name, int imageId, int power)
    {
        _name = name;
        _imageId = imageId;
        _power = power;
        _tagId = -1;
    }
    public Tag()
    {
        _name = "Default";
        _imageId = 0;
        _power =1;
        _tagId = -1;
    }
}
