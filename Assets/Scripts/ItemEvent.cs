using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ItemEvent : MonoBehaviour
{
    public FireExtinguisherUsage extinguisherUsage;

    public GameObject feModel;  //�����ģ��

    public bool isPicked = false;           //�ж��Ƿ񱻼���
    

    

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

    //��ʾ�����ģ��
    public void ShowFE()
    {
        if (feModel != null)
        {
            feModel.SetActive(true);
            isPicked = true;
        }
        
    }

    //���������ģ��
    public void HideFE()
    {
        if (feModel != null)
        {
            feModel.SetActive(false);
            isPicked=false;
        }   
    }
}
