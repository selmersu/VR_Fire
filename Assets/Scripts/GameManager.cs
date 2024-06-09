using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager game; //创建一个单例

    public bool isPicked;           //判断是否捡起

    public GameObject Player;       //获取玩家对象


    private void Awake()
    {
        game = this;    //指定单例
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
