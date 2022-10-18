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

    public float waveTime = 15;
    public Transform point;

    private float countdown = 5;

    // Generate enemies when update
    private void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(GenerateWave());
            countdown = waveTime * (GameManager.waveNum + 1);
        }

        countdown -= Time.deltaTime;
        transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    // Generate enemies per wave
    private IEnumerator GenerateWave()
    {
        Debug.Log("NUMBER  WAVE " + GameManager.waveNum);
        yield return StartCoroutine(GenerateWaveFor(GameManager.waveNum));
        GameManager.NextWave();
        yield return new WaitForSeconds(1f);
        StopAllCoroutines();
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
                yield return StartCoroutine(GenerateJoe(10, 1f));
                break;
            case (1):
                yield return StartCoroutine(GenerateJoe(20, 0.5f));
                break;            
            case (2):
                yield return StartCoroutine(GenerateJoe(7, 0.3f));
                yield return StartCoroutine(GenerateMarathonRunner(2, 0.5f));
                yield return StartCoroutine(GenerateJoe(7, 0.5f));
                yield return StartCoroutine(GenerateMarathonRunner(2, 0.5f));
                yield return StartCoroutine(GenerateJoe(7, 0.3f));
                break;            
            case (3):
                yield return GenerateSumo(1, 0.3f);
                yield return GenerateJoe(7, 0.5f);
                yield return GenerateMarathonRunner(2, 0.5f);
                yield return GenerateSumo(1, 0.3f);
                yield return GenerateJoe(7, 0.3f);
                yield return GenerateMarathonRunner(2, 0.5f);
                break;
            default:
                int rand;
                int rand2;
                for (int i = 0; i < GameManager.waveNum; i++)
                {
                    rand = UnityEngine.Random.Range(0, 6);
                    rand2 = UnityEngine.Random.Range(0, GameManager.waveNum + 5);
                    switch (rand)
                    {
                        case (0):
                            yield return GenerateJoe((int)Math.Ceiling(rand2 / 3.0f), 0.4f);
                            break;
                        case (1):
                            yield return GenerateSumo((int)Math.Ceiling(rand2 / 3.0f) / 3, 0.4f);
                            break;
                        case (2):
                            yield return GenerateMarathonRunner((int)Math.Ceiling(rand2 / 3.0f) / 3, 0.4f);
                            break;
                        case (3):
                            yield return GenerateMukBanger((int)Math.Ceiling(rand2 / 3.0f) / 3, 0.4f);
                            break;
                        case (4):
                            yield return GenerateAristocrat((int)Math.Ceiling(rand2 / 3.0f) / 3, 0.4f);
                            break;
                        case (5):
                            yield return GenerateCritic((int)Math.Ceiling(rand2 / 3.0f) / 3, 0.4f);
                            break;
                    }
                    yield return new WaitForSeconds(0.5f);
                }
                break;
        }

        yield return new WaitForSeconds(3f);
    }
}
