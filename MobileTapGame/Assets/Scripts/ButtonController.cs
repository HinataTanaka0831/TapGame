using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour
{
    /// <summary>
    /// 「Game」に遷移する
    /// </summary>
   public void GameScene()
   {
        // フェードして画面遷移する
        if (FadeManager.Instance != null)
         {
              FadeManager.Instance.LoadScene("Game");
         }
         else
         {
              SceneManager.LoadScene("Game");
         }
   }

    /// <summary>
    /// 「Title」に遷移する
    /// </summary>
    public void TitleScene()
    {
        // フェードして画面遷移する
        if (FadeManager.Instance != null)
        {
            FadeManager.Instance.LoadScene("Title");
        }
        else
        {
            SceneManager.LoadScene("Title");
        }
    }
}
