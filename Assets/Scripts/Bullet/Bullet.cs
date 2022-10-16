using UnityEngine;
public class Bullet : MonoBehaviour {
    private Transform target;
    public float speed = 7f;
    // Level of food
    public enum OPTIONS
    {
        level1, level2, level3, level4
    }

    public OPTIONS type;
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
                speed *= 1.25f;
                damageAmount *= 2;
                break;
            case OPTIONS.level3:
                speed *= 1.5f;
                damageAmount *= 3;
                break;
            case OPTIONS.level4:
                speed *= 1.75f;
                damageAmount *= 4;
                break;
        }
    }
    public void Seek(Transform _target) {
        target = _target;
    }
    // Move food based on velocity
    void Update() {
        if (target == null){
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame){
            HitTarget();
            return;
        }
        transform.Translate (dir.normalized * distanceThisFrame,Space.World);


    }

    // Check if collide with enemy then attack
    private void OnTriggerEnter(Collider col)
    {
        

    }


 

    void HitTarget(){
        // Damage enemy
        var enemyHealth = target.gameObject.GetComponent<Enemy>();
        enemyHealth.ApplyFood(this.damageAmount);

        // Create collision particles in opposite direction to movement.
        var particles = Instantiate(this.collisionParticles);
        particles.transform.position = transform.position;

        // Destroy item
        Destroy(gameObject);
    }


}