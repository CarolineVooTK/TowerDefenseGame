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
    Dictionary<int,  float> enemySpacingList = new Dictionary<int,float>();
    public int enemyWaveCost = 150;
    public int waveTime = 10;
    public Transform point;

    private float countdown = 5;
    private enum enemy{
        Joe,Cindy,Sumo,MukBang,Aristocrat,Critic
    };
    private enum spacing{
        Wide,Close,Closer,SuperTight
    };

    private void start(){
        enemySpacingList.Add((int)spacing.Wide,0.6f);
        enemySpacingList.Add((int)spacing.Close,0.4f);
        enemySpacingList.Add((int)spacing.Closer,0.3f);
        enemyList.Add((int)enemy.Joe,10);
    }
    // Generate enemies when update

    private void updateEnemyAvailability(int waveNum){
        if (waveNum == 3 ){
            enemyList.Add((int)enemy.Cindy,20);
        }
        if (waveNum == 5 ){
            enemyList.Add((int)enemy.Sumo,80);
        }
        if (waveNum == 10 ){
            enemyList.Add((int)enemy.MukBang,100);
        }
        if (waveNum == 15 ){
            enemyList.Add((int)enemy.Aristocrat,200);
        }
        if (waveNum == 20 ){
            enemyList.Add((int)enemy.Critic,300);
        }
    }
    
    private void updateEnemySpacing(int waveNum){
        if (waveNum == 10 ){
            enemySpacingList.Add((int)spacing.SuperTight,0.1f);
        }
    }
    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (countdown <= 0 && enemies.Length==0)
        {
            updateEnemyAvailability(GameManager.waveNum);
            updateEnemySpacing(GameManager.waveNum);
            StartCoroutine(GenerateWave());
            countdown = waveTime;
        }

        countdown -= Time.deltaTime;
        transform.localPosition = new Vector3(0f, 0f, 0f);
    }

    // Generate enemies per wave
    private IEnumerator GenerateWave()
    {
        Debug.Log("NUMBER  WAVE " + (GameManager.waveNum+1));
        yield return StartCoroutine(GenerateWaveFor(GameManager.waveNum));
        yield return new WaitForSeconds(5f);
        StopAllCoroutines();
        GameManager.NextWave();

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
            case (0): // total credits == 1000
                yield return StartCoroutine(GenerateJoe(10, 1f));
                break;
            case (1):
                yield return StartCoroutine(GenerateJoe(15, 0.5f));
                break;            
            case (2):
                yield return StartCoroutine(GenerateJoe(7, 0.3f));
                yield return StartCoroutine(GenerateMarathonRunner(2, 0.5f));
                yield return StartCoroutine(GenerateJoe(7, 0.3f));
                break;            
            case (3):
                yield return  StartCoroutine(GenerateJoe(7, 0.5f));
                yield return  StartCoroutine(GenerateMarathonRunner(2, 0.5f));
                yield return  StartCoroutine(GenerateJoe(7, 0.3f));
                yield return  StartCoroutine(GenerateMarathonRunner(2, 0.5f));
                break;
            case (4):
                yield return  StartCoroutine(GenerateJoe(10, 0.3f));
                yield return  StartCoroutine(GenerateSumo(1, 0.6f));
                yield return  StartCoroutine(GenerateJoe(10, 0.3f));
                break;
            case (5):
                yield return  StartCoroutine(GenerateSumo(1, 0.3f));
                yield return  StartCoroutine(GenerateJoe(10, 0.5f));
                yield return  StartCoroutine(GenerateMarathonRunner(7, 0.5f));
                yield return  StartCoroutine(GenerateSumo(1, 0.3f));
                yield return  StartCoroutine(GenerateJoe(10, 0.3f));
                yield return  StartCoroutine(GenerateMarathonRunner(10, 0.5f));
                break;
            case (6):
                yield return  StartCoroutine(GenerateMarathonRunner(10, 0.3f));
                yield return  StartCoroutine(GenerateSumo(1, 0.4f));
                yield return  StartCoroutine(GenerateJoe(20, 0.2f));
                yield return  StartCoroutine(GenerateSumo(1, 0.4f));
                yield return  StartCoroutine(GenerateMarathonRunner(10, 0.5f));
                yield return  StartCoroutine(GenerateJoe(10, 0.2f));
                yield return  StartCoroutine(GenerateSumo(2, 0.6f));
                yield return  StartCoroutine(GenerateMarathonRunner(10, 0.8f));
                break;
            case (10):
                yield return  StartCoroutine(GenerateMukBanger(5, 1f));
                yield return  StartCoroutine(GenerateSumo(3, 0.4f));
                yield return  StartCoroutine(GenerateJoe(20, 0.2f));
                yield return  StartCoroutine(GenerateMarathonRunner(10, 0.5f));
                yield return  StartCoroutine(GenerateJoe(10, 0.2f));
                yield return  StartCoroutine(GenerateSumo(3, 0.6f));
                yield return  StartCoroutine(GenerateMarathonRunner(10, 0.8f));
                break;
            default:
                int cost = wave * (250);
                int rand;
                int rand2;
                while (cost > 0){
                    rand = UnityEngine.Random.Range(0, enemyList.Count);
                    rand2 = UnityEngine.Random.Range(0, enemySpacingList.Count);
                    Debug.Log(cost + "IS LEFTTTTTTTT");
                    if ((cost-500)>0){
                        switch (rand)
                        {
                            case (0):
                                yield return GenerateJoe(15+wave, enemySpacingList[rand2]);
                                break;
                            case (1):
                                yield return GenerateMarathonRunner(7+((int)Math.Ceiling((double)wave/3)), enemySpacingList[rand2]);
                                break;
                            case (2):
                                yield return GenerateSumo(1 +((int)Math.Ceiling((double)wave/4)), enemySpacingList[rand2]);
                                break;
                            case (3):
                                yield return GenerateMukBanger(1+((int)Math.Ceiling((double)wave/4)), enemySpacingList[rand2]);
                                break;
                            case (4):
                                yield return GenerateAristocrat(1+((int)Math.Ceiling((double)wave/4)), enemySpacingList[rand2]);
                                break;
                            case (5):
                                yield return GenerateCritic(1+((int)Math.Ceiling((double)wave/4)), enemySpacingList[rand2]);
                                break;
                        }
                    }
                    cost-=500;
                }
                break;
        }

        yield return new WaitForSeconds(3f);
    }
}
