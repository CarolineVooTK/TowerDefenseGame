using UnityEngine;

// Class for creating objects of enemies according to waves
public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject e_averageJoe;
    [SerializeField] private GameObject e_marathonRunner;
    [SerializeField] private GameObject e_mukbanger;
    [SerializeField] private GameObject e_sumo;
    [SerializeField] private GameObject e_foodCritic;
    [SerializeField] private GameObject e_aristocrat;

    // Generate enemies when start
    private void Start()
    {
        GenerateEnemy();

        transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    // Generate enemy based on waves
    private void GenerateEnemy()
    {
        var enemyTransform = Instantiate(this.e_averageJoe).transform;
        enemyTransform.transform.position = transform.position;
    }
}
