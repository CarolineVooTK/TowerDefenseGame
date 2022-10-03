using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyTemplate;

    private void Start()
    {
        GenerateEnemy();

        transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    private void GenerateEnemy()
    {
        var enemyTransform = Instantiate(this.enemyTemplate).transform;
    }
}
