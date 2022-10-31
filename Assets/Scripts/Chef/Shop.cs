using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour {
    BuildManager buildManager;
    private TurretBluePrint standardTurret;
    public UnityEvent OnSelectPremium;
    public UnityEvent OnSelectStandard;
    // Set instance
    void Start(){
        buildManager = BuildManager.instance;
    }
    // Purchase Standard Chest
    public void PurchaseStandardChest(){
        Debug.Log("Standard Purchase");
        OnSelectStandard.Invoke();
        buildManager.SetScroll(0);
    }

    // Purchase Standard Chest
    public void PurchasePremiumChest(){
        Debug.Log("Premium Purchase");
        OnSelectPremium.Invoke();
        buildManager.SetScroll(1);

    }

    // Reset Chest
    public void ResetOption()
    {
        buildManager.SetScroll(2);
    }
}