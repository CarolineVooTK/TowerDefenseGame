using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map3Generator : MonoBehaviour
{
    public GameObject mapTile;
    public GameObject pathTile;
    public GameObject sandTile;
    public GameObject attackTile;
    public GameObject point;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapLength;
    private int mapHeight = 15;

    private List <GameObject> mapTiles = new List<GameObject>();
    private List <GameObject> pathTiles = new List<GameObject>(); 
    private List <GameObject> sandTiles = new List<GameObject>(); 
    //private List <GameObject> attackTiles = new List<GameObject>();
    private List <GameObject> pathTilesRandom = new List<GameObject>();

    private bool reachedX = false;

    private GameObject currentTile;
    private int currentIndex;
    private int nextIndex;
    private int count = 0;
    private int tilesNum = 0;

    public Transform ParentPath;
    public Transform ParentAttack;
    public Transform ParentNodes;
    public Transform ParentPoint;

    public UnityEvent OnDoneMap;

    private void Start()
    {
        generateMap();
    }


    private void moveUp(){
        currentIndex = mapTiles.IndexOf(currentTile);

        nextIndex = currentIndex+74;

        if (pathTiles.Contains(mapTiles[nextIndex])){

        }
        else{
            pathTilesRandom.Add(mapTiles[nextIndex]);
            count +=1;
        }

    }
    
    private void moveDown(){
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex-74;
        if (pathTiles.Contains(mapTiles[nextIndex])){

        }
        else{
            pathTilesRandom.Add(mapTiles[nextIndex]);
            count +=1;
        }

    }
    
    private void moveLeft(){
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex-1;
        if (pathTiles.Contains(mapTiles[nextIndex])){

        }
        else{
            pathTilesRandom.Add(mapTiles[nextIndex]);
            count +=1;
        }

    }
    private void moveRight(){
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex+1;
        if (pathTiles.Contains(mapTiles[nextIndex])){

        }
        else{
            pathTilesRandom.Add(mapTiles[nextIndex]);
            count +=1;
        }

    }

   private void generateMap()
   {
        int z = -76;
        int y = mapHeight;
        int x = -95;
        while (z <= mapLength){
            while(x <= mapWidth){
                GameObject newTile = Instantiate(mapTile, ParentNodes);
                mapTiles.Add(newTile);

                newTile.transform.position = new Vector3(x,y,z); 
                tilesNum++;
                x +=4;

            }
            x = -95;
            z += 4;
        }
        int randLength = Random.Range(40,80);
        int countSand = 0;
        int randNum = Random.Range(1,50);
        while (countSand < randLength){
            int randNum2 = Random.Range(1,40);
            for(int l = 0+randNum;l<randNum+randNum2;l++){

                GameObject newSandTile = Instantiate(sandTile);
                sandTiles.Add(newSandTile);
                float q = mapTiles[tilesNum-l].transform.position.x;
                float w = mapTiles[tilesNum-1].transform.position.y;
                float e = mapTiles[tilesNum-l].transform.position.z;
                if (q >= 5 && q<=105 && e <= 8){
                }
                else{
                    newSandTile.transform.position = new Vector3(q,w,e);
                    Destroy(mapTiles[tilesNum-l]);
                }


            }
            randNum += randNum2;
            countSand++;
        }
        GameObject startTile = mapTiles[617];
        GameObject endTile;

        endTile = mapTiles[1579];
        

        currentTile = startTile;

        int loopCount = 0;

        while(reachedX == false){
            if (loopCount>0 && count!= 0){
                int rand = Random.Range(0,count);

                count = 0;
                currentTile = pathTilesRandom[rand];
                pathTilesRandom = new List<GameObject>();
                pathTiles.Add(currentTile);

                /*currentIndex = mapTiles.IndexOf(currentTile);
                attackTiles.Add(mapTiles[currentIndex+1]);
                attackTiles.Add(mapTiles[currentIndex-1]);
                attackTiles.Add(mapTiles[currentIndex+74]);
                attackTiles.Add(mapTiles[currentIndex-74]);*/


            }
            else if (loopCount == 0){
                pathTiles.Add(currentTile);
            }
            loopCount++;
            if(loopCount > 400){
                break;
            }


            if(currentTile.transform.position.x <=105 && currentTile.transform.position.x > 5){
                moveLeft();
            }
            if(currentTile.transform.position.x >= 5 &&  currentTile.transform.position.x <=105 ) {
                if(currentTile.transform.position.x <= 105 && currentTile.transform.position.z >= 4){
                    moveLeft();
                }
                else{
                    moveRight();
                }
            }
            if(currentTile.transform.position.z <= 4 && currentTile.transform.position.z >-56){
                if(currentTile.transform.position.x >= 60 && currentTile.transform.position.z <= -20){
                    moveUp();
                }
                else{
                    moveDown();
                }
            }
            if(currentTile.transform.position.z >=-56 && currentTile.transform.position.z < 8){
                    moveUp();
            }
            
            if(currentTile.transform.position.z == endTile.transform.position.z && currentTile.transform.position.x == endTile.transform.position.x){
                reachedX = true;
            }


        }

        foreach(GameObject obj in pathTiles){
            Destroy(obj);
            GameObject newPathTile = Instantiate(pathTile, ParentPath);

            newPathTile.transform.position = new Vector3(obj.transform.position.x,obj.transform.position.y,obj.transform.position.z);

            GameObject newPoint = Instantiate(point, ParentPoint);

            newPoint.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

        }
      /* foreach(GameObject obj in attackTiles){
            if(pathTiles.Contains(obj)){

            }
            else{
                Destroy(obj);
                GameObject newAttackTile = Instantiate(attackTile, ParentAttack);

                newAttackTile.transform.position = new Vector3(obj.transform.position.x,obj.transform.position.y,obj.transform.position.z); 

            }
        }*/

        OnDoneMap.Invoke();
    }
    
}
