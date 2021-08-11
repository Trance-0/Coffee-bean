using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public GameObject _canvasToOpen;
    public GameObject _canvasToClose;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnClick() {
        _canvasToClose.SetActive(false);
        _canvasToOpen.SetActive(true);
    }
    
}
