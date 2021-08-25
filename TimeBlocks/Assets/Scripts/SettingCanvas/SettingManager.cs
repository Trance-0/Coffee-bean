using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public DataManager dataManager;
    public ErrorWindow errorWindow;
    public Toggle enableTimerToggle;
    public void enableTimerUpdate()
    {
        dataManager.enableTimer = enableTimerToggle.isOn;
    }
    public Toggle analyseOCTToggle;
    public void analyseOCTUpdate()
    {
        dataManager.analyseOCT = analyseOCTToggle.isOn;
    }
    public InputField OCTValue;
    public Toggle OCTAutoToggle;
    public void OCTAutoUpdate()
    {
        dataManager.OCTAuto = OCTAutoToggle.isOn;
        OCTValue.interactable = !dataManager.OCTAuto;
    }
    public void OCTUpdate()
    {
        dataManager.manualOCT = int.Parse(OCTValue.text);
    }
    public InputField R;
    public InputField G;
    public InputField B;
    public void ColorUpdate() {
       float r = float.Parse(R.text);
        float g = float.Parse(G.text);
        float b = float.Parse(B.text);
        //Debug.Log(r+""+ R.text + ""+g+""+G.text + ""+b+""+ B.text);
        if (r < 255&&r>0 && g < 255 &&g>0&& b < 255&&b>0)
        {
            dataManager.backgroundColor = new Color(r / 255, g / 255, b / 255);
        }
        else
        {
            errorWindow.Warning("R, G, B value should be integer below 255 and greater than 0");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enableTimerToggle.isOn = dataManager.enableTimer;
        analyseOCTToggle.isOn = dataManager.analyseOCT;

        OCTValue.interactable = !dataManager.OCTAuto;
        OCTValue.text = dataManager.manualOCT.ToString() ;

        R.text = (dataManager.backgroundColor.r*255).ToString();
        G.text = (dataManager.backgroundColor.g * 255).ToString();
        B.text = (dataManager.backgroundColor.b * 255).ToString();

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
    public void UpdateData() {
        dataManager.Save();
    }
}
