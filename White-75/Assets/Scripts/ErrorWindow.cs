using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorWindow : MonoBehaviour
{
    public GameObject backGround;
    public GameObject frame;
    public GameObject text;


    // Start is called before the first frame update
    void Start()
    {
        CloseWindow();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Warning(string message) {
        text.GetComponent<Text>().text = message;
        backGround.SetActive(true);
        frame.SetActive(true);
    }
    public void CloseWindow() {
        backGround.SetActive(false);
        frame.SetActive(false);
    }
}
