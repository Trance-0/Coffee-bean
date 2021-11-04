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
    public Tag(string name, int imageId, int power)
    {
        _name = name;
        _imageId = imageId;
        _power = power;
    }
    public Tag()
    {
        _name = "Default";
        _imageId = 0;
        _power =1;
    }
    public override string ToString() {
        return string.Format("Tag: name={0}, image_id={1}, power={2}, tag_id={3}",_name,_imageId,_power,_tagId);
    }
}
