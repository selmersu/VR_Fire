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
    public FireEffectController fireEffectController;

    public ParticleSystem ps; // ����ϵͳ

    private InputDevice rightController;
    private InputDevice leftController;
    
    public float longPressDuration = 1f; // ���������ĳ���ʱ��

    private bool isLongPressing = false; // �Ƿ����ڳ���
    private float pressStartTime; // ������ʼʱ��
    private bool isFireActivated = false; // �Ƿ��Ѿ������

    [Header("Test")]
    public TMP_Text debugText; // TextMeshPro ���������
    public ParticleSystem ps_test;
    public GameObject obj_test;

    private void Start()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        itemEvent = GetComponent<ItemEvent>();
        fireEffectController = GetComponent<FireEffectController>();

        rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        leftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        
    }

    void Update()
    {
        FESpray();  //(LT)���������������
        //test
        if (Input.GetKeyDown(KeyCode.O))
        {
            fireEffectController.ExtinguishFire(ps_test);
            fireEffectController.GetBoXCollider(obj_test);
        }

            if (rightController.isValid)
        {
            // ��������ֱ��� trigger ��ť�Ƿ���
            if (rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerPressed) && triggerPressed)
            {
                HandleRaycast();
            }
        }

        /*if (itemEvent.isPicked)
        {
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
        }*/
    }

    //���������
    void FESpray()
    {
        if (itemEvent.isPicked)
        {
            // ������ֱ� trigger �ĳ���
            if (CheckLongPressLT())
            {
                // һ�γ���ֻ����һ�� GetFire ����
                if (!isFireActivated)
                {
                    GetFire();
                    isFireActivated = true;
                }
                ps.Play(); // ��������Ч��
            }
            else
            {
                ps.Stop(); // ֹͣ����Ч��
                ps.Clear();
                ps.Simulate(0f, true, true); // ����ģ������ϵͳ���������õ���ʼ״̬

                isFireActivated = false; // ���ô��� GetFire �����ı�־
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
