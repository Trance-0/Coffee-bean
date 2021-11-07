using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

public class IcalSaver : MonoBehaviour
{
    public string dir;
    // Start is called before the first frame update
    void Start()
    {
#if UNITY_ANDROID
        dir = Application.persistentDataPath;
        //dir = "/storage/emulated/0/Android/data/com.Trance0.iCalendarApp/files";
        //dir = "/storage/emulated/0/Android/data/com.DefaultCompany.TimeBlocks/files";
#endif
#if UNITY_PC
        dir = Application.dataPath;
#endif
#if UNITY_EDITOR
        dir = Application.dataPath;
#endif
        dir = dir + "/test.ics";
        string[] data = { "Hahhahahhahhahh" };
        LocalWrite(data);
        List<string> a = LocalRead();
        Debug.Log(a);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool LoadData(DataManager dataManager) {
        return false;
    }
    public bool SaveData(DataManager dataManager)
    {
        return true;
    }
    public bool LocalWrite(string[] data) {
        try
        {
            StreamWriter sw;
            if (File.Exists(dir)) {
                File.Delete(dir);
            }
            sw=File.CreateText(dir);
            foreach (string i in data) {
                sw.WriteLine(i);
            }
            sw.Close();
            sw.Dispose();
             return true;
        }
        catch (Exception e) {
            Debug.Log(e);
            return false;
        }
    }
    public List<string> LocalRead() {
        try
        {
            StreamReader reader;
            List<string> results = new List<string>();
            Debug.Log("Connecting to local...");
            if (File.Exists(dir))
            {
                FileStream fs = new FileStream(dir, FileMode.Open);
                reader = new StreamReader(fs);
                string line;
                while ((line = reader.ReadLine()) != null) {
                    results.Add(line);
                }
                reader.Close();
                reader.Dispose();
                fs.Close();
                fs.Dispose();
            }
            Debug.Log("Success");
            return results;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }
}
