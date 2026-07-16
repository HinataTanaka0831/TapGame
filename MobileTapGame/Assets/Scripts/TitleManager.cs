using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public static TitleManager Instance { get; private set; }

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
            Destroy(Instance);
        }
    }

    /// <summary>
    /// 最初に一回だけ呼ばれる初期化
    /// </summary>
   private void Start()
    {
        // タイトル画面中に流れるBGMを再生する
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBackgroundMusicTitle();
        }

    }

}
