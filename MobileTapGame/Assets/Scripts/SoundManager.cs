using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip backgroundMusicTitle;
    [SerializeField] private AudioClip soundEffectGame;
    [SerializeField] private AudioClip backgroundMusicResult;
    [SerializeField] private AudioClip fadeSound;
    [SerializeField] private AudioClip buttonClickSound;

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

    /// <summary>
    /// 最初に一回だけ呼ばれる初期化
    /// </summary>
   private void Start()
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
    /// タイトル画面中に流れる背景音楽を再生する
    /// </summary>
    public void PlayBackgroundMusicTitle()
    {
        if (audioSource != null && backgroundMusicTitle != null)
        {
            audioSource.clip = backgroundMusicTitle;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    /// <summary>
    /// ゲーム中に流れる効果音を再生する
    /// </summary>
    public void PlaySoundEffectGame()
    {
        if (audioSource != null && soundEffectGame != null)
        {
            audioSource.clip = soundEffectGame;
            audioSource.loop = true;
            audioSource.Play();
        }
    }


    
    public void PlayBackgroundMusicResult()
    {
        if (audioSource != null && backgroundMusicResult != null)
        {
            audioSource.clip = backgroundMusicResult;
            audioSource.loop = true;
            audioSource.Play();
        }
    }

    /// <summary>
    /// オブジェクトが消えた時のSEを再生する
    /// </summary>
    public void PlayFadeSound()
    {
        if (audioSource != null && fadeSound != null)
        {
            audioSource.PlayOneShot(fadeSound);
        }
    }


    /// <summary>
    /// ボタンをクリックした際のSEを再生する
    /// </summary>
    public void PlayButtonSound()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
    }
}
