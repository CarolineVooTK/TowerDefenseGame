using UnityEngine;

// Class for particle system when enemy is full
public class HitSplash : MonoBehaviour
{
    [SerializeField] private ParticleSystem targetParticleSystem;

    // Check if particle system is on
    private void Update()
    {
        if (!this.targetParticleSystem.IsAlive())
            Destroy(this.targetParticleSystem.gameObject);
    }
}
