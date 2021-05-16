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
    private static AudioClip currentTrack;

    private static Dictionary<String, String> songDictionary = new Dictionary<String, String>();

    // Use this for initialization
    void Start ()
    {
        songDictionary.Add("Glass_Bubbles", "Glass Bubbles by Cakemix");
        songDictionary.Add("Lagrange_Orbit", "Lagrange Orbit by Cakemix");
        songDictionary.Add("Subterfuge", "Subterfuge by Cakemix");
        
        Play();
    }
    
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
            currentTrack = songsToPlay[Random.Range(0, songsToPlay.Count)];
            audioSource.Play();
        }
    }

    public void Stop()
    {
        audioSource.Stop();
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
            currentTrack = songsToPlay[Random.Range(0, songsToPlay.Count)];
            audioSource.Play();
        }
    }

    public static String getSongName()
    {
        return songDictionary[currentTrack.name];
    }
}
