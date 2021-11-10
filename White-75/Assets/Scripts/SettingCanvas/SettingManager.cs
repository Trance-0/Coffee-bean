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

    public Slider hue;
    public void ColorUpdate() {
       dataManager.color =hue.value;
        dataManager.backgroundColor = Color.HSVToRGB(dataManager.color,0.8f,1f);
        //Debug.Log(r+""+ R.text + ""+g+""+G.text + ""+b+""+ B.text);
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
        hue.value = dataManager.color;
        defaultDeadline.text = dataManager.defaultDeadline.Days.ToString();
        LoadDefaultTagValue();
    }
    public void LoadDefaultTagValue() {
        tempDictionary = new Dictionary<string, int>();
        defaultTag.options.Clear();
        for (int i = 0; i < 7; i++)
        {
            if (tempDictionary.ContainsKey(dataManager.tags[i]._name))
            {
                dataManager.tags[i]._name += "(0)";

            }
            tempDictionary.Add(dataManager.tags[i]._name, i);
            defaultTag.options.Add(new Dropdown.OptionData(dataManager.tags[i]._name));
        }
        defaultTag.value = dataManager.defaultTagIndex;
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
