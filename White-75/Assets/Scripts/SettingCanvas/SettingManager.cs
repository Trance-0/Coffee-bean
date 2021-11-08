using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public DataManager dataManager;
    public ErrorWindow errorWindow;

    public DataSynchronizeWindow dataSynchronizeWindow;

    public InputField hue;
    public void ColorUpdate() {
       float h =float.Parse(hue.text);
        //Debug.Log(r+""+ R.text + ""+g+""+G.text + ""+b+""+ B.text);
        if (h<255&&h>=0)
        {
            dataManager.color = h;
        }
        else
        {
            errorWindow.Warning("Hue value should be integer below 255 and greater than 0");
        }
    }

    public InputField defaultDeadline;
    public void defaultDeadlineUpdate() {
        int deadlineOffset = int.Parse(defaultDeadline.text);
        //Debug.Log(r+""+ R.text + ""+g+""+G.text + ""+b+""+ B.text);
        if (deadlineOffset>=0)
        {
            dataManager.defaultDeadline = new TimeSpan(deadlineOffset,0,0,0);
        }
        else
        {
            errorWindow.Warning("Default deadline value should be positive integer");
        }
    }

    public Dropdown defaultTag;
    public Dictionary<string, int> tempDictionary;
    public void defaltTagUpdate() {
       dataManager.defaultTagIndex= tempDictionary[defaultTag.options[defaultTag.value].text];
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LateInit",0.1f);
    }
    public void LateInit() {
        float nh, ns, nv;
        Color.RGBToHSV(dataManager.backgroundColor, out nh, out ns, out nv);
        hue.text = (nh * 255).ToString();
        defaultDeadline.text = dataManager.defaultDeadline.Days.ToString();
        tempDictionary = new Dictionary<string, int>();
        defaultTag.options.Clear();
        for (int i = 0; i < 7; i++)
        {
            tempDictionary.Add(dataManager.tags[i]._name, i);
            defaultTag.options.Add(new Dropdown.OptionData(dataManager.tags[i]._name));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Logout() {
        throw new NotImplementedException();
    }
    public void ClearData()
    {
        dataManager.InitializeData();
    }
}
