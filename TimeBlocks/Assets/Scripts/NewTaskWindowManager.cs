using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewTaskWindowManager : MonoBehaviour
{
    public GameObject SimpleWindow;
    public TimeBlock newTimeBlock;
    public GameObject AdvancedWindow;
    public bool isAdvanced;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void buildBlock() {
        if (isAdvanced)
        {
            if (AdvancedWindow.) {
            }
        }
        else {
        }
    }
    public void OpenWindow() {
        if (isAdvanced) {
            AdvancedWindow.SetActive(true);
        }
        SimpleWindow.SetActive(true);
    }
    public void SetAdvancedModeOff() {
        isAdvanced = false;
        AdvancedWindow.SetActive(false);
    }
}
