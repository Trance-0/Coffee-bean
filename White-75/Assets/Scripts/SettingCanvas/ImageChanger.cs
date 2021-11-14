using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    public ConfigManager configManager;
    public GameObject imageChainUI;
    public Icon framePF;

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
        Icon newImage = Instantiate(framePF, imageChainUI.transform.position, Quaternion.identity);
        newImage.gameObject.transform.SetParent(imageChainUI.transform);
        newImage.gameObject.transform.localScale = new Vector3(1f,1f,1f);
        newImage.image.sprite = image;
        newImage.image.color = configManager.imageColor;
        newImage.iconId =i;
        newImage.button.interactable = false;
    }
    //remember, the tag cannot change icon by themselves, so I use TagUI, don't change it if you don't know what you are doing.
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
