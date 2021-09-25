using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FocusManager: MonoBehaviour
{
    bool isPaused = false;
   public DataManager dataManager;

    void Start()
    {

    }

    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
        dataManager.AppUseSum +=1;
        Debug.Log("Focused");
    }

    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
        Debug.Log("Not Focused");
    }

}