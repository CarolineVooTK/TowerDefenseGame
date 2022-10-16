using UnityEngine;
using System;
public class Bullet : MonoBehaviour {
    private Transform target;
    private Vector3 shootPosition;
    private float shootRange;
    private Vector3 dir;
    private float shelfLife = 3f;
    public float speed = 3f;
    // Level of food

    [SerializeField] private ParticleSystem collisionParticles;
    void Start () {
        InvokeRepeating ("ShelfLife",0f,1f);
    }
    public void ShelfLife(){
        if (shelfLife<1){
            Debug.Log("DELETED");
            Destroy(gameObject);
            return;
        }
        shelfLife-=1;
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
            float distanceThisFrame = speed * Time.deltaTime;
            transform.Translate (dir.normalized * distanceThisFrame,Space.World);


        } else {
            dir = target.position - transform.position;
            float distanceThisFrame = speed * Time.deltaTime;

            if (dir.magnitude <= distanceThisFrame){
                HitTarget();
                return;
            }
            transform.Translate (dir.normalized * distanceThisFrame,Space.World);

        }



    }


 

    void HitTarget(){
        // Damage enemy
        // var enemyHealth = target.gameObject.GetComponent<Enemy>();
        // enemyHealth.ApplyFood(this.damageAmount);

        // // Create collision particles in opposite direction to movement.
        // var particles = Instantiate(this.collisionParticles);
        // particles.transform.position = transform.position;

        // Destroy item
        Destroy(gameObject);
    }


}