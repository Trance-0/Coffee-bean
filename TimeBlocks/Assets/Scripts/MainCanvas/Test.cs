using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    ButtonExtension btn;

    void Start()
    {
        btn = GetComponent<ButtonExtension>();
        btn.onClick.AddListener(Click);
        btn.onPress.AddListener(Press);
        btn.onDoubleClick.AddListener(DoubleClick);
    }

    void Click()
    {
        Debug.Log("click");
    }

    void Press()
    {
        Debug.Log("press");
    }

    void DoubleClick()
    {
        Debug.Log("double click");
    }
}