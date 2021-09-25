using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuideWindow : MonoBehaviour
{
    public Button infoWaker;
    public GameObject info;
    public bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        info.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Switch() {
        isActive = !isActive;
        info.SetActive(isActive);
    }
}
