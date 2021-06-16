using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockChain : MonoBehaviour
{
    public DataManager dm;
    public TimeBlockUI timeBlockPF;
    public GameObject blockChainUI;
    public class timeSort : Comparer<TimeBlock>
    {
        // Compares by Length, Height, and Width.
        public override int Compare(TimeBlock x, TimeBlock y)
        {
            return x.Compare(y);
        }
    }
    public class prioritySort : Comparer<TimeBlock>
    {
        // Compares by Length, Height, and Width.
        public override int Compare(TimeBlock x, TimeBlock y)
        {
            return x.Priority() - y.Priority();
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
        if (dm.chainSize != 0)
        {
            
            if (dm.sortingByTime)
            {
                dm.sortByTime.Sort(new timeSort());
                for (int i = 0; i < dm.chainSize; i++)
                {
                    CreateANewBlock(dm.sortByTime[i]);
                }
            }
            else
            {
                dm.sortByPriority.Sort(new prioritySort());
                for (int i = 0; i < dm.chainSize; i++)
                {
                    CreateANewBlock(dm.sortByPriority[i]);
                }
            }
        }
    }
    public bool AddBlock(TimeBlock a)
    {
        dm.sortByTime.Add(a);
        dm.sortByPriority.Add(a);
        dm.chainSize++;
        Debug.Log("Chainsize" + dm.chainSize);
        Debug.Log("Realsize" + dm.sortByPriority.Count);
        return true;
    }
    public void MarkAsFinished(TimeBlock i) {
        dm.sortByPriority.Remove(i);
        dm.sortByPriority.Remove(i);
        dm.chainSize--;
        dm.finishedTask.Add(i);
        ShowBlockChain();
    }
    public void DeleteBlock(TimeBlock i)
    {
        dm.sortByPriority.Remove(i);
        dm.sortByPriority.Remove(i);
        dm.chainSize--;
        dm.deletedTask.Add(i);
        ShowBlockChain();
    }

    public void CreateANewBlock(TimeBlock i)
    {
        TimeBlockUI newBlock = Instantiate(timeBlockPF, blockChainUI.transform.position, Quaternion.identity);
        newBlock.gameObject.transform.SetParent(blockChainUI.transform);
        //Tag temp;
        //dataManager.tagDictionary.TryGetValue(i.Tag(),out temp);
        //newBlock.icon = temp._image;

        newBlock.taskName.text = i._name;
        newBlock.icon.color = new Color(1, 1, 1, 1);
        newBlock.icon = dm.tagDictionary[newBlock.timeBlock._tag]._image;
        newBlock.timeBlock = i;
        newBlock.blockChain = this;
    }
}
