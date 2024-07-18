using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCheck : MonoBehaviour
{

    public Collider checkCollider;      //������
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
    public void NextOnClick()
    {
        next.gameObject.SetActive(true);
    }

}
