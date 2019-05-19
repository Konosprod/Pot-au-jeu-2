﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum SoundType
{
    None,
    Platformer,
    Upgrade,
    Metamorphosis,
    End

};

public enum SFXType
{
    None,
    Rain
};

public class SoundManager : MonoBehaviour
{

    public AudioMixer masterMixer;

    public AudioSource bgmSource;
    public AudioSource sfxSource;

    private static Dictionary<SoundType, AudioClip> audioClips;
    private static Dictionary<SFXType, AudioClip> sfxClips;

    private SoundType actuallyPlaying;

    public static SoundManager _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        audioClips = new Dictionary<SoundType, AudioClip>();
        sfxClips = new Dictionary<SFXType, AudioClip>();

        foreach (SoundType t in Enum.GetValues(typeof(SoundType)))
        {
            if (t != SoundType.None)
                audioClips[t] = Resources.Load<AudioClip>("Sounds/" + t.ToString());
        }

        foreach (SFXType t in Enum.GetValues(typeof(SFXType)))
        {
            sfxClips[t] = Resources.Load<AudioClip>("Sounds/SFX/" + t.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopMusic()
    {
        actuallyPlaying = SoundType.None;
        bgmSource.Stop();
    }

    public void PlayMusic(SoundType type, bool forceReplay = false)
    {
        Debug.Log("here");
        if (forceReplay && type == actuallyPlaying)
        {
            bgmSource.Stop();
            bgmSource.loop = true;
            bgmSource.clip = audioClips[type];
            bgmSource.Play();
        }

        if (!forceReplay && type == actuallyPlaying)
        {
            Debug.Log("wat");
            return;
        }
        bgmSource.Stop();
        bgmSource.loop = true;
        bgmSource.clip = audioClips[type];
        bgmSource.Play();

        actuallyPlaying = type;
    }

    public void PlayMusic(string name)
    {
        SoundType t = (SoundType)Enum.Parse(typeof(SoundType), name);
        PlayMusic(t);
    }

    public void PlaySFX(SFXType type)
    {
        Debug.Log(type);
        sfxSource.pitch = UnityEngine.Random.Range(0.9f, 1.1f);
        sfxSource.PlayOneShot(sfxClips[type]);
    }

    public void PlaySFX(string name)
    {
        SFXType t = (SFXType)Enum.Parse(typeof(SFXType), name);
        PlaySFX(t);
    }

    public void SetSFXVolume(float volume)
    {
        masterMixer.SetFloat("SFXVolume", volume);
    }

    public void SetBGMVolume(float volume)
    {
        masterMixer.SetFloat("BGMVolume", volume);
    }

    public void SetMasterVolume(float volume)
    {
        masterMixer.SetFloat("MasterVolume", volume);
    }

    public void MuteBGM()
    {
        masterMixer.SetFloat("BGMVolume", -80.0f);
    }

    public void MuteSFX()
    {
        masterMixer.SetFloat("SFXVolume", -80.0f);
    }
}