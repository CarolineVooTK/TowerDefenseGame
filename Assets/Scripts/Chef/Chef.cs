using UnityEngine;
using System.Collections;
public class Chef : MonoBehaviour{

    public Transform target;

    [Header ("Chef Attributes")]
    
    // characters range
    public float range;
    // bullets per second
    public float fireRate;
    public int sellAmount;
   
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
    public int baseUpgradeAmount;
    public int upgradeAmount;
    public int level;
    public bool fullyUpgraded;
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
        this.sellAmount = 50;
        this.level = 1;
        this.fullyUpgraded=false;
        // Switch based on the type chosen and assign its respected values
        switch (type)
        {
            // Basic (dps ~50)
            case OPTIONS.farmer:
                // Attributes
                range = 12f;
                fireRate = 3f;
                fireCountdown = 2f;
                baseUpgradeAmount = 25;
                break;
            // Rare (dps ~100)
            case OPTIONS.coffee: // fast
                // Attributes
                range = 15f;
                fireRate = 1.5f;
                fireCountdown = 1f;
                sellAmount = 100;
                baseUpgradeAmount = 25;

                break;
            case OPTIONS.doughnut: // slow aoe
                // Attributes
                range = 15f;
                fireRate = 0.9f;
                fireCountdown = 1f;
                sellAmount = 100;
                baseUpgradeAmount = 25;

                break;
            case OPTIONS.sandwich: // normal
                // Attributes
                range = 15f;
                fireRate = 1.2f;
                fireCountdown = 1f;
                sellAmount = 100;
                baseUpgradeAmount = 25;

                break;
            // Super Rare (dps ~150)
            case OPTIONS.korean: // normal
                // Attributes
                range = 25f;
                fireRate = 1.4f;
                fireCountdown = 2f;
                sellAmount = 125;
                baseUpgradeAmount = 25;
                break;
            case OPTIONS.pizza: // slow aoe
                // Attributes
                range = 20f;
                fireRate = 0.6f;
                fireCountdown = 1f;
                sellAmount = 125;
                baseUpgradeAmount = 25;

                break;
            case OPTIONS.boba: // fast
                // Attributes
                range = 23f;
                fireRate = 2f;
                fireCountdown = 1f;
                sellAmount = 125;
                baseUpgradeAmount = 25;
                break;
            // Legendary (dps ~210)
            case OPTIONS.indomie: // fast
                // Attributes
                range = 25f;
                fireRate = 3f;
                fireCountdown = 0f;
                sellAmount = 200;
                baseUpgradeAmount = 25;
                break;
            case OPTIONS.laksa: // slow aoe
                // Attributes
                range = 35f;
                fireRate = 0.4f;
                fireCountdown = 1f;
                sellAmount = 200;
                baseUpgradeAmount = 25;
                break;
            case OPTIONS.sushi: // normal
                // Attributes
                range = 30f;
                fireRate = 1.6f;
                fireCountdown = 4f;
                sellAmount = 200;
                baseUpgradeAmount = 25;
                break;
        }
        this.upgradeAmount=(int)Mathf.Ceil(sellAmount/2.0f);
    }

    // Updates the current target if within range
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

    // Updates look direction and shoot
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

    // Shoots bullet, if more then one ammo randomly decide
    void Shoot()
    {   

        int index=0;
        if (bulletPrefab2 == null && bulletPrefab3 == null) index=0;
        if (bulletPrefab3 == null && bulletPrefab2)  index = Random.Range(0,1);
        if (bulletPrefab2 && bulletPrefab3) index = Random.Range(0,3);
        switch(index){
            // bullet 2;
            case 1:
            GameObject bullet2 = Instantiate(bulletPrefab2, firePoint.position,firePoint.rotation);
            bullet2.GetComponent<Bullet>().Seek(target,transform.position,range,level);
            return;
            // bullet 3;
            case 2:
            GameObject bullet3 = Instantiate(bulletPrefab3, firePoint.position,firePoint.rotation);
            bullet3.GetComponent<Bullet>().Seek(target,transform.position,range,level);
            return;
            // bullet 1;
            default:
            GameObject bullet1 = Instantiate(bulletPrefab, firePoint.position,firePoint.rotation);
            bullet1.GetComponent<Bullet>().Seek(target,transform.position,range,level);
           
            return;
        }
    }
    // shows radius of range;
    void OnDrawGizmosSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
    // returns sell amount;
    public int GetSellAmount(){
        if (level ==1) return sellAmount;
        return sellAmount + baseUpgradeAmount*level;
    }
    // returns upgrade cost;
    public int GetUpgradeAmount(){
        if (level ==1) return sellAmount + baseUpgradeAmount*level;
        return sellAmount+baseUpgradeAmount*(level*3);
    }
    
    // Adds upgrade effects;
    public void Upgrade(){
        switch (level){
            case (1):
                level++;
                fireRate *= 1.3f;
                break;
            case (2):
                range *= 1.3f;
                level++;
                break;
            case (3):
                level++;
                fireRate *= 1.3f;
                fireCountdown = 0f;
                break;
            case (4):
                level++;
                fireRate *= 1.3f;
                range *= 1.3f;
                fullyUpgraded = true;
                break;
            default:
                fullyUpgraded = true;
                break;
        }
        upgradeAmount=sellAmount+baseUpgradeAmount*level;
    }

}   