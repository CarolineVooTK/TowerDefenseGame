using UnityEngine;
using UnityEngine.Events;

public class EndGame : MonoBehaviour
{
    private readonly string tagToDamage = "Enemy";
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
            GameManager.ReduceHealth(hit.damage);

            if (GameManager.health<=0){
                Time.timeScale = 0f;
                OnEndGame.Invoke();
            }
            Destroy(col);
        }
    }
}
