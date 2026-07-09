using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject objectPrefab;
    [SerializeField] private float spawnTime = 1.5f;
    [SerializeField] private float deleteSpeed = 0.0f;
    private float delta = 0f;
    private int lastIndex_x = 0;
    private int lastIndex_y = 0;

    /// <summary>
    /// 更新  オブジェクトが生成される間隔を管理し、ランダムな位置に風船を生成する。
    /// </summary>
    void Update()
    {
        // 経過時間を計算し、指定した感覚でオブジェクトを生成する
        this.delta += Time.deltaTime;
        if (this.delta > this.spawnTime)
        {
            this.delta = 0f;

            // オブジェクトが生成される効果音を再生する
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlayAppearSound();
            }

            GameObject go = Instantiate(objectPrefab);

            // オブジェクトの位置をランダムに決定する（前回と同じ位置には生成しない）
            // 一定間隔で生成されたオブジェクトを自動で削除する
            int prefabPosition_x;
            int prefabPosition_y;
            do
            {
                prefabPosition_x = Random.Range(-1, 3);
                prefabPosition_y = Random.Range(-4, 4);
            } while (prefabPosition_x == lastIndex_x && prefabPosition_y == lastIndex_y);
            
            lastIndex_x = prefabPosition_x;
            lastIndex_y = prefabPosition_y;

            go.transform.position = new Vector2((float)prefabPosition_x, (float)prefabPosition_y);

            Destroy(go, deleteSpeed);
        }
    }
}
