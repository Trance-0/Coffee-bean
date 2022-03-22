using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using System;

public class JsonSaver : MonoBehaviour
{
    // Start is called before the first frame update
    public string workingDir;
    public bool autoSearchForDirectory=true;
    void Start()
    {
        if (autoSearchForDirectory) {
            workingDir=Directory.GetCurrentDirectory();
        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    
    
    internal void LoadData()
    {
        throw new NotImplementedException();
    }

    internal void SaveData()
    {
        throw new NotImplementedException();
    }

   
    public void TestLoadJson() {
        List<testdata> temp = new List<testdata>();
        string json = Resources.Load("testData").ToString();
        JsonData jd = JsonMapper.ToObject(json);
        testdata td = new testdata();
        td.MissionName = jd[0]["name"].ToString();
        td.tag = jd[0]["Tag"].ToString();
        print(td);
    }
    public void TestSaveJson() {
        JsonData jd = new JsonData();
        jd.SetJsonType(JsonType.Array);
        JsonData item = new JsonData();
        item["name"] = "任务1";
        item["Tag"] = "学习";
        jd.Add(item);
        using (StreamWriter sw = new StreamWriter("C:/Users/Trance/Documents/Github/TimeBlocks/TimeBlocks/Assets/Resources/testData.json"))
        {
            sw.Write(JsonMapper.ToJson(jd));
        }
    }
}
