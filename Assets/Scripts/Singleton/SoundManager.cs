/*
  ============================================
	Author	: ChanMob
	Time 	: 2020-02-04 오후 4:58:50
  ============================================
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    /* [PUBLIC VARIABLE]					*/

    public enum AudioType
    {
        BGM,
        SFX,
        None
    }

    /* [PROTECTED && PRIVATE VARIABLE]		*/

    private AudioSource _bgm_AudioSource;
    private AudioSource _sfx_AudioSource;

    private Dictionary<string, AudioClip> _clips = new Dictionary<string, AudioClip>();

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void PlaySound(AudioType type, string clipName)
    {
        if (_clips.ContainsKey(clipName) == false)
        {
            Debug.Log("오디오 클립 존재하지 않음");
            return;
        }

        AudioClip clip = _clips[clipName];

        switch (type)
        {
            case AudioType.BGM:
                _bgm_AudioSource.clip = clip;
                _bgm_AudioSource.Play();
                break;
            case AudioType.SFX:
                _sfx_AudioSource.PlayOneShot(clip);
                break;
        }
    }

    public void VolumeOnOff()
    {
        if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
        }
        else
        {
            AudioListener.volume = 1;
        }
    }

    public void SetVoulme(AudioType type, float volume)
    {
        switch (type)
        {
            case AudioType.BGM:
                _bgm_AudioSource.volume = volume;
                break;
            case AudioType.SFX:
                _sfx_AudioSource.volume = volume;
                break;
        }
    }
    
    public void StopBGM()
    {
        _bgm_AudioSource.Stop();
    }

    /*----------------[PROTECTED && PRIVATE METHOD]----------------*/

    protected override void OnAwake()
    {
        _bgm_AudioSource = transform.Find("BGMAudio").GetComponent<AudioSource>();
        _sfx_AudioSource = transform.Find("SFXAudio").GetComponent<AudioSource>();

        InitClips();
    }

    private void InitClips()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds");

        int len = clips.Length;
        for (int i = 0; i < len; i++)
        {
            AudioClip clip = clips[i];
            _clips.Add(clip.name, clip);
        }
    }
}