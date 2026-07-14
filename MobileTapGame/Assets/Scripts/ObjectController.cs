using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    [Header("Score Settings")]
    [SerializeField] private int scoreValue = 10;

    [Header("Visual Effects")]
    [SerializeField] private GameObject fadeEffectPrefab;

    private Rigidbody2D rigidBody2D;

    /// <summary>
    /// 最初に一回だけ呼ばれる初期化
    /// </summary>
   void Start()
   {
        rigidBody2D = GetComponent<Rigidbody2D>();
        
        // Rigidbody2Dの重力を無効化（スクリプトから確実に設定）
        if (rigidBody2D != null)
        {
            rigidBody2D.gravityScale = 0.0f;
        }

   }

    /// <summary>
    /// 更新
    /// </summary>
    void Update()
    {

    }

    /// <summary>
    /// オブジェクトがタップされたら削除する
    /// </summary>
    public void Fade()
    {
        if (EffectGenerator.Instance != null)
        {
            EffectGenerator.Instance.OnTap(transform.position);
        }

        // オブジェクトが消えた時のサウンドを再生
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayFadeSound();
        }

        // スコア加算
        ScoreManager.Instance.AddScore(scoreValue);

        // オブジェクト自身を消滅させる
        Destroy(gameObject);

    }
}
