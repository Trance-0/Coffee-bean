using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XCharts;

public class StatsManager : MonoBehaviour
{
    public BarChart OCTChart;
    public Text OCTSum;
    public Text taskSum;
    public Text interruptSum;
    public Text OCTMax;
    public Text AppUseSum;
    public Text joinTime;

    public DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
         OCTSum.text=dataManager.OCTSum.ToString()+" mins";
        taskSum.text = dataManager.taskSum.ToString();
        interruptSum.text = dataManager.interruptSum.ToString()+" times";
        OCTMax.text = dataManager.OCTMax.ToString()+" mins";
   AppUseSum.text=dataManager.AppUseSum.ToString()+" times";
    joinTime.text=GetTime(dataManager.joinTime)+" days";

        LoadOCT(dataManager.OCT);
}

    private void LoadOCT(List<int> OCT)
    {
        OCTChart.ClearData();
        OCTChart.AddXAxisData("Total");
        OCTChart.AddData("OCT", OCT[6]);
        OCTChart.AddXAxisData("Year");
        OCTChart.AddData("OCT", OCT[5]);
        OCTChart.AddXAxisData("Season");
        OCTChart.AddData("OCT", OCT[4]);
        OCTChart.AddXAxisData("Month");
        OCTChart.AddData("OCT", OCT[3]);
        OCTChart.AddXAxisData("Week");
        OCTChart.AddData("OCT", OCT[2]);
        OCTChart.AddXAxisData("3days");
        OCTChart.AddData("OCT", OCT[1]);
        OCTChart.AddXAxisData("Today");
        OCTChart.AddData("OCT", OCT[0]);
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
