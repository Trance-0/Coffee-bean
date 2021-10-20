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
    public InputField H;
    public void ColorUpdate() {
       float h =float.Parse(H.text);
        //Debug.Log(r+""+ R.text + ""+g+""+G.text + ""+b+""+ B.text);
        if (h<255&&h>=0)
        {
            dataManager.backgroundColor = Color.HSVToRGB(h/255,0.5f,1f);
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

        OCTAutoUpdate();

        OCTValue.interactable = !dataManager.OCTAuto;
        OCTValue.text = dataManager.manualOCT.ToString() ;
        float nh, ns, nv;
        Color.RGBToHSV(dataManager.backgroundColor, out nh, out ns, out nv);
        H.text = (nh*255).ToString();

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
    //Remember to call this method when get out of setting canvas.
    public void UpdateData() {
        dataManager.SaveSettings();
        dataManager.SaveTags();
    }
    private void OnApplicationQuit()
    {
        UpdateData();
    }
}
