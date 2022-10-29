using UnityEngine;
using System;
public class Bullet : MonoBehaviour {
    private Transform target;
    private Vector3 shootPosition;
    private float shootRange;
    private Vector3 dir;
    private float shelfLife = 5f;
    [Header ("Ammo Attributes")]

    public float fireSpeed=6f;
    public int damage=45;
    private readonly string tagToDamage = "Enemy";

    [SerializeField] private ParticleSystem collisionParticles;
        public enum OPTIONS{
        farmer,coffee,sushi,laksa,indomie,boba,pizza,korean,sandwich,doughnut
    }
  
    public OPTIONS type;
    private void Start () {
        InvokeRepeating ("ShelfLife",0f,1f);
        ResetBullet();
    }
    public void ResetBullet()
    {
        this.fireSpeed = 7f;
        this.damage = 17;
        // Switch based on the type chosen and assign its respected values
        switch (type)
        {
            // Basic (dps ~50)
            case OPTIONS.farmer:
                fireSpeed = 7f;
                damage = 17;
                break;
            // Rare (dps ~100)
            case OPTIONS.coffee:
                fireSpeed = 10f;
                damage = 50;
                break;
            case OPTIONS.doughnut:
                fireSpeed = 7f;
                damage = 150;
                break;
            case OPTIONS.sandwich:
                fireSpeed = 10f;
                damage = 25;
                break;
            // Super Rare (dps ~150)
            case OPTIONS.korean:
                fireSpeed = 6f;
                damage = 50;
                break;
            case OPTIONS.pizza:
                fireSpeed = 5f;
                damage = 300;
                break;
            case OPTIONS.boba:
                fireSpeed = 6f;
                damage = 75;
                break;
            // Legendary (dps ~210)
            case OPTIONS.indomie:
                fireSpeed = 8f;
                damage = 35;
                break;
            case OPTIONS.laksa:
                fireSpeed = 6f;
                damage = 500;
                break;
            case OPTIONS.sushi:
                fireSpeed = 8f;
                damage = 105;
                break;
        }
    }
    public void ShelfLife(){
        float distance = Vector3.Distance(shootPosition,transform.position);
        if (distance>=shootRange){
            // Debug.Log("FARMER "+Chef.DAMAGE.farmer);
            Destroy(gameObject);
            return;
        }
        if (target==null){
            shelfLife-=1;
        }
        if (shelfLife<=0){
            Destroy(gameObject);
        }
    }
    public void Seek(Transform _target,Vector3 _shootPosition,float _range) {
        target = _target;
        shootPosition = _shootPosition;
        shootRange= _range;
    }
    // Move food based on velocity
    void Update() {
        if (target == null){
            if ((transform.position - shootPosition).magnitude >= shootRange){
                var particles = Instantiate(this.collisionParticles);
                particles.transform.position = transform.position;
                Destroy(gameObject);
                return;
            }
            float distanceThisFrame = fireSpeed * Time.deltaTime;
            transform.Translate (dir.normalized * distanceThisFrame,Space.World);


        } else {
            dir = target.position - transform.position;
            float distanceThisFrame = fireSpeed * Time.deltaTime;
            transform.Translate (dir.normalized * distanceThisFrame,Space.World);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.CompareTag(this.tagToDamage))
        {
            // Damage enemy
            var enemyHealth = col.gameObject.GetComponent<Enemy>();
            enemyHealth.ApplyFood((int)this.damage);
            // Slow if farmer
            if (type == OPTIONS.farmer){
                Debug.Log("slowed");
                col.gameObject.GetComponent<Enemy>().Slow(0.5f,shootPosition);
            }

            // Create collision particles in opposite direction to movement.
            var particles = Instantiate(this.collisionParticles);
            particles.transform.position = transform.position;

            // Destroy item
            Destroy(gameObject);
        }
    }
}