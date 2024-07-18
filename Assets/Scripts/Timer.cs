using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    // TMP 文本组件
    public TextMeshProUGUI timerText;

    // 计时器变量
    private float elapsedTime = 0f;
    private bool isRunning = false;

    void Start()
    {
        isRunning = true;
    }

    void Update()
    {
        // 更新计时时间
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerText();
        }
    }

    // 更新 TMP 文本组件显示的时间
    void UpdateTimerText()
    {
        // 将时间格式化为分钟:秒数
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    // 开始计时
    public void StartTimer()
    {
        isRunning = true;
    }

    // 停止计时
    public void StopTimer()
    {
        isRunning = false;
    }

    // 重置计时
    public void ResetTimer()
    {
        elapsedTime = 0f;
        UpdateTimerText();
    }
}
