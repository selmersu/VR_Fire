using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEXCheck : MonoBehaviour
{
    public GameManager gameManager;
    public ItemEvent itemEvent;

    public Collider checkCollider;  //触发器
    public Canvas popup;
    public Canvas fire;

    public bool isInside;

    private void Start()
    {
        checkCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        //
        if (!itemEvent.isPicked)
        {
            popup.gameObject.SetActive(isInside);
            if (isInside)   //没有灭火器时导航到灭火器前
            {
                
                Transform newTarget = gameManager.GetNewTarget();   // 获取新的目标
                gameManager.ChangeTarget(newTarget);    // 更改目标
                
            }
        }
        else
        {
            Transform newTarget = gameManager.GetExitTarget();   // 获取新的目标
            gameManager.ChangeTarget(newTarget);    // 更改目标
            popup.gameObject.SetActive(false);
            fire.gameObject.SetActive(isInside);
        }
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


}
