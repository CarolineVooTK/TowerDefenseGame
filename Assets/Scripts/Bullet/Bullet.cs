using UnityEngine;
using System;
public class Bullet : MonoBehaviour {
    private Transform target;
    private Vector3 shootPosition;
    private float shootRange;
    private Vector3 dir;
    public float speed = 7f;
    // Level of food

    [SerializeField] private ParticleSystem collisionParticles;

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