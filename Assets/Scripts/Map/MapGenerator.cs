using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapTile;
    public GameObject pathTile;
    public GameObject attackTile;
    
    [SerializeField] private int mapWidth;
    [SerializeField] private int mapLength;
    private int mapHeight = 1;

    private List <GameObject> mapTiles = new List<GameObject>();
    private List <GameObject> pathTiles = new List<GameObject>();
    private List <GameObject> attackTiles = new List<GameObject>();
    private List <GameObject> pathTilesRandom = new List<GameObject>();

    private bool reachedX = false;

    private GameObject currentTile;
    private int currentIndex;
    private int nextIndex;
    private int count = 0;

    
    private void Start()
    {
        generateMap();
    }

    private void moveUp(){
        currentIndex = mapTiles.IndexOf(currentTile);

        nextIndex = currentIndex+69;
      //  currentTile = mapTiles[nextIndex];
        if (pathTiles.Contains(mapTiles[nextIndex])){

        }
        else{
            pathTilesRandom.Add(mapTiles[nextIndex]);
            count +=1;
        }

    }
    
    private void moveDown(){
       // Debug.Log("movingDown:");
        currentIndex = mapTiles.IndexOf(currentTile);
        //Debug.Log(currentIndex);
        nextIndex = currentIndex-69;
        if (pathTiles.Contains(mapTiles[nextIndex])){

        }
        //Debug.Log(nextIndex);
       // currentTile = mapTiles[nextIndex];
        else{
            pathTilesRandom.Add(mapTiles[nextIndex]);
            count +=1;
        }

    }
    //private void moveRight(){
       // currentIndex = mapTiles.IndexOf(currentTile);
      //  nextIndex = currentIndex+1;
      //  if (pathTiles.Contains(mapTiles[nextIndex])){

      //  }
        //Debug.Log(nextIndex);
       // currentTile = mapTiles[nextIndex];
      //  else{
      //      pathTilesRandom.Add(mapTiles[nextIndex]);
      //      count +=1;
      //  }

  //  }
    
    private void moveLeft(){
        currentIndex = mapTiles.IndexOf(currentTile);
        nextIndex = currentIndex-1;
        if (pathTiles.Contains(mapTiles[nextIndex])){

        }
        //Debug.Log(nextIndex);
       // currentTile = mapTiles[nextIndex];
        else{
            pathTilesRandom.Add(mapTiles[nextIndex]);
            count +=1;
        }

    }

    private void generateMap()
    {
        int z = -22;
        int y = mapHeight;
        int x = -129;
        while (z <= mapLength){
            while(x <= mapWidth){
                GameObject newTile = Instantiate(mapTile);
                mapTiles.Add(newTile);

                newTile.transform.position = new Vector3(x,y,z); 
                x +=4;

            }
            x = -129;
            z += 4;
        }
        GameObject startTile =  mapTiles[811];
        GameObject endTile = mapTiles[511];

        currentTile = startTile;

        int loopCount = 0;

        while(reachedX == false){
            if (loopCount>0 && count!= 0){
                Debug.Log(count);
                int rand = Random.Range(0,count);

                count = 0;
                currentTile = pathTilesRandom[rand];
                pathTilesRandom = new List<GameObject>();
                pathTiles.Add(currentTile);

                currentIndex = mapTiles.IndexOf(currentTile);
                attackTiles.Add(mapTiles[currentIndex+1]);
                attackTiles.Add(mapTiles[currentIndex-1]);
                attackTiles.Add(mapTiles[currentIndex+69]);
                attackTiles.Add(mapTiles[currentIndex-69]);


            }
            else if (loopCount == 0){
                pathTiles.Add(currentTile);
            }
            loopCount++;
            if(loopCount > 400){
                break;
            }
            if(currentTile.transform.position.x >-17){
                if(currentTile.transform.position.x <=7 &&currentTile.transform.position.x >=-17  ){
                    if(currentTile.transform.position.z <= 6 ){
                        //Debug.Log("moving up2");
                        moveLeft();
                    }

                }
                else if (currentTile.transform.position.x >3){
                    moveLeft();
                }

            }
          //  if(currentTile.transform.position.x < 79){
          //      Debug.Log("movingRight1");
         //       moveRight();
         //   }
            if(currentTile.transform.position.z > -10 ){
                if(currentTile.transform.position.x <=3 &&currentTile.transform.position.x >=-13  ){
                    if(currentTile.transform.position.z <= 6 ){
                        moveDown();
                    }

                }
                else if(currentTile.transform.position.x>=63 && currentTile.transform.position.x<=79){
                    if(currentTile.transform.position.z>18){
                        moveDown();
                    }

                }
                else if (currentTile.transform.position.x >3){
                    moveDown();
                }

            }
            if(currentTile.transform.position.z < 34 ){
                if(currentTile.transform.position.x <=8 &&currentTile.transform.position.x >=-17  ){
                    if(currentTile.transform.position.z <= 2 ){
                        //Debug.Log("moving up2");
                        moveUp();
                    }

                }
                else if (currentTile.transform.position.x >3){
                    moveUp();
                }
            }
            if(currentTile.transform.position.z == endTile.transform.position.z && currentTile.transform.position.x == endTile.transform.position.x){
                reachedX = true;
            }


        }

        foreach(GameObject obj in pathTiles){
            Destroy(obj);
            GameObject newPathTile = Instantiate(pathTile);

            newPathTile.transform.position = new Vector3(obj.transform.position.x,obj.transform.position.y,obj.transform.position.z); 

            
        }
        foreach(GameObject obj in attackTiles){
            if(pathTiles.Contains(obj)){

            }
            else{
                Destroy(obj);
                GameObject newAttackTile = Instantiate(attackTile);

                newAttackTile.transform.position = new Vector3(obj.transform.position.x,obj.transform.position.y,obj.transform.position.z); 

            }
        }
    }
}






    
