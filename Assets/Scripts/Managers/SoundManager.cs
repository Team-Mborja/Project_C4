using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip click;
    public AudioClip jump;
    public AudioClip explode;
    public AudioClip laser;
    public AudioClip footstep;
    public AudioClip landing;
    public AudioClip grenade_tick;
    public AudioClip c4_stick;

    public void PlaySoundFile(AudioClip audio)
    {
        AudioSource.PlayClipAtPoint(audio, Vector2.zero);
    }

  
}
