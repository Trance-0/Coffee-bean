using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagChain : MonoBehaviour
{
    public GameObject tagChainUI;
    public TagUI tagPF;

    public ImageChanger imageChanger;
    public ConfigManager configManager;
    public DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart(1));
    }
    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        ShowTagChain();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShowTagChain()
    {
        for (int i = tagChainUI.transform.childCount - 1; i >= 0; i--)
        {
            Destroy(tagChainUI.transform.GetChild(i).gameObject);
        }
        foreach (KeyValuePair<int,Tag> i in dataManager.tagDictionary)
                {
                    CreateANewTag(i.Value);
                }
   }

    private void CreateANewTag(Tag tag)
    {
        TagUI newTag = Instantiate(tagPF, tagChainUI.transform.position, Quaternion.identity);
        newTag.gameObject.transform.SetParent(tagChainUI.transform);
        newTag.tag = tag;
        newTag.self = newTag;
        newTag.imageChange = imageChanger;
        newTag.dataManager = dataManager;
        newTag.configManager = configManager;
        newTag.tagNameInput.text= tag._name;
        newTag.weightInput.text = tag._power.ToString();
        int imageId = newTag.tag._imageId;
        Debug.Log(imageId);
        newTag.image.sprite = configManager.imageReference[imageId];
     
    }
}
