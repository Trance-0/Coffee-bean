using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataSynchronizeWindow : MonoBehaviour
{
   public DataManager dataManager;
    public SQLSaver sQLSaver;
    public IcalSaver icalSaver;
    public ErrorWindow errorWindow;
    public BufferWindow bufferWindow;

    public Text saveDateOnServer;
    public Text saveDateOnLocal;
    public GameObject background;
    public GameObject frame;
    // Start is called before the first frame update
    void Start()
    {
        background.SetActive(false);
        frame.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void WakeWindow() {
        background.SetActive(true);
        frame.SetActive(true);
        saveDateOnServer.text = sQLSaver.checkSaveTime().ToString();
        saveDateOnLocal.text = icalSaver.checkSaveTime().ToString();
    }
    public void ForceUpload() {
        try {
            bufferWindow.LoadBuffer("Connecting to the server",5f);
            icalSaver.LoadData(dataManager);
            sQLSaver.Push(dataManager);
            errorWindow.Warning("Data uploaded to server.");
        } catch (Exception e) {
            Debug.Log(e);
            errorWindow.Warning("Failed to upload local save, please check your connections or contact developer to check bugs.");
        }
        background.SetActive(false);
        frame.SetActive(false);
    }
    public void ForceDownload() {
        try
        {
            bufferWindow.LoadBuffer("Connecting to the server", 5f);
            sQLSaver.Pull(dataManager);
            icalSaver.SaveData(dataManager);
            errorWindow.Warning("Data downloaded to local.");
        }
        catch (Exception e)
        {
            Debug.Log(e);
            errorWindow.Warning("Failed to download local save, please check your connections or contact developer to check bugs.");
        }
        background.SetActive(false);
        frame.SetActive(false);
    }
}
