using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagChain : MonoBehaviour
{
    //local configurations
    public GameObject tagChainUI;
    public TagUI tagPF;
    //global configurations
    public ImageChanger imageChanger;
    public ConfigManager configManager;
    public DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        //comparing to invoke, this method is in a higher level, but I like invoke still
        //StartCoroutine(LateStart(1));
        Invoke("ShowTagChain",0.1f);
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
        foreach (Tag i in dataManager.tags)
                {
                    CreateANewTag(i);
                }
   }

    private void CreateANewTag(Tag tag)
    {
        TagUI newTag = Instantiate(tagPF, tagChainUI.transform.position, Quaternion.identity);
        newTag.gameObject.transform.SetParent(tagChainUI.transform);
        newTag.tagData = tag;
        newTag.self = newTag;
        newTag.imageChange = imageChanger;
        newTag.dataManager = dataManager;
        newTag.configManager = configManager;
        newTag.tagNameInput.text= tag._name;
        newTag.weightInput.text = tag._power.ToString();
        int imageId = newTag.tagData._imageId;
        Debug.Log(imageId);
        newTag.image.sprite = configManager.imageReference[imageId];
     
    }
}
