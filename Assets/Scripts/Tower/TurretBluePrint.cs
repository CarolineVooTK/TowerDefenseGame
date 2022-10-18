using UnityEngine;
using System.Collections;

[System.Serializable]
public class TurretBluePrint {
    public GameObject prefab;
    public int cost;
    public void SetCost(int _cost){
        cost=_cost;
    }
}