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

    public GameObject edit;
    public InputField tagNameInput;
    public InputField weightInput;

    public GameObject show;
    public Text tagName;
    public Text weight;
    
    // Start is called before the first frame update
    void Start()
    {
        show.SetActive(true);
        edit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Edit() {

    }
    public void Save() {
    }
}
