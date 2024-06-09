using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Test : MonoBehaviour
{

    private InputDevice rightHandController;

    private bool isTriggerPressed = false;
    private float triggerPressTime = 0f;
    public float PressTime = 1.0f; // ������ֵʱ��

    public void Start()
    {
        rightHandController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }
    private void Update()
    {
        // ���trigger�Ƿ񱻰���
        if (rightHandController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed))
        {
            if (triggerPressed)
            {
                if (!isTriggerPressed)
                {
                    // �̰�trigger
                    isTriggerPressed = true;
                    triggerPressTime = Time.time;
                }
                else
                {
                    // ����trigger
                    if (Time.time - triggerPressTime >= PressTime)
                    {
                        OnTriggerLongPress();
                    }
                }
            }
            else
            {
                if (isTriggerPressed)
                {
                    // trigger�ոձ��ͷ�
                    isTriggerPressed = false;
                    if (Time.time - triggerPressTime < PressTime)
                    {
                        OnTriggerPress();
                    }
                }
            }
        }
    }

    // trigger�̰�
    private void OnTriggerPress()
    {
        Debug.Log("Trigger Pressed");
        
    }

    // trigger����
    private void OnTriggerLongPress()
    {
        Debug.Log("Trigger Long Pressed");
        
    }
}
