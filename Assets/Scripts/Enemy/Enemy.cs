using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int startingHunger = 50;
    public float speed = 0.1f;
    public int tokensDropped = 0;
    public float rotationSpeed = 720;
    [SerializeField] private ParticleSystem collisionParticles;
    private int wavePointIndex = 0;
    private Transform target;

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
        target = Waypoints.points[0];
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

    // Move enemy to another waypoint
    private void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        Quaternion rotation = Quaternion.LookRotation(dir - new Vector3(0,-90,0));
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    // Increase wavepoint
    private void GetNextWayPoint()
    {
        if (wavePointIndex >= Waypoints.points.Length - 1)
        {
            Destroy(gameObject);
        }
        else
        {
            wavePointIndex++;
            target = Waypoints.points[wavePointIndex];
        }
    }
}
