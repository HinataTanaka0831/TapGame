using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonGenerator : MonoBehaviour
{
    [SerializeField] private GameObject balloonPrefab;
    [SerializeField] private float span = 1.5f;
    [SerializeField] private float deleteSpeed = 0.0f;
    private float delta = 0f;
    private int lastIndex_x = 0;
    private int lastIndex_y = 0;

    // Update is called once per frame
    private void Update()
    {
        this.delta += Time.deltaTime;
        if (this.delta > this.span)
        {
            this.delta = 0f;
            GameObject go = Instantiate(balloonPrefab);

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
