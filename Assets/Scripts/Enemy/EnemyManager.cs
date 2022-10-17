using System.Collections;
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

    public float waveTime = 10;
    public Transform point;
    public int waveNum = 3;

    private float countdown = 10;

    // Generate enemies when update
    private void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(GenerateWave());
            countdown = waveTime;
        }

        countdown -= Time.deltaTime;
        transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    // Generate enemies per wave
    private IEnumerator GenerateWave()
    {
        waveNum++;

        for (int i = 0; i < waveNum; i++)
        {
            GenerateEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(2f);
    }

    // Generate enemy based on waves
    private void GenerateEnemy()
    {
        //var enemyTransform = Instantiate(this.e_averageJoe).transform;
        //enemyTransform.transform.position = transform.position;

        Instantiate(this.e_averageJoe, point.position, point.rotation);
    }
}
