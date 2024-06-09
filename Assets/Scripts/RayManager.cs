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

    public ParticleSystem ps; // ����ϵͳ
    public float longPressDuration = 1f; // ���������ĳ���ʱ��

    private bool isLongPressing = false; // �Ƿ����ڳ���
    private float pressStartTime; // ������ʼʱ��
    private bool isFireActivated = false; // �Ƿ��Ѿ������

    public TMP_Text debugText; // TextMeshPro ���������
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
            // ��������ֱ��� trigger ��ť�Ƿ���
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

            

            // ������ֱ� trigger �ĳ���
            if (CheckLongPressLT() == true)
            {
                // һ�γ���ֻ����һ�� GetFire ����
                if (!isFireActivated)
                {
                    GetFire();
                    isFireActivated = true;
                }
                ps.Play();    // ��������Ч��
            }
            else
            {
                ps.Stop();    // ֹͣ����Ч��
                ps.Clear();
                ps.Simulate(0f, true, true); // ����ģ������ϵͳ���������õ���ʼ״̬

                isFireActivated = false;    // ���ô��� GetFire �����ı�־
            }

            if (rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool PrimaryPressed) && PrimaryPressed)
            {
                itemEvent.HideFE();
            }
        }

        
    }

    // ������ֱ� trigger �ĳ���
    bool CheckLongPressLT()
    {
        if (leftController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.5f)
        {
            if (!isLongPressing)
            {
                // ��¼������ʼʱ��
                pressStartTime = Time.time;
                isLongPressing = true;
            }
            else
            {
                // ��鳤��ʱ���Ƿ񳬹��趨�ĳ�������ʱ��
                if (Time.time - pressStartTime >= longPressDuration)
                {
                    return true;
                }
            }
        }
        else
        {
            isLongPressing = false;// ���ó���״̬
        }

        return false;
    }

    private void GetFire()
    {
        if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            // ��ȡ���߻��е�����
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
        // ���XR Ray Interactor
        if (rayInteractor != null)
        {
            // ���߼��
            if (rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
            {
                // ��ȡ���߻��е�����
                GameObject hitObject = hit.collider.gameObject;

                // ����ǩ�Ƿ�Ϊ"Extin"
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
