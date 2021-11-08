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
    public Text concentrationTimeTimer;
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
        if (focusManager.onFocus)
        {
            isCounting = true;
        }
        if (isCounting)
        {

            //if (concentrationTime > dataManager.OCTMax * 2)
            //{
            //    Debug.Log("Your concentration time have exceed the time limit, record abolished.");
            //    errorWindow.Warning("Your concentration time have exceed the time limit, record abolished.");
            //    isCounting = false;
            //}
            //if (toDo.ConvertDeadlineToDateTime().Subtract(DateTime.Now).TotalSeconds < 0)
            //{
            //    Debug.Log("You have missed your deadline.");
            //    errorWindow.Warning("You have missed your deadline.");
            //    isCounting = false;
            //}
            //if (concentrationTime > dataManager.manualOCT)
            //{
            //    Debug.Log("Your concentration time have passed your goal, time to have some breaks to maintain high productivity. Of course, you can continue your task if you wish.");
            //    errorWindow.Warning("Your concentration time have passed your goal, time to have some breaks to maintain high productivity. Of course, you can continue your task if you wish.");
            //}
            TimeShow();
           
            if (!focusManager.onFocus)
            {
                NotificationManager.SendNotification(Application.productName, "Interruption detected, return to app to continue your task.");
                isCounting = false;
            }
        }
    }
    // Set all the local variables
    public void SendTask(TimeBlock task) {
        toDo = task;
        taskName.text = task._name;
        icon.sprite = configManager.imageReference[dataManager.tags[task._tagId]._imageId];
        isCounting = true;
        origin = DateTime.Now.AddMinutes(toDo._estimateTime);
    }
    //count down origin minus estimate time, always count forward
    private void TimeShow() {
       concentrationTime += Time.deltaTime;
       TimeSpan toDisplay = DateTime.Now.Subtract(origin);
        concentrationTimeTimer.text = string.Format("Concentrationtime: {0}", concentrationTime.ToString());
        timer.text = string.Format("{0}:{1}",Math.Floor(toDisplay.Duration().TotalMinutes).ToString(),toDisplay.Duration().Seconds.ToString());
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
        dataManager.AddBlock(toDo);
        dataManager.ConcentrationTimeUpDate(OCTCal());
        pauseWindow.CloseWindow();
    }
    //Continue operationg the task with new estimate time
    public void ContinueTask(int newEstimateTime) {
        toDo._estimateTime = newEstimateTime;
        SendTask(toDo);
        pauseWindow.CloseWindow();
    }
    //convert (float)concentration time to (double)
    private double OCTCal() {
        return Convert.ToDouble(concentrationTime)/60.0;
    }
    //Mark task as finished, update OCT records
    public void FinishTask() {
        dataManager.taskFinishedCount++;
        dataManager.ConcentrationTimeUpDate(OCTCal());
        pauseWindow.CloseWindow();
    }
    //If quit then reset the task
    private void OnApplicationQuit()
    {
        ResetTask(toDo._estimateTime-Mathf.FloorToInt(concentrationTime));
    }
}
