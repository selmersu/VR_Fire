using UnityEngine;

public class ExitCheck : MonoBehaviour
{
    public bool isInside = false;
    public Collider checkCollider;      //������
    public Canvas canvas;

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
            Time.timeScale = 0f; // ��ͣ��Ϸ
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
