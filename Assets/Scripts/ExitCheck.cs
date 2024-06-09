using UnityEngine;

public class ExitCheck : MonoBehaviour
{
    public bool isInside = false;
    public Collider checkCollider;      //触发器
    public Canvas canvas;

 
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
}
