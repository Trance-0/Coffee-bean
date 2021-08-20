using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockChain : MonoBehaviour
{
    public DataManager dataManager;
    public TimeBlockUI timeBlockPF;
    public GameObject blockChainUI;

    public class timeSort : Comparer<TimeBlock>
    {
        // Compares by Length, Height, and Width.
        public override int Compare(TimeBlock x, TimeBlock y)
        {
            if (x._timeStamp - y._timeStamp > 0)
            {
                return 1;
            }
            if (x._timeStamp - y._timeStamp < 0)
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
          //  Destroy(blockChainUI.transform.GetChild(i).gameObject);
        }
        if (dataManager.chainSize != 0)
        {
            
            if (dataManager.sortingByTime)
            {
                dataManager.blocks.Sort(new timeSort());
                for (int i = 0; i < dataManager.chainSize; i++)
                {
                    CreateANewBlock(dataManager.blocks[i]);
                }
            }
            else
            {
                prioritySort ps = new prioritySort();
                ps.dataManager = dataManager;
                dataManager.blocks.Sort(ps);
                for (int i = 0; i < dataManager.chainSize; i++)
                {
                    CreateANewBlock(dataManager.blocks[i]);
                }
            }
        }
    }
    public bool AddBlock(TimeBlock a)
    {
        dataManager.blocks.Add(a);
        dataManager.chainSize++;
        return true;
    }
   
    public void CreateANewBlock(TimeBlock i)
    {
        TimeBlockUI newBlock = Instantiate(timeBlockPF, blockChainUI.transform.position, Quaternion.identity);
        newBlock.gameObject.transform.SetParent(blockChainUI.transform);

        newBlock.taskName.text = i._name;
        newBlock.icon.sprite = dataManager.tagDictionary[newBlock.timeBlock._tagId]._image;
        newBlock.icon.color = new Color(1, 1, 1, 1);
        newBlock.timeBlock = i;
        newBlock.blockChain = this;
    }
}
