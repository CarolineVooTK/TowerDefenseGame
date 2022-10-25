using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour {
    BuildManager buildManager;
    private TurretBluePrint standardTurret;
    public UnityEvent OnSelectPremium;
    public UnityEvent OnSelectStandard;
    void Start(){
        buildManager = BuildManager.instance;
    }
    public void PurchaseStandardChest(){
        Debug.Log("Standard Purchase");
        // standardTurret.SetCost(10);
        OnSelectStandard.Invoke();
        buildManager.SetScroll(0);
        // buildManager.SetTurretToBuild(standardTurret);
    }

    public void PurchasePremiumChest(){
        Debug.Log("Premium Purchase");
        // standardTurret.SetCost(40);
        OnSelectPremium.Invoke();
        buildManager.SetScroll(1);
        // buildManager.SetTurretToBuild(standardTurret);

    }

    public void ResetOption()
    {
        buildManager.SetScroll(2);
    }
}