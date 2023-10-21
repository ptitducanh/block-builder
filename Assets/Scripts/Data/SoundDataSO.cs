using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sound
{
    [CreateAssetMenu(fileName = "SoundDataSO", menuName = "Data/Sound", order = 0)]
    public class SoundDataSO : ScriptableObject
    {
        [SerializeField] private SoundItemSO[] _soundItems;
        
        private Dictionary<SFX, AudioClip> _soundDictionary = new();
        
        /// <summary>
        /// Load all sound and put it into a dictionary. It'll make it easier to access the sound.
        /// </summary>
        public void Init()
        {
            foreach (var soundItem in _soundItems)
            {
                _soundDictionary.Add(soundItem.Sfx, soundItem.Clip);
            }
        }
        
        public AudioClip GetClip(SFX sfx)
        {
            if (_soundDictionary.TryGetValue(sfx, out var clip))
            {
                return clip;
            }

            return null;
        }
    }

    [Serializable]
    public class SoundItemSO
    {
        public SFX Sfx;
        public AudioClip Clip;
    }
}