using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Class for creating objects of enemies according to waves
public class EnemyManager : MonoBehaviour
{
    // 10
    [SerializeField] private GameObject e_averageJoe;
    // 20
    [SerializeField] private GameObject e_marathonRunner;
    // 40
    [SerializeField] private GameObject e_mukbanger;
    // 80
    [SerializeField] private GameObject e_sumo;
    // 200
    [SerializeField] private GameObject e_foodCritic;
    // 300
    [SerializeField] private GameObject e_aristocrat;

    Dictionary<int,  int> enemyList = new Dictionary<int,int>();
    public int enemyWaveCost = 150;
    public int waveTime = 15;
    public Transform point;

    private float countdown = 5;

    private enum enemy{
        Joe,Cindy,Sumo,MukBang,Aristocrat,Critic
    };

    private void start(){
        // Debug.Log(enemyList["averageJoe"]);
    }
    // Generate enemies when update

    private void updateEnemyAvailability(int waveNum){
        if (waveNum == 0 ){
            enemyList.Add((int)enemy.Joe,10);
        }
        if (waveNum == 3 ){
            enemyList.Add((int)enemy.Cindy,20);
        }
        if (waveNum == 5 ){
            enemyList.Add((int)enemy.Sumo,80);
        }
        if (waveNum == 10 ){
            enemyList.Add((int)enemy.MukBang,100);
        }
        if (waveNum == 20 ){
            enemyList.Add((int)enemy.Aristocrat,200);
        }
        if (waveNum == 30 ){
            enemyList.Add((int)enemy.Critic,300);
        }
    }
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
        updateEnemyAvailability(GameManager.waveNum);
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

    private IEnumerator GenerateRandom(int wave)
    {
        int cost = wave * (100) + GameManager.totalEarned*(wave)*20;
        while (cost > 0){
            int rand;
            int rand2;
            rand = UnityEngine.Random.Range(0, enemyList.Count);
            rand2 = UnityEngine.Random.Range(0, GameManager.waveNum + 5);
            if (enemyList[rand]*rand2-cost>0){
                switch (rand)
                {
                    case (0):
                        yield return StartCoroutine(GenerateJoe((int)Math.Ceiling(rand2 / 3.0f), 0.4f));
                        break;
                    case (1):
                        yield return StartCoroutine(GenerateMarathonRunner((int)Math.Ceiling(rand2 / 3.0f) / 3, 0.4f));
                        break;
                    case (2):
                        yield return StartCoroutine(GenerateSumo((int)Math.Ceiling(rand2 / 3.0f) / 3, 0.4f));
                        break;
                    case (3):
                        yield return StartCoroutine(GenerateMukBanger((int)Math.Ceiling(rand2 / 3.0f) / 3, 0.4f));
                        break;
                    case (4):
                        yield return StartCoroutine(GenerateAristocrat((int)Math.Ceiling(rand2 / 3.0f) / 3, 0.4f));
                        break;
                    case (5):
                        yield return StartCoroutine(GenerateCritic((int)Math.Ceiling(rand2 / 3.0f) / 3, 0.4f));
                        break;
                }
                cost-=enemyList[rand]*rand2;
            }
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
                yield return StartCoroutine(GenerateJoe(7, 0.3f));
                break;            
            case (3):
                yield return  StartCoroutine(GenerateSumo(1, 0.3f));
                yield return  StartCoroutine(GenerateJoe(7, 0.5f));
                yield return  StartCoroutine(GenerateMarathonRunner(2, 0.5f));
                yield return  StartCoroutine(GenerateSumo(1, 0.3f));
                yield return  StartCoroutine(GenerateJoe(7, 0.3f));
                yield return  StartCoroutine(GenerateMarathonRunner(2, 0.5f));
                break;
            default:
                yield return StartCoroutine(GenerateRandom(wave));
                break;
        }

        yield return new WaitForSeconds(3f);
    }
}
