using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testdata
{
    public string MissionName;
    public string tag;
    public override string ToString()
    {
        return string.Format("Name: {0}, Tag: {1}", MissionName, tag);
    }
}
