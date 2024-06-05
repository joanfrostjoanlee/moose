using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicController : MonoBehaviour
{
    AudioSource musicSource;
    bool gameStart;

    void Start()
    {
        musicSource = gameObject.GetComponent<AudioSource>();
        musicSource.volume = 0f;
        gameStart = true;
    }
    private void Update()
    {
        if (gameStart)
            VolumeFadeIn();
    }

    private void VolumeFadeIn()
    {
        if (musicSource.volume < 1f)
            musicSource.volume = Mathf.Lerp(musicSource.volume, 1f, Time.deltaTime * 0.5f);
        else 
            gameStart = false;
    }
}
