using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagUI : MonoBehaviour
{
    public TagChain tagChain;

    public Tag tagData;

    public TagUI self;
    public Image image;

    public InputField tagNameInput;
    public InputField weightInput;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ChangeImage(int imageId) {
        tagData._imageId = imageId;
        image.sprite = tagChain.GetImageReference(imageId);
    }
    public void WakeImageChanger() {
        tagChain.setImageChangeGoal(self);
    }
    public void Save() {
        tagData._power = int.Parse(weightInput.text);
        if (tagChain.CheckNameRepeated(tagData.name))
        {
            tagChain.Warning("This tag name is already in use.");
        }
        else
        {
            tagData._name = tagNameInput.text;
        }
        tagChain.UpdateSetting();
    }

}
