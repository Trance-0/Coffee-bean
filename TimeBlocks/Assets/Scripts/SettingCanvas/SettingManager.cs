using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Setting : MonoBehaviour
{
    public DataManager dataManager;

    public Toggle completionCheckToggle;
    public void CompletionCheckUpdate()
    {
        dataManager.completionCheck = completionCheckToggle.isOn;
    }
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
    }
    public void OCTUpdate()
    {
        dataManager.OCT = int.Parse(OCTValue.text);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
