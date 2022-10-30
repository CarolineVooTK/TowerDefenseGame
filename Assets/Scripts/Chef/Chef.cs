using UnityEngine;
using System.Collections;
public class Chef : MonoBehaviour{

    public Transform target;

    [Header ("Chef Attributes")]
    
    // characters range
    public float range;
    // bullets per second
    public float fireRate;
   
    // shoooting interval
    public float fireCountdown;
    [Header ("Ammo Types")]
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    
    [Header ("UnitySetup")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public Transform firePoint;
    public float slowAmount = 0.5f;
    public enum OPTIONS{
        farmer,coffee,sushi,laksa,indomie,boba,pizza,korean,sandwich,doughnut
    }
  
    public OPTIONS type;
    void Start () {
        InvokeRepeating ("UpdateTarget",0f,0.5f);
        ResetChef();
    }
public void ResetChef()
    {
        this.range = 15f;
        this.fireRate = 3f;
        this.fireCountdown = 0f;
        // Switch based on the type chosen and assign its respected values
        switch (type)
        {
            // Basic (dps ~50)
            case OPTIONS.farmer:
                // Attributes
                range = 12f;
                fireRate = 3f;
                fireCountdown = 2f;
                break;
            // Rare (dps ~100)
            case OPTIONS.coffee: // fast
                // Attributes
                range = 15f;
                fireRate = 1.5f;
                fireCountdown = 1f;
                break;
            case OPTIONS.doughnut: // slow aoe
                // Attributes
                range = 15f;
                fireRate = 0.9f;
                fireCountdown = 1f;
                break;
            case OPTIONS.sandwich: // normal
                // Attributes
                range = 15f;
                fireRate = 1.2f;
                fireCountdown = 1f;
                break;
            // Super Rare (dps ~150)
            case OPTIONS.korean: // normal
                // Attributes
                range = 20f;
                fireRate = 1.4f;
                fireCountdown = 2f;
                break;
            case OPTIONS.pizza: // slow aoe
                // Attributes
                range = 25f;
                fireRate = 0.6f;
                fireCountdown = 1f;
                break;
            case OPTIONS.boba: // fast
                // Attributes
                range = 20f;
                fireRate = 2f;
                fireCountdown = 1f;
                break;
            // Legendary (dps ~210)
            case OPTIONS.indomie: // fast
                // Attributes
                range = 30f;
                fireRate = 3f;
                fireCountdown = 0f;
                break;
            case OPTIONS.laksa: // slow aoe
                // Attributes
                range = 35f;
                fireRate = 0.4f;
                fireCountdown = 1f;
                break;
            case OPTIONS.sushi: // normal
                // Attributes
                range = 25f;
                fireRate = 1.6f;
                fireCountdown = 4f;
                break;
        }
    }
    void UpdateTarget () {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position,enemy.transform.position);    
            if (distance < shortestDistance && distance <=range){
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <=range){
            target = nearestEnemy.transform;
        }
    }

    void Update() {
        if (target == null) return;
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = lookRotation.eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f,rotation.y,0f); 

        float distance = Vector3.Distance(transform.position,target.position);    

        if (distance > range){
            target=null;
        }

        if (fireCountdown <= 0f){
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
    void Shoot()
    {   

        int index=0;
        if (bulletPrefab2 == null && bulletPrefab3 == null) index=0;
        if (bulletPrefab3 == null && bulletPrefab3)  index = Random.Range(0,1);
        if (bulletPrefab2 && bulletPrefab3) index = Random.Range(0,3);

        switch(index){
            case 1:
            GameObject bullet2 = Instantiate(bulletPrefab2, firePoint.position,firePoint.rotation);
            bullet2.GetComponent<Bullet>().Seek(target,transform.position,range);
            // bullet;
            return;
            case 2:
            GameObject bullet3 = Instantiate(bulletPrefab3, firePoint.position,firePoint.rotation);
            bullet3.GetComponent<Bullet>().Seek(target,transform.position,range);
            // bullet;
            return;
            default:
            GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position,firePoint.rotation);
            bullet1.GetComponent<Bullet>().Seek(target,transform.position,range);
            // bullet;
           
            return;
        }

        // if (bullet!=null){
        // }
    }
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);

    }
}   