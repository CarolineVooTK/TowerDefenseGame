using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    void Awake (){
        instance = this;
    }
    public TurretBluePrint turretToBuild;

    public GameObject standardTurretPrefab;
    public GameObject anotherTurretPrefab;
    public string placeTag = "Placeable";
    public void BuildTurretOn (Node node){
        if (node.tag!=placeTag){
            Debug.Log("Cannot Place!");
            return;
        } 
        if (GameManager.tokenBank < turretToBuild.cost){
            Debug.Log("not enough money");
            return;
        }

        GameManager.tokenBank =- turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab,node.GetBuildPosition(),Quaternion.identity);
        node.turret = turret;
        Debug.Log("Money Left" + GameManager.tokenBank);
    }
    public bool CanBuild { get {return turretToBuild != null;}}
    public void SetTurretToBuild(TurretBluePrint turret){
        turretToBuild = turret;
    }
    public GameObject GetTurretToBuild(){
        return turretToBuild.prefab;
    }
    
}