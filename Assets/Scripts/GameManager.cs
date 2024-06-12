using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AStarPathFind pathFinder;

    // 更换目标方法
    public void ChangeTarget(Transform newTarget)
    {
        pathFinder.SetTarget(newTarget);
    }


    public Transform GetNewTarget()
    {
        // 实现获取新目标的逻辑
        GameObject newTargetObject = GameObject.FindWithTag("Extin");
        if (newTargetObject != null)
        {
            return newTargetObject.transform;
        }
        else
        {
            return null;
        }
    }
    public Transform GetExitTarget()
    {
        // 实现获取出口目标的逻辑
        GameObject newTargetObject = GameObject.FindWithTag("Exit");
        if (newTargetObject != null)
        {
            return newTargetObject.transform;
        }
        else
        {
            return null;
        }
    }


    // 重新开始游戏
    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;    // 获取当前活动场景的名称

        SceneManager.LoadScene(currentSceneName);   // 重新加载当前场景
        Time.timeScale = 1f;    // 恢复游戏
    }
}
