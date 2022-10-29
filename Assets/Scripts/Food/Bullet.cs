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
    private float slowpct=0f;

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
                slowpct=0.5f;
                break;
            // Rare (dps ~100)
            case OPTIONS.coffee:
                fireSpeed = 10f;
                damage = 50;
                slowpct=0.2f;
                break;
            case OPTIONS.doughnut:
                fireSpeed = 7f;
                damage = 150;
                slowpct=0.7f;
                break;
            case OPTIONS.sandwich:
                fireSpeed = 10f;
                damage = 25;
                slowpct=0.4f;
                break;
            // Super Rare (dps ~150)
            case OPTIONS.korean:
                fireSpeed = 6f;
                damage = 50;
                slowpct=0.4f;
                break;
            case OPTIONS.pizza:
                fireSpeed = 5f;
                damage = 300;
                slowpct=0.7f;
                break;
            case OPTIONS.boba:
                fireSpeed = 6f;
                damage = 75;
                slowpct=0.2f;
                break;
            // Legendary (dps ~210)
            case OPTIONS.indomie:
                fireSpeed = 8f;
                damage = 35;
                slowpct=0.6f;
                break;
            case OPTIONS.laksa:
                fireSpeed = 6f;
                damage = 500;
                slowpct=0.6f;
                break;
            case OPTIONS.sushi:
                fireSpeed = 8f;
                damage = 105;
                slowpct=0.6f;
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
                Enemy enemy = col.gameObject.GetComponent<Enemy>();
                if (enemy.type==Enemy.OPTIONS.averageJoe) enemy.Slow(slowpct,shootPosition);
            }
            if (type == OPTIONS.coffee){
                Enemy enemy = col.gameObject.GetComponent<Enemy>();
                if (enemy.type==Enemy.OPTIONS.marathonRunner||enemy.type==Enemy.OPTIONS.aristocrat) enemy.Slow(slowpct,shootPosition);
            }
            if (type == OPTIONS.boba||type == OPTIONS.korean){
                Enemy enemy = col.gameObject.GetComponent<Enemy>();
                if (enemy.type==Enemy.OPTIONS.mukbanger) enemy.Slow(slowpct,shootPosition);
            }
            if (type == OPTIONS.pizza||type == OPTIONS.korean){
                Enemy enemy = col.gameObject.GetComponent<Enemy>();
                if (enemy.type==Enemy.OPTIONS.sumo||enemy.type==Enemy.OPTIONS.foodCritic) enemy.Slow(slowpct,shootPosition);
            }
            if (type == OPTIONS.laksa){
                col.gameObject.GetComponent<Enemy>().Slow(slowpct,shootPosition,true);
            }
            if (type == OPTIONS.sushi){
                col.gameObject.GetComponent<Enemy>().Slow(slowpct,shootPosition,true);
            }
            if (type == OPTIONS.indomie){
                col.gameObject.GetComponent<Enemy>().Slow(slowpct,shootPosition,true);
            }

            // Create collision particles in opposite direction to movement.
            var particles = Instantiate(this.collisionParticles);
            particles.transform.position = transform.position;

            // Destroy item
            Destroy(gameObject);
        }
    }
}