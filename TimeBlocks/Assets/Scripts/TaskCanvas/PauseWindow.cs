using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : MonoBehaviour
{
    public TaskOperatingContoler taskOperatingContoler;
    public GameObject pause;
    public GameObject restart;
    public GameObject shade;

    public Text timer;
    public InputField timeRemain;
    public DateTime restStartTime;
    public int defaultEstimateTime;

    public DateTime origin;

    public float restTime;

    public bool isOnRest;
    // Start is called before the first frame update
    void Start()
    {
        pause.SetActive(false);
        shade.SetActive(false);
        restart.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnRest) {
            restTime += Time.deltaTime;
            TimeSpan toDisplay = DateTime.Now.Subtract(origin);
            timer.text = toDisplay.Duration().TotalMinutes.ToString() + ":" + toDisplay.Duration().Seconds.ToString();
        }
    }
    public void Wake(int d) {
        defaultEstimateTime = d;
        restStartTime = DateTime.Now;
        pause.SetActive(true);
        restart.SetActive(false);
        shade.SetActive(true);
        isOnRest = true;
        origin = DateTime.Now;
    }
    public void Restart() {
        pause.SetActive(false);
        restart.SetActive(true);
        shade.SetActive(true);
    }
    public void ReturnToMenu() {
        if (timeRemain.text.Length==0) {
            taskOperatingContoler.ResetTask(defaultEstimateTime);
        }
        
        else {
            taskOperatingContoler.ResetTask(int.Parse(timeRemain.text));
        }
        isOnRest = false;
    }
    public void ConinuteTask() {
        if (timeRemain.text.Length == 0) {
            taskOperatingContoler.ContinueTask(defaultEstimateTime);
        }
        else {
            taskOperatingContoler.ContinueTask(int.Parse(timeRemain.text));
        }
        isOnRest = false;
    }
}
