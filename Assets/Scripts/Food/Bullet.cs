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
    public float changedExplosionRadius;
    
    public float changedDamage;
    public float damage;
    private readonly string tagToDamage = "Enemy";
    private float slowpct;
    public int level;

    [SerializeField] private ParticleSystem collisionParticles;
        public enum OPTIONS{
        farmer,coffee,sushi,laksa,indomie,boba,pizza,korean,sandwich,doughnut
    }
  
    public OPTIONS type;
    private void Start () {
        InvokeRepeating ("ShelfLife",0f,1f);
        LevelMultiplier();
    }
    public void LevelMultiplier(){
        if (level==1) {
            changedDamage=damage;
            changedExplosionRadius=explosionRadius;
            return;
        }
        changedDamage=damage*(1.5f*((float)(level-1)));
        changedExplosionRadius = explosionRadius*(1.3f*((float)(level-1)));
    }
    public void ResetBullet()
    {
        this.fireSpeed = 7f;
        this.damage = 17f;
        this.slowpct=0f;
        this.level=1;
        // Switch based on the type chosen and assign its respected values
        switch (type)
        {
            // Basic (dps ~50)
            case OPTIONS.farmer:
                fireSpeed = 7f;
                damage = 17f;
                slowpct=0.5f;
                break;
            // Rare (dps ~100)
            case OPTIONS.coffee: // fast
                fireSpeed = 10f;
                damage = 30f;
                slowpct=0.2f;
                break;
            case OPTIONS.doughnut: // slow aoe
                fireSpeed = 7f;
                damage = 50f;
                slowpct=0.7f;
                explosionRadius=3f;
                break;
            case OPTIONS.sandwich: // normal
                fireSpeed = 10f;
                damage = 55f;
                slowpct=0.4f;
                break;
            // Super Rare (dps ~150)
            case OPTIONS.korean: // normal
                fireSpeed = 8f;
                damage = 70f;
                slowpct=0.4f;
                break;
            case OPTIONS.pizza: // slow aoe
                fireSpeed = 7f;
                damage = 85f;
                slowpct=0.7f;
                explosionRadius=6f;
                break;
            case OPTIONS.boba: // fast
                fireSpeed = 6f;
                damage = 35f;
                slowpct=0.2f;
                break;
            // Legendary (dps ~210)
            case OPTIONS.indomie: // fast
                fireSpeed = 8f;
                damage = 45f;
                slowpct=0.6f;
                break;
            case OPTIONS.laksa: // slow aoe
                fireSpeed = 6f;
                damage = 200f;
                slowpct=0.6f;
                explosionRadius=8f;
                break;
            case OPTIONS.sushi: // normal
                fireSpeed = 8f;
                damage = 175f;
                slowpct=0.6f;
                break;
        }
    }
    public void ShelfLife(){
        float distance = Vector3.Distance(shootPosition,transform.position);
        if (distance>=shootRange){
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
    public void Seek(Transform _target,Vector3 _shootPosition,float _range,int _level) {
        ResetBullet();
        target = _target;
        shootPosition = _shootPosition;
        shootRange= _range;
        level =_level;
        LevelMultiplier();

    }
    // Move food based on velocity
    void Update() {
        LevelMultiplier();
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
            LevelMultiplier();
            // Damage enemy
            var enemy = col.gameObject.GetComponent<Enemy>();
            // Slow if farmer
            if (changedExplosionRadius >0){
                Explode();
            } else{
                enemy.ApplyFood((int)changedDamage);
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
                enemy.ApplyFood((int)changedDamage);
                ApplySlow(enemy);
            }
        }
    }
    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }
}