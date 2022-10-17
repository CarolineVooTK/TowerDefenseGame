using System;
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
    public int waveNum = 0;

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

        // for (int i = 0; i < waveNum; i++)
        // {
        Debug.Log(waveNum);
        GenerateWaveFor(waveNum);
        waveNum++;
            Instantiate(this.e_averageJoe, point.position, point.rotation);

            // yield return new WaitForSeconds(0.5f);
        // }
        yield return new WaitForSeconds(2f);
    }

    // Generate enemy based on waves
    private IEnumerator GenerateJoe(int multiple, float gap)
    {
        for (int i = 0; i < multiple; i++){
            Instantiate(this.e_averageJoe, point.position, point.rotation);
            yield return new WaitForSeconds(gap);
        }
    }
    private IEnumerator GenerateMarathonRunner(int multiple, float gap)
    {
        for (int i = 0; i < multiple; i++){
            Instantiate(this.e_marathonRunner, point.position, point.rotation);
            yield return new WaitForSeconds(gap);
        }
    }
    private IEnumerator GenerateMukBanger(int multiple, float gap)
    {
        for (int i = 0; i < multiple; i++){
            Instantiate(this.e_mukbanger, point.position, point.rotation);
            yield return new WaitForSeconds(gap);
        }
    }
    private IEnumerator GenerateSumo(int multiple, float gap)
    {
        for (int i = 0; i < multiple; i++){
            Instantiate(this.e_sumo, point.position, point.rotation);
            yield return new WaitForSeconds(gap);
        }
    }
    private IEnumerator GenerateCritic(int multiple, float gap)
    {
        for (int i = 0; i < multiple; i++){
            Instantiate(this.e_foodCritic, point.position, point.rotation);
            yield return new WaitForSeconds(gap);
        }
    }
    private IEnumerator GenerateAristocrat(int multiple, float gap)
    {
        for (int i = 0; i < multiple; i++){
            Instantiate(this.e_aristocrat, point.position, point.rotation);
            yield return new WaitForSeconds(gap);
        }
    }
    private IEnumerator GenerateWaveFor(int wave){
        switch (wave)
        {
            case (0):
                GenerateJoe(20, 0.5f);
                break;
            case (1):
                GenerateJoe(30, 0.3f);
                break;            
            case (2):
                GenerateJoe(7, 0.3f);
                GenerateMarathonRunner(2, 0.5f);
                GenerateJoe(7, 0.5f);
                GenerateMarathonRunner(2, 0.5f);
                GenerateJoe(7, 0.3f);
                break;            
            case (3):
                GenerateSumo(1, 0.3f);
                GenerateJoe(7, 0.5f);
                GenerateMarathonRunner(2, 0.5f);
                GenerateSumo(1, 0.3f);
                GenerateJoe(7, 0.3f);
                GenerateMarathonRunner(2, 0.5f);
                break;                  
            default:
                int rand;
                int rand2;
                for (int i = 0; i < waveNum; i++)
                {
                    rand = UnityEngine.Random.Range(0,6);
                    rand2 = UnityEngine.Random.Range(0,waveNum+5);
                    switch (rand){
                        case(0):
                            GenerateJoe((int)Math.Ceiling(rand2/3.0f),0.4f);
                            break;
                        case(1):
                            GenerateSumo((int)Math.Ceiling(rand2/3.0f)/3,0.4f);
                            break;
                        case(2):
                            GenerateMarathonRunner((int)Math.Ceiling(rand2/3.0f)/3,0.4f);
                            break;
                        case(3):
                            GenerateMukBanger((int)Math.Ceiling(rand2/3.0f)/3,0.4f);
                            break;
                        case(4):
                            GenerateAristocrat((int)Math.Ceiling(rand2/3.0f)/3,0.4f);
                            break;
                        case(5):
                            GenerateCritic((int)Math.Ceiling(rand2/3.0f)/3,0.4f);
                            break;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
                break;
        }
    }
}
