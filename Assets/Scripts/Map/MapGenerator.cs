using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject mapTile;

    [SerializeField] private int mapWidth;
    [SerializeField] private int mapLength;

    private List <GameObject> mapTiles;
    private List <GameObject> pathTiles;

    private void Start()
    {
        generateMap();
    }

    private void generateMap()
    {
        int z = 0;
        int y = 4;
        int x = 0;
        while (z <= mapLength){
            while(x <= mapWidth){
                GameObject newTile = Instantiate(mapTile);

                newTile.transform.position = new Vector3(x,y,z); 
                x +=4;

            }
            x = 0;
            z += 4;
        }
    }
}
