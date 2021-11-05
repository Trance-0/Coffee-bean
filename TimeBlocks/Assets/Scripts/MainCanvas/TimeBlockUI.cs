using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBlockUI : MonoBehaviour
{
    //global configurations
    public DataManager dataManager;
    public TaskOperatingContoler taskOperatingContoler;
    public CanvasManager canvasManager;
    public BlockChain blockChain;
    //local configurations
    public Image icon;
    public Animator animator;
    public Text taskName;
    public Image backGround;
    public TimeBlock timeBlock;
    public GameObject startTheTask;

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
        Debug.Log(timeBlock.ToString());
    }

    void Press()
    {
        Debug.Log("press");
        taskOperatingContoler.SendTask(timeBlock);
        canvasManager.ChangeCanvas(2);
    }

    void DoubleClick()
    {
        Debug.Log("double click");
        animator.SetTrigger("delete");
        //Animation needs time to play
        //DoDelete();
        Invoke("DoDelete", 1);    
    }

    void DoDelete()
    {
        dataManager.RemoveBlock(timeBlock);
        blockChain.ShowBlockChain();
    }
}
