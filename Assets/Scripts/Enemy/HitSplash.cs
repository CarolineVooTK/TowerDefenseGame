using UnityEngine;

// Class for particle system when enemy is full
public class HitSplash : MonoBehaviour
{
    [SerializeField] private ParticleSystem targetParticleSystem;

    // Check if particle system is on
    private void Update()
    {
        // Unfortunately there is no event-based API for particle systems so we
        // have to poll whether the system is alive on every update.
        if (!this.targetParticleSystem.IsAlive())
            Destroy(this.targetParticleSystem.gameObject);
    }
}
