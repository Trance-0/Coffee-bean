using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockChain : MonoBehaviour
{
    public DataManager dataManager;
    public TimeBlockUI timeBlockPF;
    public GameObject blockChainUI;
    public ConfigManager configManager;
    public TaskOperatingContoler taskOperatingContoler;
    public CanvasManager canvasManager;
    public BlockChain blockChain;

    public class timeSort : Comparer<TimeBlock>
    {
        // Compares by Length, Height, and Width.
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
        // Compares by Length, Height, and Width.
        public override int Compare(TimeBlock x, TimeBlock y)
        {
            
            if (x.GetPriority(dataManager.tagDictionary) - y.GetPriority(dataManager.tagDictionary) > 0)
            {
                return 1;
            }
            if (x.GetPriority(dataManager.tagDictionary) - y.GetPriority(dataManager.tagDictionary) < 0)
            {
                return -1;
            }
            return 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowBlockChain();
    }

    // Update is called once per frame
    void Update()
    {
        

    }
    public void ShowBlockChain()
    {
        for (int i = blockChainUI.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(blockChainUI.transform.GetChild(i).gameObject);
        }
            
            if (configManager.sortingByTime)
            {
                dataManager.blocks.Sort(new timeSort());
                for (int i = 0; i < dataManager.blocks.Count; i++)
                {
                    CreateANewBlock(dataManager.blocks[i]);
                Debug.Log("Task name: " + dataManager.blocks[i]._name + " Deadline:" + dataManager.blocks[i]._deadline);
            }
            }
            else
            {
                prioritySort ps = new prioritySort();
                ps.dataManager = dataManager;
                dataManager.blocks.Sort(ps);
                for (int i = 0; i < dataManager.blocks.Count; i++)
                {
                    CreateANewBlock(dataManager.blocks[i]);
                Debug.Log("Task name: "+dataManager.blocks[i]._name+" Priority:"+dataManager.blocks[i].GetPriority(dataManager.tagDictionary));
                }
            }
    }
    public void Search(string keywords) {
        for (int i = blockChainUI.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(blockChainUI.transform.GetChild(i).gameObject);
        }
        
            for (int i = 0; i < dataManager.blocks.Count; i++)
            {
            if (dataManager.blocks[i]._name.Contains(keywords)) {
                CreateANewBlock(dataManager.blocks[i]);
            }
            }
    }
    public bool AddBlock(TimeBlock a)
    {
        dataManager.blocks.Add(a);
        dataManager.SaveBlock(a);
        return true;
    }
   
    public void CreateANewBlock(TimeBlock i)
    {
        TimeBlockUI newBlock = Instantiate(timeBlockPF, blockChainUI.transform.position, Quaternion.identity);
        newBlock.gameObject.transform.SetParent(blockChainUI.transform);
        newBlock.timeBlock = i;
        newBlock.taskName.text = i._name;
        if (newBlock.timeBlock._tagId==-1) {
            newBlock.timeBlock._tagId = dataManager.defaultTagId;
        }
        int imageId = dataManager.tagDictionary[newBlock.timeBlock._tagId]._imageId;
        Debug.Log(imageId);
        newBlock.icon.sprite = configManager.imageReference[imageId];
        newBlock.taskOperatingContoler = taskOperatingContoler;
        newBlock.dataManager = dataManager;
        newBlock.canvasManager = canvasManager;
        newBlock.blockChain = blockChain;
    }
}
