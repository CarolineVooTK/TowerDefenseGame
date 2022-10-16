using UnityEngine;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    void Awake (){
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject anotherTurretPrefab;
    void Start () {
        turretToBuild = standardTurretPrefab;
    }

    private TurretBluePrint turretToBuild;
    public void BuildTurretOn (Node node){
        // if (GameManager.tokenbank < turretToBuild.cost){
            //Debug.Log("not enough money")
            //return;
        //}

        //GameManager.tokenbank =- turretToBuild.cost;
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab,node.GetBuildPosition(),Quaternion.identity);
        node.turret = turret;
        //Debug.Log("Money Left" + GameManager.tokenBank)
    }
    public bool CanBuild { get {return turretToBuild != null;}}
    public void SetTurretToBuild(TurretBluePrint turret){
        turretToBuild = turret;
    }
}