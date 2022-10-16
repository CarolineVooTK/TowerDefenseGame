using UnityEngine;

public class Shop : MonoBehaviour {
    BuildManager buildManager;
    public TurretBluePrint standardTurret;
    public TurretBluePrint anotherTurret;
    void Start(){
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardChest(){
        Debug.Log("Standard Purchase");
        buildManager.SetTurretToBuild(standardTurret);
    }

    public void PurchasePremiumChest(){
        Debug.Log("Premium Purchase");
        buildManager.SetTurretToBuild(anotherTurret);

    }
}