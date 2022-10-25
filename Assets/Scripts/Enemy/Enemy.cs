using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// Class for enemy object
public class Enemy : MonoBehaviour
{
    // Types of enemies
    public enum OPTIONS
    {
        averageJoe, marathonRunner, mukbanger, foodCritic, sumo, aristocrat
    }

    public OPTIONS type;
    public float startingHunger;
    public float speed;
    public int tokensDropped;
    public float rotationSpeed;
    [SerializeField] private ParticleSystem collisionParticles;

    private int wavePointIndex = 0;
    private Transform target;
    private float _currentHunger;

    // Check current hunger
    private float CurrentHunger
    {
        get => _currentHunger;
        set
        {
            _currentHunger = value;

            // Destroy object when full
            if (CurrentHunger <= 0)
            {
                // Create collision particles in opposite direction to movement.
                var particles = Instantiate(this.collisionParticles);
                particles.transform.position = transform.position + new Vector3(0f, 1.6f, 0f);


                // Add token from death
                GameManager.AddToken(tokensDropped);
                Destroy(gameObject);
            }
        }
    }

    // Reset values when start
    private void Start()
    {
        ResetEnemy();
        target = WayPoints.points[0];
    }

    // Reset the statistics of the enemies
    public void ResetEnemy()
    {
        startingHunger = 30;
        speed = 5;
        tokensDropped = 5;

        // Switch based on the type chosen and assign its respected values
        switch (type)
        {
            case OPTIONS.averageJoe:
                break;
            case OPTIONS.marathonRunner:
                startingHunger *= 0.8f;
                speed *= 1.4f;
                tokensDropped += 2;
                break;
            case OPTIONS.mukbanger:
                startingHunger *= 2f;
                speed *= 0.9f;
                tokensDropped += 20;
                break;
            case OPTIONS.foodCritic:
                startingHunger *= 2.5f;
                speed *= 0.8f;
                tokensDropped += 25;
                break;
            case OPTIONS.sumo:
                startingHunger *= 10f;
                speed *= 0.25f;
                tokensDropped += 70;
                break;
            case OPTIONS.aristocrat:
                startingHunger *= 3.5f;
                speed *= 0.5f;
                tokensDropped += 60;
                break;
        }

        rotationSpeed = 720;
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
        // Move object to a direction
        Vector3 dir = target.position - transform.position;
        transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);

        // Rotate object to a direction
        Quaternion rotation = Quaternion.LookRotation(dir);
        //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
        transform.rotation = rotation;

        // If distance is close as 0.2, assume it already reach the waypoint and update
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    // Increase wavepoint
    private void GetNextWayPoint()
    {
        // When reach max of wavepoint, destroy item
        if (wavePointIndex >= WayPoints.points.Length - 1)
        {
            Destroy(gameObject);
        }
        else
        {
            wavePointIndex++;
            target = WayPoints.points[wavePointIndex];
        }
    }
}