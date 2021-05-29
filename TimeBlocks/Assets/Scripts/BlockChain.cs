using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockChain : MonoBehaviour
{
    public DataManager dataManager;
    public TimeBlockUI timeBlockPF;
    public GameObject blockChainUI;
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
        if (dataManager.sortingByTime)
        {
            PriorityQueue<TimeBlock> temp = dataManager.sortByTime.Clone();
            for (int i= 0;i<dataManager.chainSize;i++)
            {
                CreateANewBlock(temp.Dequeue());
            }
        }
        else {
            PriorityQueue<TimeBlock> temp = dataManager.sortByPriority.Clone();
            for (int i = 0; i < dataManager.chainSize; i++)
            {
                CreateANewBlock(temp.Dequeue());
            }
        }
    }
    public bool AddBlock(TimeBlock a)
    {
        dataManager.sortByTime.Enqueue(a);
        dataManager.sortByPriority.Enqueue(a);
        dataManager.chainSize++;
        Debug.Log("Chainsize" + dataManager.chainSize);
        Debug.Log("Realsize" + dataManager.sortByPriority.Count());
        return true;
    }
    

    public void CreateANewBlock(TimeBlock i)
    {
        TimeBlockUI newBlock = Instantiate(timeBlockPF, blockChainUI.transform.position, Quaternion.identity);
        newBlock.gameObject.transform.SetParent(blockChainUI.transform);
        //Tag temp;
        //dataManager.tagDictionary.TryGetValue(i.Tag(),out temp);
        //newBlock.icon = temp._image;
        newBlock.taskName.text = i._name;
        newBlock.icon.color = new Color(1,1,1,1);
    }
}
