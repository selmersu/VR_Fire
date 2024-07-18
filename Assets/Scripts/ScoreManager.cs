using UnityEngine;
using TMPro;

public class TimerAndScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    // ��ʱ������
    private float elapsedTime = 0f;
    private bool isRunning = false;

    // ��ʼ����
    private int score = 100;

    // ���ڿ���ÿ10����ֵļ�ʱ��
    private float decrementInterval = 10f;
    private float timeSinceLastDecrement = 0f;
    private bool startDecrementing = false;

    void Start()
    {
        isRunning = true;

        // ��ʼ��������ʾ
        UpdateScoreText();
    }

    void Update()
    {
        // �����ʱ���������У����¼�ʱʱ��
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;

            // ��ʼ����ÿ10����ֵ�ʱ��
            if (elapsedTime >= 60f)
            {
                startDecrementing = true;
            }

            if (startDecrementing)
            {
                timeSinceLastDecrement += Time.deltaTime;

                if (timeSinceLastDecrement >= decrementInterval)
                {
                    // ���ٷ���
                    score -= 2;

                    // ���ü�ʱ��
                    timeSinceLastDecrement = 0f;

                    // ���·�����ʾ
                    UpdateScoreText();
                }
            }

            
        }
    }

  

    // ���� TMP �ı������ʾ�ķ���
    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
    }

    // ��ʼ��ʱ
    public void StartTimer()
    {
        isRunning = true;
    }

    // ֹͣ��ʱ
    public void StopTimer()
    {
        isRunning = false;
    }

    // ���ü�ʱ
    public void ResetTimer()
    {
        elapsedTime = 0f;
        timeSinceLastDecrement = 0f;
        startDecrementing = false;
        score = 100;

        UpdateScoreText();
    }
}
