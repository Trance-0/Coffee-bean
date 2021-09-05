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
    public GameObject pauseWindow;

    public InputField newEstimateTime;

    public TimeBlock toDo;

    public ConfigManager configManager;
    public DataManager dataManager;

    public DateTime timeToFinish;
    public DateTime startTime;
    public DateTime pauseTime;
    public bool countForward;
    public int concentrationTime;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (dataManager.enableTimer) {
            DateTime now = DateTime.Now;
            if (countForward) {
                TimeSpan span = now - timeToFinish;
                timer.text = span.Minutes+":"+span.Seconds;
            }
            else {
                TimeSpan span =timeToFinish-now;
                timer.text = span.Minutes + ":" + span.Seconds;
                if (span.TotalSeconds==0) {
                    countForward = true;
                }
            }
        }
    }
    public void SendTask(TimeBlock input) {
        toDo = input;
        dataManager.blocks.Remove(input);
        dataManager.SaveBlocks();
        icon.sprite = configManager.imageReference[dataManager.tagDictionary[toDo._tagId]._imageId];
        taskName.text = toDo._name;
        if (dataManager.enableTimer)
        {
            SetTimer(toDo._estimateTime);
        }
        else {
            timer.text = "";
        }
    }

   public void SetTimer(int estimateTime)
    {
        if (estimateTime == -1)
        {
            countForward = true;

        }
        else
        {
            countForward = false;
           
        }
        DateTime now = DateTime.Now;
        timeToFinish = now.AddMinutes(estimateTime);
        startTime = now;
    }

   public void Pause() {
        pauseWindow.SetActive(true);
        pauseTime = DateTime.Now;
        concentrationTime += Convert.ToInt32((DateTime.Now - startTime).TotalMinutes);
   }
   public void Finished() {
        concentrationTime += Convert.ToInt32((DateTime.Now - startTime).TotalMinutes);
        dataManager.taskSum += 1;
        dataManager.OCTUpDate(concentrationTime);
        dataManager.SaveOCT();
    }
   public void ReturnTaskToList() {
        if (newEstimateTime.text=="")
        {
            if (countForward)
            {
                toDo._estimateTime = -1;
            }
            else
            {
                toDo._estimateTime = Convert.ToInt32((timeToFinish-pauseTime).TotalMinutes) ;
            }
        }
        else {
            toDo._estimateTime = int.Parse(newEstimateTime.text);
        }
        dataManager.blocks.Add(toDo);
        Finished();
        pauseWindow.SetActive(false);
    }
    public void RenewTask() {
        if (newEstimateTime.text == "")
        {
            if (countForward)
            {
                toDo._estimateTime = -1;
            }
            else
            {
                toDo._estimateTime = Convert.ToInt32((timeToFinish - pauseTime).TotalMinutes);
            }
        }
        else
        {
            toDo._estimateTime = int.Parse(newEstimateTime.text);
        }
        pauseWindow.SetActive(false);
        startTime = DateTime.Now;
        SetTimer(toDo._estimateTime);
    }
}
