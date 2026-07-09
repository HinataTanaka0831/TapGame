using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance { get; private set; }

    private CanvasGroup canvasGroup;
    private bool isFading = false;


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
            InitFadeCanvas();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// フェード用の黒い画面（Canvas）をプログラムから自動生成する
    /// </summary>
    private void InitFadeCanvas()
    {
        // キャンバスの作成
        GameObject canvasObj = new GameObject("FadeCanvas");
        canvasObj.transform.SetParent(transform);

        Canvas canvas = canvasObj.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 999; // 最前面に表示されるようにする

        canvasObj.AddComponent<CanvasScaler>();
        canvasObj.AddComponent<GraphicRaycaster>();

        // 全面を覆う黒い画像の作成
        GameObject imageObj = new GameObject("FadeImage");
        imageObj.transform.SetParent(canvasObj.transform);

        Image image = imageObj.AddComponent<Image>();
        image.color = Color.black;

        // 画像のサイズを画面全体に広げる
        RectTransform rectTransform = image.rectTransform;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchoredPosition = Vector2.zero;

        // 透明度を制御するためのCanvasGroupを追加
        canvasGroup = canvasObj.AddComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false; // 通常時は背後のオブジェクトをタップ可能にする
    }

    // シーン遷移を呼ぶためのパブリックメソッド
    public void LoadScene(string sceneName, float fadeTime = 0.5f)
    {
        if (isFading) return;
        StartCoroutine(FadeSequence(sceneName, fadeTime));
    }

    /// <summary>
    /// 複数フレームにまたがってフェードを実行する
    /// </summary>
    /// <param name="sceneName">各シーンの名前</param>
    /// <param name="fadeTime">フェードアニメーションをする時間</param>
    /// <returns></returns>
    private IEnumerator FadeSequence(string sceneName, float fadeTime)
    {
        isFading = true;
        canvasGroup.blocksRaycasts = true; // フェード中は背後へのタップを無効化する

        // 1. フェードアウト (画面が徐々に黒くなる)
        float timer = 0f;
        while (timer < fadeTime)
        {
            timer += Time.unscaledDeltaTime; // タイムスケールに依存しない時間計測
            canvasGroup.alpha = Mathf.Clamp01(timer / fadeTime);
            yield return null;
        }
        canvasGroup.alpha = 1f;

        // 2. 非同期でシーンをロードする
        yield return SceneManager.LoadSceneAsync(sceneName);

        // ロード完了後、少しだけ待つ（一瞬で切り替わるのを防ぐため）
        yield return new WaitForSecondsRealtime(0.1f);

        // 3. フェードイン (画面が徐々に明るくなる)
        timer = 0f;
        while (timer < fadeTime)
        {
            timer += Time.unscaledDeltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1f - (timer / fadeTime));
            yield return null;
        }
        canvasGroup.alpha = 0f;

        canvasGroup.blocksRaycasts = false; // 背後へのタップを再び有効化
        isFading = false;
    }
}
