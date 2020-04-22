using UnityEngine;

public class Explosion_Animation : MonoBehaviour
{
    // Sound Manager GameObject
        public SoundManager soundScript;

   // Destroys the animation after 0.2 seconds and play the explosion sound
    void Start()
    {
        soundScript.PlaySoundFile(soundScript.explode);
        Destroy(gameObject, 0.2f);
    }
}
