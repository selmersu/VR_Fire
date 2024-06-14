using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEXCheck : MonoBehaviour
{
    public GameManager gameManager;
    public ItemEvent itemEvent;

    public Collider checkCollider;  //������
    public Canvas canvas;
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
            canvas.gameObject.SetActive(isInside);
            if (isInside)   //û�������ʱ�����������ǰ
            {
                
                Transform newTarget = gameManager.GetNewTarget();   // ��ȡ�µ�Ŀ��
                gameManager.ChangeTarget(newTarget);    // ����Ŀ��
                
            }
        }
        else
        {
            Transform newTarget = gameManager.GetExitTarget();   // ��ȡ�µ�Ŀ��
            gameManager.ChangeTarget(newTarget);    // ����Ŀ��
            canvas.gameObject.SetActive(false);
        }
    }

    // ���봥������Χʱ
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))     // �����봥�����Ķ����Ƿ�������Ҫ���Ķ���
        {
            isInside = true;
        }

    }

    // �뿪��������Χʱ
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))     // ����뿪�������Ķ����Ƿ�������Ҫ���Ķ���
        {
            isInside = false;
        }
    }


}
