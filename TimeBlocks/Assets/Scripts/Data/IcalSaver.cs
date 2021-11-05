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
        StreamReader a = LocalRead();
        
        Debug.Log(a.ReadLine());
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
    public StreamReader LocalRead() {
        try
        {
            StreamReader sr;
            if (File.Exists(dir))
            {
                FileStream fs = new FileStream(dir, FileMode.Open);
                sr = new StreamReader(fs);
                return sr;
            }
            return null;
        }
        catch (Exception e)
        {
            Debug.Log(e);
            return null;
        }
    }
}
