using UnityEngine;
using UnityEngine.Events;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    void Awake (){
        instance = this;
    }
    public TurretBluePrint turretToBuild;
    [Header ("COST")]
    public int premiumCost=400;
    public int standardCost=200;
    [Header("TURRETS")]
    public TurretBluePrint farmer;
    public TurretBluePrint indomie;
    public TurretBluePrint laksa;
    public TurretBluePrint sushi;
    public TurretBluePrint coffee;
    public TurretBluePrint doughnuts;
    public TurretBluePrint sandwich;
    public TurretBluePrint boba;
    public TurretBluePrint korean;
    public TurretBluePrint pizza;
    public enum scrollType{
        COMMON,PREMIUM
    }
    public int scrollInt;
    public void SetScroll(int _set){
        scrollInt=_set;
    }
    public string placeTag = "Placeable";
    public UnityEvent OnNoMoney;
    public void BuildTurretOn (Node node){
        if (scrollInt==1) PurchasePremiumChest();
        if (scrollInt==0) PurchaseStandardChest();       
        if (node.tag!=placeTag){
            Debug.Log("Cannot Place!");
            return;
        } 
        if (GameManager.tokenBank < turretToBuild.cost){
            Debug.Log("not enough money");
            OnNoMoney.Invoke();
            return;
        }


        GameManager.ReduceToken(turretToBuild.cost);
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab,node.GetBuildPosition(),Quaternion.identity);
        node.turret = turret;
        Debug.Log("Money Left " + GameManager.tokenBank);
    }
    public bool CanBuild { get {return turretToBuild != null;}}
    public void SetTurretToBuild(TurretBluePrint turret){
        turretToBuild = turret;
    }
    public GameObject GetTurretToBuild(){
        return turretToBuild.prefab;
    }

    public void PurchasePremiumChest(){
        int roll = Random.Range(0,100);
        switch(roll){
            case (<10):
                 turretToBuild=farmer;
                break;
            case (<40):
                 turretToBuild=ChooseRareTurret();
                break;
            case (<75):
                 turretToBuild=ChooseUltraRareTurret();
                break;
            default:
                 turretToBuild=ChooseLegendaryTurret();
                break;
        }
        Debug.Log("Premium Purchased");
        turretToBuild.SetCost(premiumCost);
        // SetScroll(1);
    }
    public void PurchaseStandardChest(){
        int roll = Random.Range(0,100);
        switch(roll){
            case (<60):
                turretToBuild=farmer;
                break;
            case (<85):
                turretToBuild=ChooseRareTurret();
                break;
            case (<95):
                turretToBuild=ChooseUltraRareTurret();
                break;
            default:
                turretToBuild=ChooseLegendaryTurret();
                break;
        }
        Debug.Log("Standard Purchased");
        turretToBuild.SetCost(standardCost);
        // SetScroll(0);
    }
    public TurretBluePrint ChooseRareTurret(){
        int roll = Random.Range(0,3);
        if (roll==0) return coffee;
        if (roll==1) return doughnuts;
        return sandwich;
    }
    public TurretBluePrint ChooseUltraRareTurret(){
        int roll = Random.Range(0,3);
        if (roll==0) return boba;
        if (roll==1) return korean;
        return pizza;
    }
    public TurretBluePrint ChooseLegendaryTurret(){
        int roll = Random.Range(0,3);
        if (roll==0) return laksa;
        if (roll==1) return sushi;
        return indomie;
    }
    
}