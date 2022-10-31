using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Map2Generator : MonoBehaviour
{
    //the tiles for the map
    public GameObject mapTile;
    public GameObject pathTile;
    public GameObject attackTile;
    public GameObject point;

    //the objects that are instantiated
    public GameObject aloe;
    public GameObject stone;
    public GameObject cactus;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapLength;
    private int mapHeight = 3;

    private List <GameObject> mapTiles = new List<GameObject>();
    private List <GameObject> pathTiles = new List<GameObject>();
    private List <GameObject> attackTiles = new List<GameObject>();
    private List <GameObject> pathTilesRandom = new List<GameObject>();
    private List<int> itemTiles = new List<int>();
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
    
    //functions for moving the direction of the tile 
    private void moveUp(){
        currentIndex = mapTiles.IndexOf(currentTile);

        nextIndex = currentIndex+48;
        //if the tile is on the path, do not move that direction
        if (pathTiles.Contains(mapTiles[nextIndex])){}
        else{
            pathTilesRandom.Add(mapTiles[nextIndex]);
            count +=1;
        }
    }
    
    private void moveDown(){
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex-48;
        if (pathTiles.Contains(mapTiles[nextIndex])){}
        else{
            pathTilesRandom.Add(mapTiles[nextIndex]);
            count +=1;
        }
    }
    
    private void moveLeft(){
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex-1;
        if (pathTiles.Contains(mapTiles[nextIndex])){}
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
            //generate ground tiles for the map 
            while(x <= mapWidth){
                GameObject newTile = Instantiate(mapTile, ParentNodes);
                mapTiles.Add(newTile);

                newTile.transform.position = new Vector3(x,y,z); 
                tilesNum++;
                x +=4;
            }
            x = 11;
            z += 4;
        }
        //starting and end tile of the enemies
        GameObject startTile =  mapTiles[609];
        GameObject endTile = mapTiles[585];

        currentTile = startTile;

        int loopCount = 0;
        //generate path and attack tiles until it reaches the enemies ending point
        while(reachedX == false){
            if (loopCount>0 && count!= 0){
                int rand = Random.Range(0,count);

                count = 0;
                //choose a random tile from the list of movable direction and set as current tile
                currentTile = pathTilesRandom[rand];
                pathTilesRandom = new List<GameObject>();
                pathTiles.Add(currentTile);

                currentIndex = mapTiles.IndexOf(currentTile);
                //random number used to choose from a set of configurations
                int randAtt = Random.Range(1,10);
                //different amount of attack tiles
                if (randAtt == 1){
                    attackTiles.Add(mapTiles[currentIndex+1]);
                }
                if (randAtt == 2){
                    attackTiles.Add(mapTiles[currentIndex-1]);
                }
                if (randAtt == 3){
                    attackTiles.Add(mapTiles[currentIndex+48]);
                }
                if (randAtt == 4){
                    attackTiles.Add(mapTiles[currentIndex-48]);
                }
                if (randAtt == 5){
                    attackTiles.Add(mapTiles[currentIndex+1]);
                    attackTiles.Add(mapTiles[currentIndex-1]);
                }
                if (randAtt == 6){
                    attackTiles.Add(mapTiles[currentIndex+48]);
                    attackTiles.Add(mapTiles[currentIndex-48]);
                }
                if (randAtt == 7){
                    attackTiles.Add(mapTiles[currentIndex+1]);
                    attackTiles.Add(mapTiles[currentIndex-48]);
                }
                if (randAtt == 8){
                    attackTiles.Add(mapTiles[currentIndex+48]);
                    attackTiles.Add(mapTiles[currentIndex-1]);
                }
                if (randAtt == 9){
                    attackTiles.Add(mapTiles[currentIndex+48]);
                    attackTiles.Add(mapTiles[currentIndex-1]);
                    attackTiles.Add(mapTiles[currentIndex+1]);
                    attackTiles.Add(mapTiles[currentIndex-48]);
                }

            }
            //if starting point just add into into path
            else if (loopCount == 0){
                pathTiles.Add(currentTile);
            }
            loopCount++;
            if(loopCount > 400){
                break;
            }
            //check if tile is in preset boundary of path before moving
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
            //check if reached ending point
            if(currentTile.transform.position.z == endTile.transform.position.z && currentTile.transform.position.x == endTile.transform.position.x){
                reachedX = true;
            }


        }
        //destroy ground tile and generate path tiles
        foreach(GameObject obj in pathTiles){
            Destroy(obj);
            GameObject newPathTile = Instantiate(pathTile, ParentPath);

            newPathTile.transform.position = new Vector3(obj.transform.position.x,obj.transform.position.y,obj.transform.position.z);

            GameObject newPoint = Instantiate(point, ParentPoint);

            newPoint.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, obj.transform.position.z);

        }
        //destroy ground tile and generate attack tiles
        foreach(GameObject obj in attackTiles){
            //if attack tile is already a path tile do not generate
            if(pathTiles.Contains(obj)){}
            else{
                Destroy(obj);
                GameObject newAttackTile = Instantiate(attackTile, ParentAttack);

                newAttackTile.transform.position = new Vector3(obj.transform.position.x,obj.transform.position.y,obj.transform.position.z); 

            }
        }
        //functions to generate random objects on map
        int itemCount = 0;
        //generating different objects using different ranges
        while (itemCount < 40){
            //generate random tile number to place object
            int randTile = Random.Range(0,tilesNum);
            //if tile contains item already skip
            if(itemTiles.Contains(randTile)){}
            //if tile is a path or attack tile skip
            else if((pathTiles.Contains(mapTiles[randTile])) || (attackTiles.Contains(mapTiles[randTile]))){}
            //check if tile is in preset boundary
            else{
                if ((((mapTiles[randTile]).transform.position.x) >47) &&((mapTiles[randTile]).transform.position.x<139)){
                    GameObject newItem= Instantiate(cactus);

                    newItem.transform.position = new Vector3((mapTiles[randTile]).transform.position.x,((mapTiles[randTile]).transform.position.y),(mapTiles[randTile]).transform.position.z); 
                    itemTiles.Add(randTile);
                }
            }
            itemCount++;
        }
        while (itemCount < 70){
            int randTile = Random.Range(50,tilesNum);
            if(itemTiles.Contains(randTile)){}
            else if((pathTiles.Contains(mapTiles[randTile])) || (attackTiles.Contains(mapTiles[randTile]))){}
            else{
                if ((((mapTiles[randTile]).transform.position.x) >47) &&((mapTiles[randTile]).transform.position.x<139)){
                    GameObject newItem= Instantiate(stone);

                    newItem.transform.position = new Vector3((mapTiles[randTile]).transform.position.x,((mapTiles[randTile]).transform.position.y),(mapTiles[randTile]).transform.position.z); 
                    itemTiles.Add(randTile);
                }
            }
            itemCount++;
        }

        while (itemCount < 120){
            int randTile = Random.Range(70,tilesNum);
            if(itemTiles.Contains(randTile)){}
            else if((pathTiles.Contains(mapTiles[randTile])) || (attackTiles.Contains(mapTiles[randTile]))){}
            else{
                if ((((mapTiles[randTile]).transform.position.x) >47) &&((mapTiles[randTile]).transform.position.x<139)){
                    GameObject newItem= Instantiate(aloe);

                    newItem.transform.position = new Vector3((mapTiles[randTile]).transform.position.x,((mapTiles[randTile]).transform.position.y),(mapTiles[randTile]).transform.position.z); 
                    itemTiles.Add(randTile);
                }
            }
            itemCount++;
        }


        OnDoneMap.Invoke();
    }
    
}
