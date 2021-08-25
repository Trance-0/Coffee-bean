using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    public ConfigManager configManager;
    public GameObject imageChainUI;
    public GameObject framePF;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = imageChainUI.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(imageChainUI.transform.GetChild(i).gameObject);
        }
        for(int i= 0; i < configManager.imageReference.Count; i++)
        {
            CreateANewTag(i,configManager.imageReference[i]);
        }
    }

    private void CreateANewTag(int i,Sprite image)
    {
        GameObject newImage = Instantiate(framePF, imageChainUI.transform.position, Quaternion.identity);
        newImage.gameObject.transform.SetParent(imageChainUI.transform);
        newImage.GetComponent<Image>().sprite = image;
        newImage.GetComponent<Image>().color = configManager.imageColor;
        newImage.GetComponent<Icon>().iconId =i;
        newImage.GetComponent<Button>().interactable = false;
    }
    public void setGoal(TagUI k) {
        for (int i = imageChainUI.transform.childCount - 1; i >= 0; i--)
        {
            imageChainUI.transform.GetChild(i).GetComponent<Icon>().setGoal(k);
            imageChainUI.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }
    public void Reset()
    {
        for (int i = imageChainUI.transform.childCount - 1; i >= 0; i--)
        {
            imageChainUI.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
