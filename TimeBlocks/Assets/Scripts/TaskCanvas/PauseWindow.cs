using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseWindow : MonoBehaviour
{
    public TaskOperatingContoler taskOperatingContoler;
    public GameObject pause;
    public GameObject restart;
    public GameObject shade;

    public Text timer;
    public InputField timeRemain;
    // Start is called before the first frame update
    void Start()
    {
        pause.SetActive(false);
        shade.SetActive(false);
        restart.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Wake() {
        pause.SetActive(true);
        shade.SetActive(true);
    }
}
