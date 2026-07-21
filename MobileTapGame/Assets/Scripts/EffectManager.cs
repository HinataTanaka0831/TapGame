using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }
    [SerializeField] private GameObject effectPrefab;

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
    /// タップされたオブジェクトにエフェクトを表示する
    /// </summary>
    /// <param name="position"> エフェクト表示する位置を設定</param>
    public void OnTap(Vector3 position)
    {
        // エフェクトを生成（タップした位置に、回転なしで）
        // Instanciate(生成するもの, 位置, 回転)
        GameObject effectInstance = Instantiate(effectPrefab, position, Quaternion.identity);

        // 生成したエフェクトを自動で消す処理
        // プレハブからAnimatorを取得してアニメーションの長さを調べる。アニメーション終了後に破棄する
        Animator anim = effectInstance.GetComponent<Animator>();
        if (anim != null)
        {
           
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            float animLength = stateInfo.length;

            Destroy(effectInstance, animLength);
        }
        else
        {
            // Animatorがない場合は1秒後に消す（安全策）
            Destroy(effectInstance, 1.0f);
        }

    }

}
