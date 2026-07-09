using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    
    [Header("UI Text References (Inspectorからアタッチしてください)")]
    [SerializeField] private TextMeshProUGUI timeText; 
    [SerializeField] private TextMeshProUGUI scoreText;

    /// <summary>
    /// ゲーム内に一つしか存在しないようにする
    /// </summary>
   private void Awake()
    {
        // 最初の一つ目ならインスタンスを保存し、2つ以上生成されないように重複したオブジェクトは破棄する
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// 最初に一回だけ呼ばれる初期化
    /// </summary>
    private void Start()
    {
        // 初期UIの更新
        UpdateTimeUI();
        UpdateScoreUI();
    }

    /// <summary>
    /// 制限時間のUIを更新
    /// </summary>
    public void UpdateTimeUI()
    {
        // GameManagerのインスタンスと制限時間テキストが存在する場合にのみUIを更新
        if (GameManager.Instance != null && this.timeText != null)
        {
            float remainingTime = GameManager.Instance.RemainingTime;
            this.timeText.text = "残り時間: " + remainingTime.ToString("F1") + "秒";
        }
    }

    /// <summary>
    /// スコアのUIを更新
    /// </summary>
    public void UpdateScoreUI()
    {
        // GameManagerのインスタンスとスコアテキストが存在する場合にのみUIを更新
        if (GameManager.Instance != null && this.scoreText != null)
        {
            int currentScore = ScoreManager.Instance.Score;
            this.scoreText.text = "スコア: " + currentScore.ToString();
        }
    }
}
