using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public List<GameObject> canvasList;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeCanvas(int index) {
        foreach (GameObject i in canvasList) {
            i.SetActive(false);
        }
        canvasList[index].SetActive(true);
    }
    
}
