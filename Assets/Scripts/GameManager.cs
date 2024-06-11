using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager game; //����һ������

    public bool isPicked;           //�ж��Ƿ����

    public GameObject Player;       //��ȡ��Ҷ���


    private void Awake()
    {
        game = this;    //ָ������
    }

    // ���¿�ʼ��Ϸ
    public void RestartGame()
    {
        // ��ȡ��ǰ�����������
        string currentSceneName = SceneManager.GetActiveScene().name;

        // ���¼��ص�ǰ����
        SceneManager.LoadScene(currentSceneName);
        Time.timeScale = 1f; // �ָ���Ϸ
    }
}
