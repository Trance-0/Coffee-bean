using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewTaskWindowManager : MonoBehaviour
{
    public BlockChain blockChain;
    public GameObject SimpleWindow;
    public TimeBlock newTimeBlock;
    public GameObject AdvancedWindow;
    public InputField TaskNameS;
    public InputField TaskNameA;
    public Dropdown Year;
    public Dropdown Month;
    public Dropdown Day;
    public Dropdown Chunk;
    public Dropdown Tags;
    public InputField EstimateTime;

    public DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        
        DateTime today = DateTime.Today;
        Year.options.Clear();
        for (int i=0;i<10;i++) {
            Year.options.Add(new Dropdown.OptionData( (today.Year+i).ToString()));
        }
        Month.options.Clear();
        for (int i = 0; i < 12; i++)
        {
            Month.options.Add(new Dropdown.OptionData(((today.Month + i)%12+1).ToString()));
        }
        Day.options.Clear();
        for (int i = 0; i < 31; i++)
        {
            Day.options.Add(new Dropdown.OptionData(((today.Day + i) % 31+1).ToString()));
        }
        Chunk.options.Clear();
        Chunk.options.Add(new Dropdown.OptionData("Morning"));
        Chunk.options.Add(new Dropdown.OptionData("Afternoon"));
        Chunk.options.Add(new Dropdown.OptionData("Evening"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void BuildBlock() {
        string taskname;
        if (dataManager.isAdvanced) {
            taskname = TaskNameA.text;
        }
        else {
            taskname = TaskNameS.text;
        }
        int chunkid=0;
        if (Chunk.itemText.text == "Morning") {
            chunkid = 0;
        }
        else if (Chunk.itemText.text == "Afternoon")
        {
            chunkid = 1;
        }
        else if (Chunk.itemText.text == "Evening")
        {
            chunkid = 2;
        }
        newTimeBlock=new TimeBlock(taskname, int.Parse(Year.options[Year.value].text), int.Parse(Month.options[Month.value].text), int.Parse(Day.options[Day.value].text), chunkid, int.Parse(EstimateTime.text),Tags.itemText.text);
        dataManager.lastInput = newTimeBlock;
    }
    public void Save() {
       // BuildBlock();
        blockChain.AddBlock(newTimeBlock);
        blockChain.ShowBlockChain();
    }
    public void CloseWindow() {
        dataManager.isAddNewTaskWindowAwake = false;
        if (dataManager.isAdvanced)
        {
            AdvancedWindow.SetActive(false);
        }
        else
        {
            SimpleWindow.SetActive(false);
        }
    }
    public void OpenWindow() {
        dataManager.isAddNewTaskWindowAwake = true;
        if (dataManager.isAdvanced)
        {
            AdvancedWindow.SetActive(true);
            TaskNameA.text = dataManager.lastInput.Name();
        }
        else
        {
            SimpleWindow.SetActive(true);
            TaskNameS.text = dataManager.lastInput.Name();
        }
    }
    public void SetAdvancedModeOff() {
        //BuildBlock();
        CloseWindow();
        dataManager.isAdvanced = false;
        OpenWindow();
    }
    public void SetAdvancedModeOn()
    {
        //BuildBlock();
        CloseWindow();
        dataManager.isAdvanced = true;
        OpenWindow();
    }
}
