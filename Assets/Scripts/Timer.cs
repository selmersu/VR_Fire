using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // TMP �ı����
    public TextMeshProUGUI timerText;

    // ��ʱ������
    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        isRunning = true;
    }

    void Update()
    {
        // ���¼�ʱʱ��
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    // ���� TMP �ı������ʾ��ʱ��
    void UpdateTimerText()
    {
        // ��ʱ���ʽ��Ϊ����:����
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
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
        UpdateTimerText();
    }
}
