using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagUI : MonoBehaviour
{
    public Tag tag;
    public ConfigManager configManager;
    public ImageChanger imageChange;

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
        tag._imageId = imageId;
        image.sprite = configManager.imageReference[imageId];
    }
    public void WakeImageChanger() {
        imageChange.setGoal(self);
    }
    public void Save() {
        tag._power = int.Parse(weightInput.text);
        tag._name = tagNameInput.text;
    }

}
