using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class DataSaver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SaveConfig(DataManager dm,string jsonName) {
        JsonData jd = new JsonData();
        jd.SetJsonType(JsonType.Array);
        JsonData item = new JsonData();
        item["analyseOptimalConcentrationTime"] = dm.analyseOptimalConcentrationTime;

    }
    public void SaveBlocks(List<TimeBlock> blocks,string jsonName) {
        JsonData jd = new JsonData();
        jd.SetJsonType(JsonType.Array);
        foreach (TimeBlock i in blocks) {
            JsonData item = new JsonData();
            item["_name"] = i._name;
            item["_year"] = i._year;
            item["_month"] = i._month;
            item["_day"] = i._day;
            item["_chunk"] = i._chunk;
            item["_timeRequired"] = i._timeRequired;
            item["_tag"] = i._tag;
            item["_priority"] = i._priority;
            item["_isOver"] = i._isOver;
            item["_isFailed;"] = i._isFailed;
}
        using (StreamWriter sw = new StreamWriter("C:/Users/Trance/Documents/Github/TimeBlocks/TimeBlocks/Assets/Resources/"+jsonName+".json"))
        {
            sw.Write(JsonMapper.ToJson(jd));
        }
    }
    public void LoadBlocks(List<TimeBlock> blocks, string jsonName)
    {
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
