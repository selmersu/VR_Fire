using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class RayManager : MonoBehaviour
{
    public XRRayInteractor rayInteractor;
    public ItemEvent itemEvent;
    public FireExtinguisherUsage extinguisherUsage;
    public FireEffectController fireEffectController;

    private InputDevice rightController;
    private InputDevice leftController;

    public ParticleSystem ps; // 粒子系统
    public float longPressDuration = 1f; // 长按触发的持续时间

    private bool isLongPressing = false; // 是否正在长按
    private float pressStartTime; // 长按开始时间
    private bool isFireActivated = false; // 是否已经激活函数

    public TMP_Text debugText; // TextMeshPro 组件的引用
    public ParticleSystem ps1;
    public GameObject obj;

    private void Start()
    {
        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            fireEffectController.ExtinguishFire(ps1);
            fireEffectController.GetBoXCollider(obj);
        }

            if (rightController.isValid)
        {
            // 检查右手手柄的 trigger 按钮是否按下
            if (rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
            {
                HandleRaycast();
            }
        }
        if (itemEvent.isPicked)
        {
            /*if (!isFireActivated)
            {
                if (Input.GetKey(KeyCode.O))
                {
                    ps.Play();
                    isFireActivated = true;
                }
            }
            else
            {
                ps.Stop();  
                isFireActivated = false;
            }*/

            

            // 检测左手柄 trigger 的长按
            if (CheckLongPressLT() == true)
            {
                // 一次长按只调用一次 GetFire 函数
                if (!isFireActivated)
                {
                    GetFire();
                    isFireActivated = true;
                }
                ps.Play();    // 播放粒子效果
            }
            else
            {
                ps.Stop();    // 停止粒子效果
                ps.Clear();
                ps.Simulate(0f, true, true); // 重新模拟粒子系统，将其重置到初始状态

                isFireActivated = false;    // 重置触发 GetFire 函数的标志
            }

            if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool PrimaryPressed) && PrimaryPressed)
            {
                itemEvent.HideFE();
            }
        }

        
    }

    // 检测左手柄 trigger 的长按
    bool CheckLongPressLT()
    {
        if (leftController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.5f)
        {
            if (!isLongPressing)
            {
                // 记录长按开始时间
                pressStartTime = Time.time;
                isLongPressing = true;
            }
            else
            {
                // 检查长按时间是否超过设定的长按触发时间
                if (Time.time - pressStartTime >= longPressDuration)
                {
                    return true;
                }
            }
        }
        else
        {
            isLongPressing = false;// 重置长按状态
        }

        return false;
    }

    private void GetFire()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            // 获取射线击中的物体
            GameObject hitObject = hit.collider.gameObject;
            if (hitObject.CompareTag("Fire"))
            {
                if (fireEffectController != null)
                {
                    fireEffectController.GetParticleSystem(hitObject);
                    fireEffectController.GetBoXCollider(hitObject);
                }

                else return;
            }

            //extinguisherUsage.ExtinguishFireAtController(rightController);
        }
    }

    private void HandleRaycast()
    {
        // 检查XR Ray Interactor
        if (rayInteractor != null)
        {
            // 射线检测
            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                // 获取射线击中的物体
                GameObject hitObject = hit.collider.gameObject;

                // 检查标签是否为"Extin"
                if (hitObject.CompareTag("Extin"))
                {
                    if (itemEvent != null)
                    {
                        itemEvent.ShowFE();
                    }
                    else
                    {
                        Debug.LogWarning("itemEvent is not assigned.");
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("rayInteractor is not assigned.");
        }
    }
}
