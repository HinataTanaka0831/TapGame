using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] private float gameTime = 30.0f; // ゲーム制限時間
    private float remainingTime;

    public float RemainingTime => remainingTime;

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
        // 制限時間を初期化
        remainingTime = gameTime;

    }

    /// <summary>
    /// 更新
    /// </summary>
    private void Update()
    {
        // 制限時間のカウントダウンをして、0になったらスコアを保存してリザルトシーンに遷移する
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            remainingTime = 0f;
            ScoreManager.Instance.SaveScore();
            // フェードマネージャーを使用してリザルトシーンに遷移
            if (FadeManager.Instance != null)
            {
                FadeManager.Instance.LoadScene("Result");
            }
            else
            {
                SceneManager.LoadScene("Result");
            }
        }

        UIManager.Instance?.UpdateTimeUI();
    }


}
