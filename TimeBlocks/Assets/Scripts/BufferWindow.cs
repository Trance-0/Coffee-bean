using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BufferWindow : MonoBehaviour
{
    public ConfigManager configManager;

    public GameObject background;
    public GameObject icon;
    public GameObject title;
    public GameObject tips;
    
    public float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        CloseWindow();
    }

    // Update is called once per frame
    void Update()
    {
        icon.transform.Rotate(new Vector3(0,0,-rotateSpeed));
    }
    public void CloseWindow() {
        background.SetActive(false);
        icon.SetActive(false);
        title.SetActive(false);
        tips.SetActive(false);
    }

    public void OpenWindow()
    {
        background.SetActive(true);
        icon.SetActive(true);
        title.SetActive(true);
        tips.SetActive(true);
    }
    public void LoadBuffer(string content,float time) {
        OpenWindow();
        title.GetComponent<Text>().text = string.Format("{0}...",content);
        tips.GetComponent<Text>().text = string.Format("Tips: {0}",configManager.tips[Random.Range(0,configManager.tips.Length)]);
        Invoke("CloseWindow",time);
    }
}
