using UnityEngine;
using System.Collections;

public class DestroyOffScreen : MonoBehaviour
{
    // Triggered as soon as the object is outside of the camera frustum.
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
