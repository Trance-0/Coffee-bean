using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagChain : MonoBehaviour
{
    //local configurations
    public GameObject tagChainUI;
    public TagUI tagPF;
    public ErrorWindow errorWindow;
    //global configurations
    public ConfigManager configManager;
    public DataManager dataManager;
    public SettingManager settingManager;
    public ImageChanger imageChanger;

    // Start is called before the first frame update
    void Start()
    {
        //comparing to invoke, this method is in a higher level, but I like invoke still
        //StartCoroutine(LateStart(1));
        Invoke("LateInit",0.1f);
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        LateInit();
    }

    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void setImageChangeGoal(TagUI tagUI)
    {
        imageChanger.setGoal(tagUI);
    }
    public bool CheckNameRepeated(string name)
    {
        //cause if user did not change at all, there must be a name identical to name, that is self.
        int nameCount = 0;
        for (int i = 0; i < dataManager.tags.Length; i++)
        {
            if (dataManager.tags[i]._name.CompareTo(name) == 0)
            {
                nameCount++;
            }
        }
        return nameCount>1;
    }
    public void Warning(string message) {
        errorWindow.Warning(message);
    }

    public Sprite GetImageReference(int imageId) {
        return configManager.imageReference[imageId];
    }
    public void LateInit()
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
    public void UpdateSetting() {
        settingManager.LoadDefaultTagValue();
    }

    private void CreateANewTag(Tag tag)
    {
        TagUI newTag = Instantiate(tagPF, tagChainUI.transform.position, Quaternion.identity);
        newTag.gameObject.transform.SetParent(tagChainUI.transform);
        newTag.tagData = tag;
        newTag.self = newTag;
        newTag.tagChain = this;
        newTag.tagNameInput.text= tag._name;
        newTag.weightInput.text = tag._power.ToString();
        int imageId = newTag.tagData._imageId;
        Debug.Log(imageId);
        newTag.image.sprite = configManager.imageReference[imageId];
     
    }
}
