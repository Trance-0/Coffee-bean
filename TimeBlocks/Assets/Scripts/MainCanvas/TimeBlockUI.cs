using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBlockUI : MonoBehaviour
{
    public Image icon;
    public Text taskName;
    public Image backGround;
    public TimeBlock timeBlock;
    public GameObject startTheTask;

    public DataManager dataManager;
    public TaskOperatingContoler taskOperatingContoler;
    public CanvasManager canvasManager;
    public BlockChain blockChain;

    // Start is called before the first frame update
    void Start()
    {
        ButtonExtension btn = startTheTask.GetComponent<ButtonExtension>();
        btn.onClick.AddListener(Click);
        btn.onPress.AddListener(Press);
        btn.onDoubleClick.AddListener(DoubleClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Click()
    {
        Debug.Log("click");
    }

    void Press()
    {
        Debug.Log("press");
        taskOperatingContoler.SendTask(timeBlock);
        canvasManager.ChangeCanvas(0);
    }

    void DoubleClick()
    {
        Debug.Log("double click");
        dataManager.blocks.Remove(timeBlock);
        dataManager.Save();
        blockChain.ShowBlockChain(); 
    }
}
