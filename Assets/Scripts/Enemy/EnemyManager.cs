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
    public float currentCost=100;
    public float prevWaveCost=100;

    public Transform point;

    private float countdown = 5;
    private enum enemy{
        Joe,Cindy,Sumo,MukBang,Critic,Aristocrat
    };
    private enum spacing{
        Wide,Close,Closer,SuperTight
    };
    public Transform ParentEnemy;

    // Adds enemy spacing to roster
    private void Start(){
        enemySpacingList.Add((int)spacing.Wide,0.6f);
        enemySpacingList.Add((int)spacing.Close,0.4f);
        enemySpacingList.Add((int)spacing.Closer,0.3f);
        enemySpacingList.Add((int)spacing.SuperTight,0.1f);

    }
    
    
    // Adds new enemies to roster
    private void updateEnemy(int waveNum){
        if (GameManager.waveNum == 0 ) enemyList.TryAdd((int)enemy.Joe,10);
        if (GameManager.waveNum == 3 ) enemyList.TryAdd((int)enemy.Cindy,20);
        if (GameManager.waveNum == 5 ) enemyList.TryAdd((int)enemy.Sumo,200);
        if (GameManager.waveNum == 10 ) enemyList.TryAdd((int)enemy.MukBang,250);
        if (GameManager.waveNum == 15 ) enemyList.TryAdd((int)enemy.Critic,500);
        if (GameManager.waveNum == 20) enemyList.TryAdd((int)enemy.Aristocrat,1000);
    }

    // Manages waves
    private void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (countdown <= 0 && enemies.Length==0)
        {
            updateEnemy(GameManager.waveNum);
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

    // Generate Joe
    private IEnumerator GenerateJoe(int multiple, float gap)
    {
        for (int i = 0; i < multiple; i++){
            Instantiate(this.e_averageJoe, point.position, point.rotation, ParentEnemy);
            yield return new WaitForSeconds(gap);
        }
    }

    // Generate Cindy
    private IEnumerator GenerateMarathonRunner(int multiple, float gap)
    {
        for (int i = 0; i < multiple; i++){
            Instantiate(this.e_marathonRunner, point.position, point.rotation, ParentEnemy);
            yield return new WaitForSeconds(gap);
        }
    }

    // Generate Kim Jung Duos
    private IEnumerator GenerateMukBanger(int multiple, float gap)
    {
        for (int i = 0; i < multiple; i++){
            Instantiate(this.e_mukbanger, point.position, point.rotation, ParentEnemy);
            yield return new WaitForSeconds(gap);
        }
    }

    // Generate MIYAMA
    private IEnumerator GenerateSumo(int multiple, float gap)
    {
        for (int i = 0; i < multiple; i++){
            Instantiate(this.e_sumo, point.position, point.rotation, ParentEnemy);
            yield return new WaitForSeconds(gap);
        }
    }

    // Generate ratatioulie
    private IEnumerator GenerateCritic(int multiple, float gap)
    {
        for (int i = 0; i < multiple; i++){
            Instantiate(this.e_foodCritic, point.position, point.rotation, ParentEnemy);
            yield return new WaitForSeconds(gap);
        }
    }

    // Generate Bill fences
    private IEnumerator GenerateAristocrat(int multiple, float gap)
    {
        for (int i = 0; i < multiple; i++){
            Instantiate(this.e_aristocrat, point.position, point.rotation, ParentEnemy);
            yield return new WaitForSeconds(gap);
        }
    }


    // Generate waves + procedural wave generation
    private IEnumerator GenerateWaveFor(int wave){
        switch (wave)
        {
            case (0): // total credits == 1000
                yield return StartCoroutine(GenerateJoe(10, 1.5f));
                prevWaveCost=100;
                break;
            case (1):
                yield return StartCoroutine(GenerateJoe(7, 0.6f));
                yield return new WaitForSeconds(0.5f);
                yield return StartCoroutine(GenerateJoe(7, 0.6f));

                prevWaveCost=250;
                break;            
            case (2):
                yield return StartCoroutine(GenerateMarathonRunner(7, 1f));
                yield return StartCoroutine(GenerateJoe(10, 0.2f));
                prevWaveCost=250;
                break;            
            case (3):
                yield return  StartCoroutine(GenerateMarathonRunner(3, 0.5f));
                yield return  StartCoroutine(GenerateJoe(10, 0.3f));
                yield return  StartCoroutine(GenerateMarathonRunner(3, 0.5f));
                yield return  StartCoroutine(GenerateJoe(10, 0.3f));
                break;
            case (4):
                yield return  StartCoroutine(GenerateJoe(10, 0.5f));
                yield return  StartCoroutine(GenerateMarathonRunner(5, 1f));
                yield return  StartCoroutine(GenerateJoe(5, 0.3f));
                yield return  StartCoroutine(GenerateMarathonRunner(5, 1f));
                break;
            case (5):
                yield return  StartCoroutine(GenerateJoe(5, 0.2f));
                yield return  StartCoroutine(GenerateMarathonRunner(7, 0.3f));
                yield return  StartCoroutine(GenerateJoe(15, 0.2f));
                yield return  StartCoroutine(GenerateMarathonRunner(7, 0.3f));
                break;
            case (6):
                yield return  StartCoroutine(GenerateSumo(1, 0.4f));
                yield return  StartCoroutine(GenerateJoe(10, 0.3f));
                yield return new WaitForSeconds(0.5f);
                yield return  StartCoroutine(GenerateJoe(10, 0.3f));

                break;
            case (7):
                yield return  StartCoroutine(GenerateSumo(1, 0.4f));
                yield return  StartCoroutine(GenerateMarathonRunner(7, 0.7f));
                yield return  StartCoroutine(GenerateJoe(10, 0.3f));
                yield return  StartCoroutine(GenerateMarathonRunner(7, 0.7f));
                yield return  StartCoroutine(GenerateJoe(10, 0.5f));
                yield return  StartCoroutine(GenerateSumo(1, 0.4f));
                yield return  StartCoroutine(GenerateJoe(10, 0.3f));
                prevWaveCost=3000;
                break;
            case (10):
                yield return  StartCoroutine(GenerateMukBanger(1, 1f));
                yield return  StartCoroutine(GenerateJoe(20, 1f));
                yield return  StartCoroutine(GenerateSumo(3, 2f));
                yield return  StartCoroutine(GenerateJoe(20, 0.2f));
                yield return  StartCoroutine(GenerateMarathonRunner(10, 0.5f));
                yield return  StartCoroutine(GenerateJoe(10, 0.2f));
                yield return  StartCoroutine(GenerateSumo(2, 2f));
                yield return  StartCoroutine(GenerateMarathonRunner(10, 0.8f));
                prevWaveCost= 4000;
                break;
            case (15):
                yield return  StartCoroutine(GenerateCritic(1, 1f));
                yield return  StartCoroutine(GenerateMukBanger(3, 1f));
                yield return  StartCoroutine(GenerateSumo(3, 1f));
                yield return  StartCoroutine(GenerateJoe(20, 0.2f));
                yield return  StartCoroutine(GenerateSumo(3, 1f));
                yield return  StartCoroutine(GenerateMarathonRunner(10, 0.5f));
                yield return  StartCoroutine(GenerateSumo(3, 1f));
                yield return  StartCoroutine(GenerateJoe(10, 0.2f));
                yield return  StartCoroutine(GenerateCritic(1, 1f));
                yield return  StartCoroutine(GenerateSumo(3, 0.6f));
                yield return  StartCoroutine(GenerateMarathonRunner(10, 0.8f));
                prevWaveCost= 7000;
                break;
            case (20):
                yield return  StartCoroutine(GenerateAristocrat(1, 1f));
                yield return  StartCoroutine(GenerateCritic(1, 1f));
                yield return  StartCoroutine(GenerateMukBanger(3, 1f));
                yield return  StartCoroutine(GenerateSumo(3, 0.4f));
                yield return  StartCoroutine(GenerateJoe(20, 0.2f));
                yield return  StartCoroutine(GenerateSumo(3, 0.4f));
                yield return  StartCoroutine(GenerateMarathonRunner(10, 0.5f));
                yield return  StartCoroutine(GenerateSumo(3, 0.4f));
                yield return  StartCoroutine(GenerateAristocrat(1, 1f));
                yield return  StartCoroutine(GenerateJoe(10, 0.2f));
                yield return  StartCoroutine(GenerateCritic(1, 1f));
                yield return  StartCoroutine(GenerateSumo(3, 0.6f));
                yield return  StartCoroutine(GenerateMarathonRunner(10, 0.8f));
                prevWaveCost= 12000;
                break;
            default:
                float cost = prevWaveCost + (wave * 200);
                currentCost = cost;
                int rand;
                int rand2;
                int generateExtra=1;
                int currentUnit;
                int currentUnitCost=0;
                Debug.Log("initial: "+cost);

                while (cost > 0){
                    rand = UnityEngine.Random.Range(0, enemyList.Count);
                    rand2 = UnityEngine.Random.Range(0, enemySpacingList.Count);
                    currentUnit = enemyList[rand];
                    switch (rand)
                        {
                            case (0):
                                generateExtra = (int)Math.Ceiling((double)wave/2);
                                yield return GenerateJoe(10+generateExtra, 0.3f);
                                currentUnitCost=currentUnit*10*(generateExtra);
                                break;
                            case (1):
                                generateExtra = (int)Math.Ceiling((double)wave/3);
                                yield return GenerateMarathonRunner(7+generateExtra, enemySpacingList[rand2]);
                                currentUnitCost=currentUnit*7+currentUnit*(generateExtra);
                                break;
                            case (2):
                                generateExtra = (int)Math.Ceiling((double)wave/6);
                                yield return GenerateSumo(generateExtra, enemySpacingList[rand2]);
                                currentUnitCost=currentUnit*(generateExtra);
                                break;
                            case (3):
                                generateExtra = (int)Math.Ceiling((double)wave/8);
                                yield return GenerateMukBanger(generateExtra, enemySpacingList[rand2]);
                                currentUnitCost=currentUnit*(generateExtra);
                                break;
                            case (4):
                                generateExtra = (int)Math.Ceiling((double)wave/15);
                                yield return GenerateCritic(generateExtra, enemySpacingList[rand2]);
                                currentUnitCost=currentUnit*(generateExtra);
                                break;
                            case (5):
                                generateExtra = (int)Math.Ceiling((double)wave/20);
                                yield return GenerateAristocrat(generateExtra, enemySpacingList[rand2]);
                                currentUnitCost=currentUnit*(generateExtra);
                                break;
                        }
                    Debug.Log("bought: "+currentUnitCost);
                    cost-=currentUnitCost;
                    Debug.Log("remaining: "+cost);
                    }
                break;
        }
        prevWaveCost = currentCost;
        yield return new WaitForSeconds(3f);
    }
}
