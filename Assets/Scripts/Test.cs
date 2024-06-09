using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Test : MonoBehaviour
{

    private InputDevice rightHandController;

    private bool isTriggerPressed = false;
    private float triggerPressTime = 0f;
    public float PressTime = 1.0f; // 长按阈值时间

    public void Start()
    {
        rightHandController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }
    private void Update()
    {
        // 检查trigger是否被按下
        if (rightHandController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed))
        {
            if (triggerPressed)
            {
                if (!isTriggerPressed)
                {
                    // 短按trigger
                    isTriggerPressed = true;
                    triggerPressTime = Time.time;
                }
                else
                {
                    // 长按trigger
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
                    // trigger刚刚被释放
                    isTriggerPressed = false;
                    if (Time.time - triggerPressTime < PressTime)
                    {
                        OnTriggerPress();
                    }
                }
            }
        }
    }

    // trigger短按
    private void OnTriggerPress()
    {
        Debug.Log("Trigger Pressed");
        
    }

    // trigger长按
    private void OnTriggerLongPress()
    {
        Debug.Log("Trigger Long Pressed");
        
    }
}
