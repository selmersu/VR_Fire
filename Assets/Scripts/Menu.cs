using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;
using Unity.XR.PXR;

public class Menu : MonoBehaviour
{
    public GameObject menu;                     // 菜单UI的游戏对象
    private InputDevice leftHandController;     // 左手控制器
    private bool isMenuOpen = false;            // 默认菜单关闭
    private bool isMenuButtonPressed = false;   // 用于检测菜单按钮是否已按下

    public AudioSource[] audioSources;          // 游戏中的音频源
    public ParticleSystem[] particleSystems;    // 游戏中的粒子系统

    private bool isVR = false;

    void Start()
    {
        leftHandController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand); // 获取左手控制器设备
        if (PXR_Input.IsControllerConnected(PXR_Input.Controller.LeftController))
        {
            isVR = true;
        }
    }

    void Update()
    {
        // 非VR模式
        if (!isVR)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ToggleMenu();
            }
        }
        else
        {
            // VR模式
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
        Time.timeScale = 0f; // 暂停游戏

        // 停止声音
        foreach (AudioSource audio in audioSources)
        {
            audio.Pause();
        }

        // 停止粒子特效
        foreach (ParticleSystem particle in particleSystems)
        {
            particle.Pause();
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // 恢复游戏

        // 恢复声音
        foreach (AudioSource audio in audioSources)
        {
            audio.UnPause();
        }

        // 恢复粒子特效
        foreach (ParticleSystem particle in particleSystems)
        {
            particle.Play();
        }
    }
}
