using UnityEngine;
using TMPro;

public class TimerAndScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    // 计时器变量
    private float elapsedTime = 0f;
    private bool isRunning = false;

    // 初始分数
    private int score = 100;

    // 用于控制每10秒减分的计时器
    private float decrementInterval = 10f;
    private float timeSinceLastDecrement = 0f;
    private bool startDecrementing = false;

    void Start()
    {
        isRunning = true;

        // 初始化分数显示
        UpdateScoreText();
    }

    void Update()
    {
        // 如果计时器正在运行，更新计时时间
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;

            // 开始计算每10秒减分的时间
            if (elapsedTime >= 60f)
            {
                startDecrementing = true;
            }

            if (startDecrementing)
            {
                timeSinceLastDecrement += Time.deltaTime;

                if (timeSinceLastDecrement >= decrementInterval)
                {
                    // 减少分数
                    score -= 2;

                    // 重置计时器
                    timeSinceLastDecrement = 0f;

                    // 更新分数显示
                    UpdateScoreText();
                }
            }

            
        }
    }

  

    // 更新 TMP 文本组件显示的分数
    void UpdateScoreText()
    {
        scoreText.text = score.ToString();
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
        timeSinceLastDecrement = 0f;
        startDecrementing = false;
        score = 100;

        UpdateScoreText();
    }
}
