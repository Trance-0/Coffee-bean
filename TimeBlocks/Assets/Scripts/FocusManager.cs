
using UnityEngine;

public class FocusManager: MonoBehaviour
{
    bool isPaused = false;

    void OnGUI()
    {
        if (isPaused)
           Debug.Log("Game paused");
    }

    void OnApplicationFocus(bool hasFocus)
    {
        isPaused = !hasFocus;
    }

    void OnApplicationPause(bool pauseStatus)
    {
        isPaused = pauseStatus;
    }
}