using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager game; //创建一个单例

    public bool isPicked;           //判断是否捡起

    public GameObject Player;       //获取玩家对象


    private void Awake()
    {
        game = this;    //指定单例
    }

    // 重新开始游戏
    public void RestartGame()
    {
        // 获取当前活动场景的名称
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 重新加载当前场景
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1f; // 恢复游戏
    }
}
