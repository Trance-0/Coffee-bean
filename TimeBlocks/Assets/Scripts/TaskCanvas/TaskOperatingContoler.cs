using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 * When screen is on, the timer would not on
 * When screen is closed, the timer would count down
 * When screen is reopend, the timer would refreshed
 * if no notes were added, the OCT would be recorded and mission failed.
 * if notes were added, they would be added to main screen
 * 
 * When timer is over, notify, OCT = final time.
 * When timer is disabled, the OCT would recorded till the next time open phone.
 * 
 * 
 * if finished, the task would be finished.
 * if paused, the task would be returned to the list again.
 */ 

public class TaskOperatingContoler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
