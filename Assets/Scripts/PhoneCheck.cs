using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCheck : MonoBehaviour
{

    public Collider checkCollider;      //触发器
    public Canvas canvas;
    public Image next;
    public bool isInside = false;

    private void Start()
    {
        checkCollider = GetComponent<Collider>();
    }

    void Update()
    {
        canvas.gameObject.SetActive(isInside);
    }

    // 进入触发器范围时
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))     // 检查进入触发器的对象是否是你想要检测的对象
        {
            isInside = true;
        }

    }

    // 离开触发器范围时
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))     // 检查离开触发器的对象是否是你想要检测的对象
        {
            isInside = false;
        }
    }
    public void NextOnClick()
    {
        next.gameObject.SetActive(true);
    }

}
