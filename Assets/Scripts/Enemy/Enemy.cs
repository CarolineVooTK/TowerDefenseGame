using UnityEngine;

// Class for enemy object
public class Enemy : MonoBehaviour
{
    // Types of enemies
    public enum OPTIONS
    {
        averageJoe, marathonRunner, mukbanger, foodCritic, sumo, aristocrat
    }

    public OPTIONS type;
    public int startingHunger;
    public float speed;
    public int tokensDropped;
    public float rotationSpeed;
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

            // Destroy object when full
            if (CurrentHunger <= 0)
            {
                // Create collision particles in opposite direction to movement.
                var particles = Instantiate(this.collisionParticles);
                particles.transform.position = transform.position;

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
        startingHunger = 50;
        speed = 5;
        tokensDropped = 1;

        // Switch based on the type chosen and assign its respected values
        switch (type)
        {
            case OPTIONS.averageJoe:
                break;
            case OPTIONS.marathonRunner:
                startingHunger *= 2;
                speed *= 3;
                tokensDropped *= 1;
                break;
            case OPTIONS.mukbanger:
                startingHunger *= 2;
                speed *= 3;
                tokensDropped *= 1;
                break;
            case OPTIONS.foodCritic:
                startingHunger *= 2;
                speed *= 3;
                tokensDropped *= 1;
                break;
            case OPTIONS.sumo:
                startingHunger *= 2;
                speed *= 3;
                tokensDropped *= 1;
                break;
            case OPTIONS.aristocrat:
                startingHunger *= 2;
                speed *= 3;
                tokensDropped *= 1;
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
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);

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