using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour
{

    private RectTransform rect;
    private Transform player;   //玩家
    private static Image item;
    private Image playerImage;

    void Start()
    {

        item = Resources.Load<Image>("Image");  //加载Image
        rect = GetComponent<RectTransform>();
        player = GameObject.FindGameObjectWithTag("Player").transform;  //

        if (player != null)    
            playerImage = Instantiate(item);    // 如果玩家对象不为空，实例化玩家图标

    }

    

    void Update()
    {

        ShowPlayer();

    }

    private void ShowPlayer()
    {
        playerImage.rectTransform.sizeDelta = new Vector2(12,12);                       // 玩家图标的大小
        playerImage.rectTransform.anchoredPosition = new Vector2(0,0);                  // 设置玩家在迷你地图中的位置
        playerImage.rectTransform.eulerAngles = new Vector3(0,0,-player.eulerAngles.y); // 地图跟随玩家角度旋转
        playerImage.sprite = Resources.Load<Sprite>("Texture/Player");                  //玩家贴图
        playerImage.transform.SetParent(transform,false);                               // 将玩家图标设置为当前游戏对象的子对象
    }

}
