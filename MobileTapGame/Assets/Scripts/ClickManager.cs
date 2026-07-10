using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    /// <summary>
    /// 更新
    /// </summary>
   void Update()
   {
       OnEnableTapObject();
       OnEnableTapButton();
   }

    /// <summary>
    /// オブジェクトをタップしたら削除する
    /// </summary>
   private void OnEnableTapObject()
    {
        // 左クリックまたは画面タップされた瞬間
        if (Input.GetMouseButtonDown(0))
        {
            // 画面のクリック位置から、3D空間に向かって光線（レイ）を飛ばす
            Ray clickPosition = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 光線が「何か」に当たったか判定
            RaycastHit2D hit = Physics2D.GetRayIntersection(clickPosition);

            // 何かのコライダーに当たったかチェックし、当たったオブジェクトに「BalloonControllerスクリプト」がついていたら削除する
            if (hit.collider != null)
            {
                BalloonController target = hit.collider.GetComponent<BalloonController>();
                if (target != null)
                {
                    target.Fade();
                }
            }
        }
    }

    /// <summary>
    /// リザルト画面のボタンをタップしたらシーン遷移する
    /// </summary>
   private void OnEnableTapButton()
    {
        // 左クリックまたは画面タップされた瞬間
        if (Input.GetMouseButtonDown(0))
        {
            // 画面のクリック位置から、3D空間に向かって光線（レイ）を飛ばす
            Ray clickPosition = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 光線が「何か」に当たったか判定（一番手前のものに当たると止まります）
            RaycastHit2D hit = Physics2D.GetRayIntersection(clickPosition);

            // 何かのコライダーに当たったかチェックし、当たったオブジェクトに「ButtonControllerスクリプト」がついていたらタグを判定してシーン遷移する
            if (hit.collider != null)
            {
                if (SoundManager.Instance != null)
                {
                    SoundManager.Instance.PlayButtonSound();
                }

                ButtonController target = hit.collider.GetComponent<ButtonController>();
                if (target != null)
                {
                    if (hit.collider.CompareTag("GameButton"))
                    {
                        target.GameScene();
                    }

                    if (hit.collider.CompareTag("TitleButton"))
                    {
                        target.TitleScene();
                    }
                }
            }
        }

    }

}
