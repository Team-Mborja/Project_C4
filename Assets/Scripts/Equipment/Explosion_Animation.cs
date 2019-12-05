using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Animation : MonoBehaviour
{
    public SoundManager soundScript;

   // Destroys the animation after 0.25 seconds
    void Start()
    {
        soundScript.PlaySoundFile(soundScript.explode);
        Destroy(gameObject, 0.2f);
    }
}
