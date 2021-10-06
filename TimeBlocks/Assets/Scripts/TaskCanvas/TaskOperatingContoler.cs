using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Image icon;
    public Text taskName;
    public Text timer;

    public InputField newEstimateTime;

    public TimeBlock toDo;

    public ConfigManager configManager;
    public DataManager dataManager;
    public FocusManager focusManager;
    public ErrorWindow errorWindow;
    public PauseWindow pauseWindow;

    public float concentrationTime;
    public int interruptionTime;
    public bool isCounting;
    public DateTime origin;
    public bool countForward;
    public bool taskOperating;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (focusManager.onFocus)
        {
            if (concentrationTime>dataManager.OCTMax*2) {
                errorWindow.Warning("Your concentration time have exceed the time limit, record abolished.");
                isCounting = false ;
            }
            if (DateTime.Now.Subtract(toDo.ConvertDeadlineToDateTime()).TotalSeconds<0) {
                errorWindow.Warning("You have missed your deadline.");
                isCounting = false;
            }
            if (concentrationTime>dataManager.manualOCT) {
                errorWindow.Warning("Your concentration time have passed your goal, time to have some breaks to maintain high productivity. Of course, you can continue your task if you wish.");
            }
            TimeShow();
        }
        else {
            NotificationManager.SendNotification(configManager.appName,"Interruption detected, return to app to continue your task.");
            interruptionTime++;
            isCounting = false;
        }
    }
    public void SendTask(TimeBlock task) {
        toDo = task;
        taskOperating = true;
        if (toDo._estimateTime < 0)
        {
            countForward = true;
            origin = DateTime.Now;
        }
        else {
            countForward = false;
            origin = DateTime.Now.AddMinutes(toDo._estimateTime);
        }
    }

    private void TimeShow() {
        if (dataManager.enableTimer)
        {
            if (isCounting)
            {
                concentrationTime += Time.deltaTime;
                TimeSpan toDisplay = DateTime.Now.Subtract(origin);
                if (!countForward&&toDisplay.Ticks<0)
                {
                    countForward = true;
                }
                timer.text = toDisplay.Duration().TotalMinutes.ToString() +":"+toDisplay.Duration().Seconds.ToString();
            }
        }
        else {
            timer.text = "";
        }
    }
    public void PauseTask() {
        pauseWindow.Wake();
        isCounting = false;
    }
    public void ResetTask(int newEstimateTime) {
        toDo._estimateTime = newEstimateTime;
        dataManager.blocks.Add(toDo);
        dataManager.OCTUpDate(OCTCal());
    }
    public void ContinueTask(int newEstimateTime) {
    }
    private double OCTCal() {
        return Convert.ToDouble(concentrationTime) / 60.0;
    }
    public void FinishTask() {
        dataManager.taskSum++;
        dataManager.OCTUpDate(OCTCal());
    }
    private void OnApplicationQuit()
    {
        if (countForward) {
            ResetTask(-1);
        }
        else {
            ResetTask(toDo._estimateTime-Mathf.FloorToInt(concentrationTime));
        }
    }
}
