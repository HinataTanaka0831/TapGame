using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip popSound; // 風船が割れる音

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

    // Update is called once per frame
    void Update()
    {
        
    }

    // 風船が割れたときの効果音再生
    public void PlayPopSound()
    {
        if (audioSource != null && popSound != null)
        {
            audioSource.PlayOneShot(popSound);
        }
    }

}
