using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FocusManager: MonoBehaviour
{
    bool isPaused = false;

    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
        Debug.Log("Focused");
    }

    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
        Debug.Log("Not Focused");
    }

}