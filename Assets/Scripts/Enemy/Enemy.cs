using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int startingHunger = 50;
    public double speed = 50;
    public int tokensDropped = 0;
    [SerializeField] private ParticleSystem collisionParticles;

    private int _currentHunger;

    // Check current hunger
    private int CurrentHunger
    {
        get => _currentHunger;
        set
        {
            _currentHunger = value;
            Debug.Log(_currentHunger);

            // destroy object when full
            if (CurrentHunger <= 0)
            {
                // Create collision particles in opposite direction to movement.
                var particles = Instantiate(this.collisionParticles);
                particles.transform.position = transform.position;

                Destroy(gameObject);
            }
        }
    }

    // Reset hunger 
    private void Start()
    {
        ResetHungerToStarting();
    }

    public void ResetHungerToStarting()
    {
        CurrentHunger = startingHunger;
    }

    // Reduce hunger when given food
    public void ApplyFood(int damage)
    {
        CurrentHunger -= damage;
    }
}
