using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SortBy : MonoBehaviour
{
    public ConfigManager configManager;

    public BlockChain blockChain;
    public Button controlButton;
    public Sprite sortByTimeIcon;
    public Sprite sortByPriorityIcon;
    // Start is called before the first frame update
    void Start()
    {
        OnClick();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick() {
        if (!configManager.isAddNewTaskWindowAwake)
        {
            if (configManager.sortingByTime)
            {
                controlButton.image.sprite = sortByPriorityIcon;
                configManager.sortingByTime = false;
                blockChain.ShowBlockChain();
            }
            else
            {
                controlButton.image.sprite = sortByTimeIcon;
                configManager.sortingByTime = true;
                blockChain.ShowBlockChain();
            }
        }
    }
}
