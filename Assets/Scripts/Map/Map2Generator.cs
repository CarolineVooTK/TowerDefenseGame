using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map2Generator : MonoBehaviour
{
    public GameObject mapTile;
    public GameObject pathTile;
    public GameObject attackTile;
    public GameObject point;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapLength;
    private int mapHeight = 3;

    private List <GameObject> mapTiles = new List<GameObject>();
    private List <GameObject> pathTiles = new List<GameObject>();
    private List <GameObject> attackTiles = new List<GameObject>();
    private List <GameObject> pathTilesRandom = new List<GameObject>();

    private bool reachedX = false;

    private GameObject currentTile;
    private int currentIndex;
    private int nextIndex;
    private int count = 0;

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

        nextIndex = currentIndex+48;
        if (pathTiles.Contains(mapTiles[nextIndex])){

        }
        else{
            pathTilesRandom.Add(mapTiles[nextIndex]);
            count +=1;
        }

    }
    
    private void moveDown(){
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex-48;
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

   private void generateMap()
   {
        int z = -117;
        int y = mapHeight;
        int x = 11;
        while (z <= mapLength){
            while(x <= mapWidth){
                GameObject newTile = Instantiate(mapTile, ParentNodes);
                mapTiles.Add(newTile);

                newTile.transform.position = new Vector3(x,y,z); 
                x +=4;

            }
            x = 11;
            z += 4;
        }
        GameObject startTile =  mapTiles[609];
        GameObject endTile = mapTiles[585];

        currentTile = startTile;

        int loopCount = 0;

        while(reachedX == false){
            if (loopCount>0 && count!= 0){
                int rand = Random.Range(0,count);

                count = 0;
                currentTile = pathTilesRandom[rand];
                pathTilesRandom = new List<GameObject>();
                pathTiles.Add(currentTile);

                currentIndex = mapTiles.IndexOf(currentTile);
                attackTiles.Add(mapTiles[currentIndex+1]);
                attackTiles.Add(mapTiles[currentIndex-1]);
                attackTiles.Add(mapTiles[currentIndex+48]);
                attackTiles.Add(mapTiles[currentIndex-48]);


            }
            else if (loopCount == 0){
                pathTiles.Add(currentTile);
            }
            loopCount++;
            if(loopCount > 400){
                break;
            }
            if(currentTile.transform.position.x >47 ){
               if(currentTile.transform.position.x >= 47&&currentTile.transform.position.x <=63  ){
                    if(currentTile.transform.position.z >= -86 ){
                        moveLeft();
                    }

                }
                else{
                    moveLeft();
                }

            }
            if(currentTile.transform.position.z > -91){
                if(currentTile.transform.position.x >=52 &&currentTile.transform.position.x <=79  ){
                    if(currentTile.transform.position.z >= -82){
                        moveDown();
                    }

                }
                else if(currentTile.transform.position.x>=47 && currentTile.transform.position.x <=91
                        || currentTile.transform.position.x>=135 && currentTile.transform.position.x <=143){

                }
                else if (currentTile.transform.position.x >82){
                    moveDown();
                }

            }
            if(currentTile.transform.position.z < -69 ){
                if(currentTile.transform.position.x >=47 &&currentTile.transform.position.x <=65 ){
                    if(currentTile.transform.position.z >=-95){
                        moveUp();
                    }

                }
                else if (currentTile.transform.position.x >64){
                    moveUp();
                }
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
        foreach(GameObject obj in attackTiles){
            if(pathTiles.Contains(obj)){

            }
            else{
                Destroy(obj);
                GameObject newAttackTile = Instantiate(attackTile, ParentAttack);

                newAttackTile.transform.position = new Vector3(obj.transform.position.x,obj.transform.position.y,obj.transform.position.z); 

            }
        }

        OnDoneMap.Invoke();
    }
    
}
