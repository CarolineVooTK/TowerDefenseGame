using UnityEngine;

public class Shop : MonoBehaviour {
    BuildManager buildManager;
    private TurretBluePrint standardTurret;
    void Start(){
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardChest(){
        Debug.Log("Standard Purchase");
        // standardTurret.SetCost(10);
        buildManager.SetScroll(0);
        // buildManager.SetTurretToBuild(standardTurret);
    }

    public void PurchasePremiumChest(){
        Debug.Log("Premium Purchase");
        // standardTurret.SetCost(40);
        buildManager.SetScroll(1);
        // buildManager.SetTurretToBuild(standardTurret);

    }
}