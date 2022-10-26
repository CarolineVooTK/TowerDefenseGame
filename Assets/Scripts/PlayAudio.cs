using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource explosion;
    public void Awake()
    {
        explosion.Play();
        Destroy(gameObject,explosion.clip.length);
    }
}
