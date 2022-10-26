using UnityEngine;

// Class to play audio when loaded 
public class PlayAudio : MonoBehaviour
{
    public AudioSource explosion;

    // When awake, play audio then destroy game object
    public void Awake()
    {
        explosion.Play();
        Destroy(gameObject,explosion.clip.length);
    }
}
