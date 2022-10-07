using UnityEngine;
using System.Collections;

public class HitSplash : MonoBehaviour
{
    [SerializeField] private ParticleSystem targetParticleSystem;

    private void Update()
    {
        // Unfortunately there is no event-based API for particle systems so we
        // have to poll whether the system is alive on every update.
        if (!this.targetParticleSystem.IsAlive())
            Destroy(this.targetParticleSystem.gameObject);
    }
}
