using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Animation : MonoBehaviour
{
   // Destroys the animation after 0.25 seconds
    void Start()
    {
        Destroy(gameObject, 0.25f);
    }
}
