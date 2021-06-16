using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TagUI : MonoBehaviour
{
    public Tag t;
    public InputField tagNameInput;
    public InputField weightInput;
    public Image image;
    // Start is called before the first frame update
    void Start()
    {
        tagNameInput.text = t._tagName;
        weightInput.text = t._power.ToString();
        image = t._image;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
