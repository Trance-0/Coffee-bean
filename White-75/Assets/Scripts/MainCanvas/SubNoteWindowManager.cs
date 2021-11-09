using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubNoteWindowManager : MonoBehaviour
{
    public Button subNoteShow;
    public DataManager dataManager;
    public ErrorWindow errorWindow;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = subNoteShow.GetComponent<Button>();
        btn.onClick.AddListener(SubNoteShow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SubNoteShow() {
        errorWindow.Warning(dataManager.interruptions);
    }
    
}
