using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using Unity.XR.PXR;

public class Menu : MonoBehaviour
{
    public GameObject menu;                     // �˵�UI����Ϸ����
    private InputDevice leftHandController;     // ���ֿ�����
    private bool isMenuOpen = false;            // Ĭ�ϲ˵��ر�
    private bool isMenuButtonPressed = false;   // ���ڼ��˵���ť�Ƿ��Ѱ���

    public AudioSource[] audioSources;          // ��Ϸ�е���ƵԴ
    public ParticleSystem[] particleSystems;    // ��Ϸ�е�����ϵͳ

    private bool isVR = false;

    void Start()
    {
        leftHandController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand); // ��ȡ���ֿ������豸
        if (PXR_Input.IsControllerConnected(PXR_Input.Controller.LeftController))
        {
            isVR = true;
        }
    }

    void Update()
    {
        // ��VRģʽ
        if (!isVR)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMenu();
            }
        }
        else
        {
            // VRģʽ
            if (leftHandController.isValid)
            {
                if (leftHandController.TryGetFeatureValue(CommonUsages.menuButton, out bool menuPressed))
                {
                    if (menuPressed && !isMenuButtonPressed)
                    {
                        isMenuButtonPressed = true;
                        ToggleMenu();
                    }
                    else if (!menuPressed)
                    {
                        isMenuButtonPressed = false;
                    }
                }
            }
        }
    }

    void ToggleMenu()
    {
        isMenuOpen = !isMenuOpen;
        menu.SetActive(isMenuOpen);

        if (isMenuOpen)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // ��ͣ��Ϸ

        // ֹͣ����
        foreach (AudioSource audio in audioSources)
        {
            audio.Pause();
        }

        // ֹͣ������Ч
        foreach (ParticleSystem particle in particleSystems)
        {
            particle.Pause();
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // �ָ���Ϸ

        // �ָ�����
        foreach (AudioSource audio in audioSources)
        {
            audio.UnPause();
        }

        // �ָ�������Ч
        foreach (ParticleSystem particle in particleSystems)
        {
            particle.Play();
        }
    }
}
