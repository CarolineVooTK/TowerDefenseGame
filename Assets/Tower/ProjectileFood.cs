using UnityEngine;
using System.Collections;

public class ProjectileFood : MonoBehaviour
{
    [SerializeField] private Vector3 velocity;

    [SerializeField] private int damageAmount = 50;
    [SerializeField] private string tagToDamage = "Enemy";

    private void Update()
    {
        transform.Translate(this.velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider col)
    {
        
        if (col.gameObject.CompareTag(this.tagToDamage))
        {
            // Damage object with relevant tag. Note that this assumes the 
            // HealthManager component is attached to the respective object.
            var enemyHealth = col.gameObject.GetComponent<Enemy>();
            enemyHealth.ApplyFood(this.damageAmount);

            // Destroy self.
            Destroy(gameObject);
        }
    }
}
