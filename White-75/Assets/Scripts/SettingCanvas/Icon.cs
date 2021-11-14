using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Icon : MonoBehaviour
{
    public TagUI goal;
    public Button button;
    public Image image;
    public int iconId;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setGoal(TagUI k)
    {
        goal=k;
    }
    public void Change() {
        Debug.Log("Clicked");
        goal.ChangeImage(iconId);
    }
}
