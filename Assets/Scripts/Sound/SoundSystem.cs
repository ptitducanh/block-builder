using System;
using Sound;
using UnityEngine;

public class SoundSystem : MonoBehaviour, ISoundSystem
{
    [SerializeField] private SoundDataSO _soundDataSo;
    [SerializeField] private AudioSource sfxSource;
    private void Awake ()
    {
        ServiceLocator.Register<ISoundSystem>(this);
        _soundDataSo.Init();
    }

    public void PlaySFX(SFX sfx)
    {
        var sfxClip = _soundDataSo.GetClip(sfx);
        if (sfxClip != null)
        {
            sfxSource.PlayOneShot(sfxClip);
        }
    }
}