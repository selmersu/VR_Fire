using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager game; //����һ������

    public bool isPicked;           //�ж��Ƿ����

    public GameObject Player;       //��ȡ��Ҷ���


    private void Awake()
    {
        game = this;    //ָ������
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
