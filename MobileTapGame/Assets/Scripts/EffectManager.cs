using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class EffectManager : MonoBehaviour
{
    public static EffectManager Instance { get; private set; }
    // Inspectorでエフェクトのプレハブをセットする枠
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

    // タップされたオブジェクト（背景や、エフェクトを出したい土台）につける前提
   public void OnTap(Vector3 position)
    {
        // 2. エフェクトを生成（タップした位置に、回転なしで）
        // Instanciate(生成するもの, 位置, 回転)
        GameObject effectInstance = Instantiate(effectPrefab, position, Quaternion.identity);

        // 3. 生成したエフェクトを自動で消す処理（オプション）
        // エフェクト側に専用スクリプトをつけてアニメーション後にDestroyさせるのが一番安全ですが、
        // とりあえず手軽にやるなら、アニメーションの時間分待ってからDestroyするコルーチンを使います。

        // プレハブからAnimatorを取得してアニメーションの長さを調べる
        Animator anim = effectInstance.GetComponent<Animator>();
        if (anim != null)
        {
           
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            float animLength = stateInfo.length;

            // アニメーション終了後に破棄する
            Destroy(effectInstance, animLength);
        }
        else
        {
            // Animatorがない場合は1秒後に消す（安全策）
            Destroy(effectInstance, 1.0f);
        }

    }

}
