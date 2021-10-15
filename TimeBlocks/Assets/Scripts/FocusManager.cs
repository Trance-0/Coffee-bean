using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FocusManager: MonoBehaviour
{
    //global variables
   public DataManager dataManager;
    //local variables
    public bool onFocus = false;

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