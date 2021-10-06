using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FocusManager: MonoBehaviour
{
    public bool onFocus = false;
   public DataManager dataManager;

    void Start()
    {

    }

    void OnApplicationFocus(bool hasFocus)
    {
        onFocus = !hasFocus;
        dataManager.AppUseSum +=1;
        Debug.Log("Focused");
    }

    void OnApplicationPause(bool pauseStatus)
    {
        onFocus = pauseStatus;
        Debug.Log("Not Focused");
    }

}