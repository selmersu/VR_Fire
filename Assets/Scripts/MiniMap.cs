using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{

    private RectTransform rect;
    private Transform player;   //���
    private static Image item;
    private Image playerImage;

    void Start()
    {

        item = Resources.Load<Image>("Image");  //����Image
        rect = GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;  //

        if (player != null)    
            playerImage = Instantiate(item);    // �����Ҷ���Ϊ�գ�ʵ�������ͼ��

    }

    

    void Update()
    {

        ShowPlayer();

    }

    private void ShowPlayer()
    {
        playerImage.rectTransform.sizeDelta = new Vector2(12,12);                       // ���ͼ��Ĵ�С
        playerImage.rectTransform.anchoredPosition = new Vector2(0,0);                  // ��������������ͼ�е�λ��
        playerImage.rectTransform.eulerAngles = new Vector3(0,0,-player.eulerAngles.y); // ��ͼ������ҽǶ���ת
        playerImage.sprite = Resources.Load<Sprite>("Texture/Player");                  //�����ͼ
        playerImage.transform.SetParent(transform,false);                               // �����ͼ������Ϊ��ǰ��Ϸ������Ӷ���
    }

}
