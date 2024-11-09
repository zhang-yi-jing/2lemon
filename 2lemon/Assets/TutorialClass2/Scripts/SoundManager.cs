using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;

    public Sound[] sounds;  

    private  Dictionary<string,AudioSource> audioSourcesDic;//audioSourcesDic 是一个私有的字典，用于存储声音名称和对应的AudioSource组件。

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSourcesDic = new Dictionary<string, AudioSource>();

        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundGameObject = new GameObject("Sound_" + i + "_" + sounds[i].name);
            soundGameObject.transform.SetParent(this.transform);
            sounds[i].SetSource(soundGameObject.AddComponent<AudioSource>());
            audioSourcesDic.Add(sounds[i].name, soundGameObject.GetComponent<AudioSource>());
        }

        // 订阅死亡事件
        EventCenter.Instance.Subscribe("DeathEvent", PlayerDeathSound);
    }

    public void Play(string soundName, float volume = 1, bool loop = false)
    {
        AudioSource source = audioSourcesDic[soundName];
        if (!source.isPlaying)
        {
            source.volume = volume;
            source.loop = loop;
            source.Play();
        }
    }

    public void Pause(string soundName)
    {
        AudioSource source = audioSourcesDic[soundName];
        source.Pause();
    }

    public void UnPause(string soundName)
    {
        AudioSource source = audioSourcesDic[soundName];
        source.UnPause();
    }

    public void SetVolume(string soundName, float volume)
    {
        AudioSource source = audioSourcesDic[soundName];
        source.volume = volume;
    }

    public void Stop(string soundName)
    {
        AudioSource source = audioSourcesDic[soundName];
        source.Stop();
    }

    public void StopAll()
    {
        foreach (var audioSource in audioSourcesDic.Values)
        {
            audioSource.Stop();
        }
    }

    public void PlayerDeathSound()
    {
        SoundManager.Instance.Play("PlayerDeath");
    }

}

[System.Serializable]
public class Sound
{
    public AudioClip clip;
    public string name;

    public float volume = 1;
    public bool loop = false;

    public Sound(string name, AudioClip clip)
    {
        this.name = name;
        this.clip = clip;
    }

    public void SetSource(AudioSource source)
    {
        source.clip = clip;
        source.volume = volume;
        source.loop = loop;
    }

    

}