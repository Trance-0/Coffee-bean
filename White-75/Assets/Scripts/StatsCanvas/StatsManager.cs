using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XCharts;

public class StatsManager : MonoBehaviour
{
    public BarChart ConcentrationTime;
    public BarChart ConcentrationTimeDistribution;
    public Text concentrationTimeSum;
    public Text taskFinishedCount;
    public Text taskFailedCount;
    public Text joinTime;
    public Text[] taskFailedReasons;
    public Text longestConcentrationTime;

    public DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("LateInit", 1f); 
    }
    public void LateInit() {
        LoadConcentrationTime(dataManager.concentrationTime);
        LoadConcentrationTimeDistribution(dataManager.concentrationTimeDistribution);
        concentrationTimeSum.text = dataManager.concentrationTimeSum.ToString() + " min";
        taskFailedCount.text = dataManager.taskFailedCount.ToString();
        taskFinishedCount.text = dataManager.taskFinishedCount.ToString();
        joinTime.text = (DateTime.Now - dataManager.joinTime).TotalDays.ToString() + " days";
        for (int i = 0; i < 5; i++)
        {
            taskFailedReasons[i].text = dataManager.taskFailedReasons[i];
        }
        longestConcentrationTime.text = dataManager.longestConcentrationTime.ToString();
    }

    private void LoadConcentrationTime(double[] OCT)
    {
        ConcentrationTime.ClearData();
        ConcentrationTime.AddXAxisData("Total");
        ConcentrationTime.AddData("OCT", OCT[6]);
        ConcentrationTime.AddXAxisData("Year");
        ConcentrationTime.AddData("OCT", OCT[5]);
        ConcentrationTime.AddXAxisData("Season");
        ConcentrationTime.AddData("OCT", OCT[4]);
        ConcentrationTime.AddXAxisData("Month");
        ConcentrationTime.AddData("OCT", OCT[3]);
        ConcentrationTime.AddXAxisData("Week");
        ConcentrationTime.AddData("OCT", OCT[2]);
        ConcentrationTime.AddXAxisData("3days");
        ConcentrationTime.AddData("OCT", OCT[1]);
        ConcentrationTime.AddXAxisData("Today");
        ConcentrationTime.AddData("OCT", OCT[0]);
    }
    private void LoadConcentrationTimeDistribution(double[] OCT)
    {
        ConcentrationTimeDistribution.ClearData();
        ConcentrationTimeDistribution.AddXAxisData("0-2");
        ConcentrationTimeDistribution.AddData("OCT", OCT[11]);
        ConcentrationTimeDistribution.AddXAxisData("2-4");
        ConcentrationTimeDistribution.AddData("OCT", OCT[10]);
        ConcentrationTimeDistribution.AddXAxisData("4-6");
        ConcentrationTimeDistribution.AddData("OCT", OCT[9]);
        ConcentrationTimeDistribution.AddXAxisData("6-8");
        ConcentrationTimeDistribution.AddData("OCT", OCT[8]);
        ConcentrationTimeDistribution.AddXAxisData("8-10");
        ConcentrationTimeDistribution.AddData("OCT", OCT[7]);
        ConcentrationTimeDistribution.AddXAxisData("10-12");
        ConcentrationTimeDistribution.AddData("OCT", OCT[6]);
        ConcentrationTimeDistribution.AddXAxisData("12-14");
        ConcentrationTimeDistribution.AddData("OCT", OCT[5]);
        ConcentrationTimeDistribution.AddXAxisData("14-16");
        ConcentrationTimeDistribution.AddData("OCT", OCT[4]);
        ConcentrationTimeDistribution.AddXAxisData("16-18");
        ConcentrationTimeDistribution.AddData("OCT", OCT[3]);
        ConcentrationTimeDistribution.AddXAxisData("18-20");
        ConcentrationTimeDistribution.AddData("OCT", OCT[2]);
        ConcentrationTimeDistribution.AddXAxisData("20-22");
        ConcentrationTimeDistribution.AddData("OCT", OCT[1]);
        ConcentrationTimeDistribution.AddXAxisData("22-24");
        ConcentrationTimeDistribution.AddData("OCT", OCT[0]);
    }

    private string GetTime(long joinTime)
    {
        DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        DateTime dt = startTime.AddSeconds(joinTime);
        DateTime tt = DateTime.Today;
        TimeSpan deltat = tt - dt;
       return deltat.Days.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
