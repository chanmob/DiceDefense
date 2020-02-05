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

    /*----------------[PUBLIC METHOD]------------------------------*/

    public void PlaySound(AudioType type, AudioClip clip)
    {
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

    public void VolumeOnOff(bool on)
    {
        if(on)
            AudioListener.volume = 1;

        else
            AudioListener.volume = 0;
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

    protected override void Awake()
    {
        base.Awake();

        _bgm_AudioSource = transform.Find("BGMAudio").GetComponent<AudioSource>();
        _sfx_AudioSource = transform.Find("SFXAudio").GetComponent<AudioSource>();
    }
}