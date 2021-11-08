using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cheat : MonoBehaviour
{
    public InputField input;
    public CanvasManager canvasManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeCanvas() {
        canvasManager.ChangeCanvas(int.Parse(input.text));
    }
}
