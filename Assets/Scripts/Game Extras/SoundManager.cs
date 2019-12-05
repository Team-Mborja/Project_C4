using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip click;
    public AudioClip jump;
    public AudioClip explode;

    public void PlaySoundFile(AudioClip audio)
    {
        AudioSource.PlayClipAtPoint(audio, Vector2.zero);
    }

  
}
