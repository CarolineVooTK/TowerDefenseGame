using UnityEngine;

// Class for food to feed enemy
public class ProjectileFood : MonoBehaviour
{
    // Level of food
    public enum OPTIONS
    {
        level1, level2, level3, level4
    }

    public OPTIONS type;
    public float speed = 10f;
    private Vector3 velocity;
    public int damageAmount;
    [SerializeField] private ParticleSystem collisionParticles;

    private readonly string tagToDamage = "Enemy";
    // Reset values when start
    private void Start()
    {
        ResetStats();
    }

    // Reset the statistics of foods
    public void ResetStats()
    {
        velocity = new Vector3(3f, 0, 0);

        // Switch based on the type chosen and assign its respected values
        switch (type)
        {
            case OPTIONS.level1:
                break;
            case OPTIONS.level2:
                velocity *= 1.25f;
                damageAmount *= 2;
                break;
            case OPTIONS.level3:
                velocity *= 1.5f;
                damageAmount *= 3;
                break;
            case OPTIONS.level4:
                velocity *= 1.75f;
                damageAmount *= 4;
                break;
        }
    }

    // Move food based on velocity
    private void Update()
    {
        // float distanceThisFrame = speed * Time.deltaTime;
        // transform.Translate(this.velocity.normalized * distanceThisFrame);
    }

    // Check if collide with enemy then attack
    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.CompareTag(this.tagToDamage))
        {
            // Damage enemy
            var enemyHealth = col.gameObject.GetComponent<Enemy>();
            enemyHealth.ApplyFood(this.damageAmount);

            // Create collision particles in opposite direction to movement.
            var particles = Instantiate(this.collisionParticles);
            particles.transform.position = transform.position;

            // Destroy item
            Destroy(gameObject);
        }
    }
}
