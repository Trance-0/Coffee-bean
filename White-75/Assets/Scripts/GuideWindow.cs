using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideWindow : MonoBehaviour
{
    public ConfigManager configManager;
    public Button infoWaker;
    public GameObject info;
    public bool isActive;
    public float localTimer;
    // Start is called before the first frame update
    void Start()
    {
        localTimer = configManager.guideFadeTime;
        isActive = false;
        info.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive) {
            localTimer -= Time.deltaTime;
        }
        if (localTimer<0) {
            localTimer = configManager.guideFadeTime;
            isActive = false;
            info.SetActive(false);
        }
    }

    public void Switch() {
        isActive = !isActive;
        info.SetActive(isActive);
    }
}
