using UnityEngine;
using UnityEngine.Events;

public class EndGame : MonoBehaviour
{
    private readonly string tagToDamage = "Enemy";
    public int health = 40;
    [SerializeField] private UnityEvent OnEndGame;

    void Update(){
        if (Input.GetKeyDown("e")){
            OnEndGame.Invoke();
        }
    }

    // Check if collide with enemy then attack
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag(this.tagToDamage))
        {
            Enemy hit = col.gameObject.GetComponent<Enemy>();
            health-= hit.damage;
            if (health<=0){
                Time.timeScale = 0f;
                OnEndGame.Invoke();
            }
            Destroy(col);
        }
    }
}
