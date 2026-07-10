using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI hightScoreText;
    private Rigidbody2D rigidBody2D;

    /// <summary>
    /// 最初に一回だけ呼ばれる初期化
    /// </summary>
    void Start()
    {
        // リザルト画面中に流れるBGMを再生する
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBackgroundMusicResult();
        }

        // ゲーム中に保存されたスコアを読み出す　
        // ※まだ保存されていなければ0を返す
        int finalScore = PlayerPrefs.GetInt("LatestScore", 0);

        // 保存されている「過去のハイスコア」を読み出す
        int hightScore = PlayerPrefs.GetInt("HightScore", 0);

        // もし今回のスコアが過去のハイスコアを超えていたら、ハイスコアを更新し、「HightScore」という名前でスコアを保存する
        if (finalScore > hightScore)
        {
            hightScore = finalScore;
            PlayerPrefs.SetInt("HightScore", hightScore);
            PlayerPrefs.Save();
        }

        // UIにスコアを反映する
        if (finalScoreText != null)
        {
            finalScoreText.text = "最終スコア: " + finalScore.ToString();
        }

        if (hightScoreText != null)
        {
            hightScoreText.text = "ハイスコア: " + hightScore.ToString();
        }


        rigidBody2D = GetComponent<Rigidbody2D>();

        if (rigidBody2D != null)
        {
            // Rigidbody2Dの重力を無効化（スクリプトから確実に設定）
            rigidBody2D.gravityScale = 0.0f;
        }
    }

}
