using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// タップ時のスコア加算と UI 更新を管理するクラス
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    private int score = 0;

    public int Score => score;

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
   void Start()
   {
       this.score = 0;
   }

    /// <summary>
    /// スコアを加算する
    /// </summary>
    public void AddScore(int points)
    {
        // タップされたオブジェクトの点数を総スコアに反映して、UIManager にスコア更新を通知する
        this.score += points;
        if (UIManager.Instance != null)
        {
            UIManager.Instance.UpdateScoreUI();
        }
    }

    /// <summary>
    /// スコアを保存する
    /// </summary>
    public void SaveScore()
    {
        // 今回のスコアを「LatestScore」という名前で一時保存し、「PlayerPrefs.Save()」で確実に保存を確定させる
        PlayerPrefs.SetInt("LatestScore", this.score);
        PlayerPrefs.Save();
    }

}
