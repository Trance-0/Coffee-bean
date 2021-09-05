using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Background : MonoBehaviour
{
    public Image backgroud;
    public DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        backgroud.color = dataManager.backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
