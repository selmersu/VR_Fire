using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public AStarPathFind pathFinder;

    // ����Ŀ�귽��
    public void ChangeTarget(Transform newTarget)
    {
        pathFinder.SetTarget(newTarget);
    }


    public Transform GetNewTarget()
    {
        // ʵ�ֻ�ȡ��Ŀ����߼�
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
        // ʵ�ֻ�ȡ����Ŀ����߼�
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


    // ���¿�ʼ��Ϸ
    public void RestartGame()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;    // ��ȡ��ǰ�����������

        SceneManager.LoadScene(currentSceneName);   // ���¼��ص�ǰ����
        Time.timeScale = 1f;    // �ָ���Ϸ
    }
}
