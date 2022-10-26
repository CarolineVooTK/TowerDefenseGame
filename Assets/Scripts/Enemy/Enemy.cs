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
    public GameObject explosive;

    private int wavePointIndex = 0;
    public Transform target;
    private float _currentHunger;
    private int level;

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
                // Seperate sound effect 
                var soundEffect = Instantiate(this.explosive);
                soundEffect.transform.position = transform.position + new Vector3(0f, 1.6f, 0f);

                // Create collision particles in opposite direction to movement.
                for (int i=0; i<level; i++)
                {
                    var particles = Instantiate(this.collisionParticles);
                    particles.transform.position = transform.position + new Vector3(0f, 2f, 0f);
                }
               
                // Add token from death
                GameManager.AddToken(tokensDropped);
                Destroy(gameObject);
            }
        }
    }

    // Reset values when start
    void Start()
    {
        ResetEnemy();
        target = WayPoints.points[0];
    }

    // Reset the statistics of the enemies
    public void ResetEnemy()
    {
        startingHunger = 50;
        speed = 5;
        tokensDropped = 5;
        level = 1;

        // Switch based on the type chosen and assign its respected values
        switch (type)
        {
            case OPTIONS.averageJoe:
                break;
            case OPTIONS.marathonRunner:
                startingHunger = 70;
                speed *= 1.4f;
                tokensDropped = 15;
                level = 2;
                break;
            case OPTIONS.mukbanger:
                startingHunger = 2500;
                speed *= 0.6f;
                tokensDropped += 20;
                level = 3;
                break;
            case OPTIONS.foodCritic:
                startingHunger = 7000;
                speed *= 0.8f;
                tokensDropped = 1250;
                level = 15;
                break;
            case OPTIONS.sumo:
                startingHunger = 2000;
                speed *= 0.25f;
                tokensDropped = 200;
                level = 5;
                break;
            case OPTIONS.aristocrat:
                startingHunger = 5000;
                speed *= 0.5f;
                tokensDropped = 2000;
                level = 20;
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
        if (target==null){
            target = WayPoints.points[wavePointIndex];
        }
        // Move object to a direction
        Vector3 dir = target.position - transform.position;
        transform.Translate(speed * Time.deltaTime * dir.normalized, Space.World);

        // Rotate object to a direction
        if (dir != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(dir);
            //transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
            transform.rotation = rotation;
        }

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