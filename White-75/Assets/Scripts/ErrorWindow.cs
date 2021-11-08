using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrorWindow : MonoBehaviour
{
    public GameObject BG;
    public GameObject text;
    public GameObject confirm;


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
        BG.SetActive(true);
        text.SetActive(true);
        confirm.SetActive(true);
    }
    public void CloseWindow() {
        BG.SetActive(false);
        text.SetActive(false);
        confirm.SetActive(false);
    }
}
