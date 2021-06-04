using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBlockUI : MonoBehaviour
{
    public Image icon;
    public Text taskName;
    public Image backGround;
    public Transform horizontalLayOut;
    public TimeBlock timeBlock;
    public BlockChain blockChain;
    public int deleteThreshold;
    public int finishThreshold;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
     //   if (horizontalLayOut.GetComponent<RectTransform>().position.x<finishThreshold) {
     //       Debug.Log("Finish");
     //       blockChain.MarkAsFinished(timeBlock);
      // }
     //   if (horizontalLayOut.GetComponent<RectTransform>().position.x > deleteThreshold)
     //   {
     //       Debug.Log("Delete"+ horizontalLayOut.GetComponent<RectTransform>().position.x);
     //       blockChain.DeleteBlock(timeBlock);
     //   }
    }
}
