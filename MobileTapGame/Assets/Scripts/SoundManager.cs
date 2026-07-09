using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusic; 
    [SerializeField] private AudioClip appearSound;
    [SerializeField] private AudioClip fadeSound;

    /// <summary>
    /// ゲーム内に一つしか存在しないようにする
    /// </summary>
   private void Awake()
    {
        // 最初の一つ目ならインスタンスを保存し、2つ以上生成されないように重複したオブジェクトは破棄する
        // シーンを跨いでも破棄されないようにする。既にインスタンスが存在する場合は破棄する
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // AudioSourceのコンポーネント取得（アタッチし忘れたとき用）
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

    }

    
    /// <summary>
    /// ゲーム中に流れる背景音楽を再生する
    /// </summary>
    public void PlayBackgroundMusic()
    {
        if (audioSource != null && backgroundMusic != null)
        {
            audioSource.clip = backgroundMusic;
            audioSource.loop = true;
            audioSource.Play();
        }
    }


    /// <summary>
    /// オブジェクトが生成された時の効果音を再生する
    /// </summary>
    public void PlayAppearSound()
    {
        if (audioSource != null && appearSound != null)
        {
            audioSource.PlayOneShot(appearSound);
        }
    }

    /// <summary>
    /// オブジェクトが消えた時の効果音を再生する
    /// </summary>
    public void PlayFadeSound()
    {
        if (audioSource != null && fadeSound != null)
        {
            audioSource.PlayOneShot(fadeSound);
        }
    }

}
