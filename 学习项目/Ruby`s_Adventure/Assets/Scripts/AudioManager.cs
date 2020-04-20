using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 播放音乐音效
/// </summary>
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    private AudioSource audioS;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;
        audioS = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 播放指定的音效
    /// </summary>
    /// <param name="clip"></param>
    public void AudioPlay(AudioClip clip)
    {
        audioS.PlayOneShot(clip);
    }
}
