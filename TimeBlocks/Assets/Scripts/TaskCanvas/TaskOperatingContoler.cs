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
    //local configuations
    public Image icon;
    public Text taskName;
    public Text timer;
    //core data
    public TimeBlock toDo;
    //global configurations
    public ConfigManager configManager;
    public DataManager dataManager;
    public FocusManager focusManager;
    public ErrorWindow errorWindow;
    public PauseWindow pauseWindow;
    //local variables
    public float concentrationTime;
    public int interruptionTime;
    public bool isCounting;
    public DateTime origin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*if 
         * time limit exceed
         * deadline already passed
         * then 
         * send warning
         * else
         * update timer
         * 
         * if
         * being interrupt
         * then
         * send notification
         * 
         */
        if (isCounting)
        {
            if (concentrationTime > dataManager.OCTMax * 2)
            {
                errorWindow.Warning("Your concentration time have exceed the time limit, record abolished.");
                isCounting = false;
            }
            if (DateTime.Now.Subtract(toDo.ConvertDeadlineToDateTime()).TotalSeconds < 0)
            {
                errorWindow.Warning("You have missed your deadline.");
                isCounting = false;
            }
            if (concentrationTime > dataManager.manualOCT)
            {
                errorWindow.Warning("Your concentration time have passed your goal, time to have some breaks to maintain high productivity. Of course, you can continue your task if you wish.");
            }
            TimeShow();
            if (!focusManager.onFocus)
            {
                NotificationManager.SendNotification(configManager.appName, "Interruption detected, return to app to continue your task.");
                interruptionTime++;
                isCounting = false;
            }
        }
    }
    // Set all the local variables
    public void SendTask(TimeBlock task) {
        toDo = task;
        isCounting = true;
            origin = DateTime.Now.AddMinutes(toDo._estimateTime);
    }
    //count down origin minus estimate time, always count forward
    private void TimeShow() {
        if (dataManager.enableTimer)
        {
            if (isCounting)
            {
                concentrationTime += Time.deltaTime;
                TimeSpan toDisplay = DateTime.Now.Subtract(origin);
                timer.text = toDisplay.Duration().TotalMinutes.ToString() +":"+toDisplay.Duration().Seconds.ToString();
            }
        }
        else {
            timer.text = "";
        }
    }
    //Wake up pause window and stop counting time
    public void PauseTask() {
        TimeSpan delta = DateTime.Now - origin;
        pauseWindow.Wake(Convert.ToInt32(delta.TotalMinutes));
        isCounting = false;
    }
    //Stop operating the task and register the original task with a new estimate time, update OCT records
    public void ResetTask(int newEstimateTime) {
        toDo._estimateTime = newEstimateTime;
        dataManager.blocks.Add(toDo);
        dataManager.OCTUpDate(OCTCal());
    }
    //Continue operationg the task with new estimate time
    public void ContinueTask(int newEstimateTime) {
        toDo._estimateTime = newEstimateTime;
        SendTask(toDo);
    }
    //convert (float)concentration time to (double)
    private double OCTCal() {
        return Convert.ToDouble(concentrationTime) / 60.0;
    }
    //Mark task as finished, update OCT records
    public void FinishTask() {
        dataManager.taskSum++;
        dataManager.OCTUpDate(OCTCal());
    }
    //If quit then reset the task
    private void OnApplicationQuit()
    {
            ResetTask(toDo._estimateTime-Mathf.FloorToInt(concentrationTime));
    }
}
