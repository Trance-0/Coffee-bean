using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    //waiting to implement, if everythings goes well, store the data of config manager in an extra json file.
    public bool rememberPassword;
    public bool isOffline;
    public string[] tips;
    public float guideFadeTime;

    public List<Sprite> imageReference;
    public Color imageColor;
    public TimeBlock lastInput;
    public bool sortingByTime;

    public bool isAdvanced;
    public bool isAddNewTaskWindowAwake;
    //remember to set a quit for everywindow

    public bool isOnline;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
