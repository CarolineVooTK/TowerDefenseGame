using UnityEngine;
using System.Collections;
public class Chef : MonoBehaviour{
    public Transform target;

    [Header ("Attribute")]
    public float range = 15f;
    public float fireRate = 1f;
    public float fireCountdown = 0f;
    [Header ("UnitySetup")]
    public string enemyTag = "Enemy";
    public Transform partToRotate;
    public GameObject bulletPrefab;
    public GameObject bulletPrefab2;
    public GameObject bulletPrefab3;
    public Transform firePoint;


    void Start () {
        InvokeRepeating ("UpdateTarget",0f,0.5f);
    }

    void UpdateTarget () {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position,enemy.transform.position);    
            if (distance < shortestDistance){
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


        if (fireCountdown <= 0f){
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
    void Shoot()
    {   

        // GameObject[] bullets = new GameObject[(GameObject)Instantiate(bulletPrefab, firePoint.position,firePoint.rotation),
        //                             (GameObject)Instantiate(bulletPrefab2, firePoint.position,firePoint.rotation),
        //                             (GameObject)Instantiate(bulletPrefab3, firePoint.position,firePoint.rotation)]; 
        int index=0;
        if (bulletPrefab2 == null && bulletPrefab3 == null) index=0;
        if (bulletPrefab3 == null && bulletPrefab3)  index = Random.Range(0,1);
        if (bulletPrefab2 && bulletPrefab3) index = Random.Range(0,2);

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
    void OnDrawGizmoSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);

    }
}