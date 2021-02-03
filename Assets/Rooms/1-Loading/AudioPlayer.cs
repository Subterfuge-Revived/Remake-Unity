using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioPlayer : MonoBehaviour
{
    public List<AudioClip> songsToPlay;
    public List<AudioClip> playedSongs;
    public AudioSource audioSource;

    public void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }

    public void Play()
    {
        if (!audioSource.playOnAwake)
        {
            audioSource.clip = songsToPlay[Random.Range(0, songsToPlay.Count)];
            audioSource.Play();
        }
    }

    public void Stop()
    {
        audioSource.Stop();
    }
    
    // Use this for initialization
    void Start ()
    {
        Play();
    }
     
    // Update is called once per frame
    void Update ()
    {
        if (!audioSource.isPlaying)
        {
            if (songsToPlay.Count == 0)
            {
                songsToPlay.AddRange(playedSongs);
                playedSongs.Clear();
            }
            
            audioSource.clip = songsToPlay[Random.Range(0, songsToPlay.Count)];
            audioSource.Play();
        }
    }
}
