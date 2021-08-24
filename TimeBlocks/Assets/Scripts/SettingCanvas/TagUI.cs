using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagUI : MonoBehaviour
{
    public Tag t;
    public ConfigManager configManager;
    public Button imageChange;

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
    void ChangeImage(int imageId) {
        t._imageId = imageId;
    }
    void Save() {
        t._power = int.Parse(weightInput.text);
        t._name = tagNameInput.text;
    }
}
