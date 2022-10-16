using UnityEngine;
using System;
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
                Debug.Log("shooot");

            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }
    void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position,firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if (bullet!=null){
            bullet.Seek(target);
        }
    }
    void OnDrawGizmoSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);

    }
}