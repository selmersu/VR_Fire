using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ItemEvent : MonoBehaviour
{
    public FireExtinguisherUsage extinguisherUsage;

    public GameObject feModel;  //灭火器模型

    public bool isPicked = false;           //判断是否被捡起
    

    

    private InputDevice rightController;
    private InputDevice leftController;
    private void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        
        
    }

    private void Update()
    {
        
    }

    //显示灭火器模型
    public void ShowFE()
    {
        if (feModel != null)
        {
            feModel.SetActive(true);
            isPicked = true;
        }
        
    }

    //隐藏灭火器模型
    public void HideFE()
    {
        if (feModel != null)
        {
            feModel.SetActive(false);
            isPicked=false;
        }   
    }
}
