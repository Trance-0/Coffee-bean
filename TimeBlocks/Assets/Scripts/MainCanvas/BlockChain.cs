using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockChain : MonoBehaviour
{
    //global configurations
    public DataManager dataManager;
    public TimeBlockUI timeBlockPF;
    public GameObject blockChainUI;
    public ConfigManager configManager;
    public TaskOperatingContoler taskOperatingContoler;
    public CanvasManager canvasManager;
    public BlockChain blockChain;
    //local variables
    public class timeSort : Comparer<TimeBlock>
    {
        // Compares by deadline.
        public override int Compare(TimeBlock x, TimeBlock y)
        {
            if (x._deadline - y._deadline > 0)
            {
                return 1;
            }
            if (x._deadline - y._deadline < 0)
            {
                return -1;
            }
            return 0;
        }
    }
    public class prioritySort : Comparer<TimeBlock>
    {
        public DataManager dataManager;
        // Compares by priority * deadline.
        public override int Compare(TimeBlock x, TimeBlock y)
        {
            if (x.GetPriority(dataManager.tags) - y.GetPriority(dataManager.tags) > 0)
            {
                return -1;
            }
            if (x.GetPriority(dataManager.tags) - y.GetPriority(dataManager.tags) < 0)
            {
                return 1;
            }
            return 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //if the dictionary is not initialized, use the second one.

        //ShowBlockChain();
       // Invoke("ShowBlockChain",0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void ShowBlockChain()
    {
        dataManager.LoadData();
        //erase all the blocks on the Vertical layout
        for (int i = blockChainUI.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(blockChainUI.transform.GetChild(i).gameObject);
        }
            //Order by methods
            if (configManager.sortingByTime)
            {
                Array.Sort(dataManager.blocks,new timeSort());
                for (int i = 0; i < 7; i++)
                {
                if (dataManager.blocks[i]._name.CompareTo(dataManager.nullTask._name)!=0) {
                    CreateANewBlock(dataManager.blocks[i]);
                    Debug.Log(string.Format("Ordering by time, task name:{0},dealine:{1}", dataManager.blocks[i]._name, dataManager.blocks[i]._deadline));
                }
            }
            }
            else
            {
                prioritySort ps = new prioritySort();
                ps.dataManager = dataManager;
            Array.Sort(dataManager.blocks, ps);
            for (int i = 0; i < 7; i++)
                {
                    CreateANewBlock(dataManager.blocks[i]);
                Debug.Log(string.Format("Ordering by priority, task name:{0},priority:{1}", dataManager.blocks[i]._name,  dataManager.blocks[i].GetPriority(dataManager.tags)));
                }
            }
    }
    //Search task by keywords
    public void Search(string keywords) {
        //erase all the blocks in Vertical layout
        for (int i = blockChainUI.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(blockChainUI.transform.GetChild(i).gameObject);
        }
            for (int i = 0; i < 7; i++)
            {
            //remember to detect by lower case.
            if (dataManager.blocks[i]._name.ToLower().Contains(keywords.ToLower())) {
                CreateANewBlock(dataManager.blocks[i]);
            }
            }
    }
    //Add new block to the server
    public bool AddBlock(TimeBlock a)
    {
        dataManager.AddBlock(a);
        return true;
    }
   //instatiate block UI
    public void CreateANewBlock(TimeBlock i)
    {
        TimeBlockUI newBlock = Instantiate(timeBlockPF, blockChainUI.transform.position, Quaternion.identity);
        newBlock.gameObject.transform.SetParent(blockChainUI.transform);
        newBlock.timeBlock = i;
        newBlock.taskName.text = i._name;
        if (newBlock.timeBlock._tagId==-1) {
            newBlock.timeBlock._tagId = dataManager.defaultTagIndex;
        }
        Debug.Log(string.Format("Calling block image id for {0}, with id {1}",i._name,newBlock.timeBlock._tagId));
        int imageId = dataManager.tags[newBlock.timeBlock._tagId]._imageId;
        Debug.Log(string.Format("Image id: {0}",imageId));
        newBlock.icon.sprite = configManager.imageReference[imageId];
        newBlock.taskOperatingContoler = taskOperatingContoler;
        newBlock.dataManager = dataManager;
        newBlock.canvasManager = canvasManager;
        newBlock.blockChain = blockChain;
    }
}
