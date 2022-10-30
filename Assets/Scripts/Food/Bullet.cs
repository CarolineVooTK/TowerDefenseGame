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
    public float explosionRadius=0f;
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
            case OPTIONS.coffee: // fast
                fireSpeed = 10f;
                damage = 30;
                slowpct=0.2f;
                break;
            case OPTIONS.doughnut: // slow aoe
                fireSpeed = 7f;
                damage = 30;
                slowpct=0.7f;
                explosionRadius=3f;
                break;
            case OPTIONS.sandwich: // normal
                fireSpeed = 10f;
                damage = 55;
                slowpct=0.4f;
                break;
            // Super Rare (dps ~150)
            case OPTIONS.korean: // normal
                fireSpeed = 8f;
                damage = 70;
                slowpct=0.4f;
                break;
            case OPTIONS.pizza: // slow aoe
                fireSpeed = 7f;
                damage = 75;
                slowpct=0.7f;
                explosionRadius=6f;
                break;
            case OPTIONS.boba: // fast
                fireSpeed = 6f;
                damage = 35;
                slowpct=0.2f;
                break;
            // Legendary (dps ~210)
            case OPTIONS.indomie: // fast
                fireSpeed = 8f;
                damage = 45;
                slowpct=0.6f;
                break;
            case OPTIONS.laksa: // slow aoe
                fireSpeed = 6f;
                damage = 200;
                slowpct=0.6f;
                explosionRadius=8f;
                break;
            case OPTIONS.sushi: // normal
                fireSpeed = 8f;
                damage = 175;
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
            var enemy = col.gameObject.GetComponent<Enemy>();
            // Slow if farmer
            if (explosionRadius >0){
                Explode();
            } else{
                enemy.ApplyFood((int)this.damage);
                ApplySlow(enemy);
            }
            // Create collision particles in opposite direction to movement.
            var particles = Instantiate(this.collisionParticles);
            particles.transform.position = transform.position;

            // Destroy item
            Destroy(gameObject);
        }
    }
    void ApplySlow(Enemy enemy){
        if (type == OPTIONS.farmer){
                if (enemy.type==Enemy.OPTIONS.averageJoe) enemy.Slow(slowpct,shootPosition);
            }
            if (type == OPTIONS.coffee){
                if (enemy.type==Enemy.OPTIONS.marathonRunner||enemy.type==Enemy.OPTIONS.aristocrat) enemy.Slow(slowpct,shootPosition);
            }
            if (type == OPTIONS.boba||type == OPTIONS.korean){
                if (enemy.type==Enemy.OPTIONS.mukbanger) enemy.Slow(slowpct,shootPosition);
            }
            if (type == OPTIONS.pizza||type == OPTIONS.korean){
                if (enemy.type==Enemy.OPTIONS.sumo||enemy.type==Enemy.OPTIONS.foodCritic) enemy.Slow(slowpct,shootPosition);
            }
            if (type == OPTIONS.laksa){
                enemy.Slow(slowpct,shootPosition,true);
            }
            if (type == OPTIONS.sushi){
                enemy.Slow(slowpct,shootPosition,true);
            }
            if (type == OPTIONS.indomie){
                enemy.Slow(slowpct,shootPosition,true);
            }

    }
    void Explode(){
        Collider[] collider = Physics.OverlapSphere(transform.position,explosionRadius);
        foreach(Collider collider1 in collider){
            if (collider1.tag == "Enemy"){
                Enemy enemy = collider1.gameObject.GetComponent<Enemy>();
                enemy.ApplyFood((int)this.damage);
                ApplySlow(enemy);
            }
        }
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}