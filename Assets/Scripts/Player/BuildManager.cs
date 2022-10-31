using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {

    public static BuildManager instance;

    void Awake (){
        instance = this;
    }
    public TurretBluePrint turretToBuild;
    public Node selectedNode;
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
        if (scrollInt == 1)
        {
            PurchasePremiumChest();
        }
        if (scrollInt == 0)
        {
            PurchaseStandardChest();
        }
    }
    public string placeTag = "Placeable";
    public UnityEvent OnNoMoney;
    public UnityEvent OnPurchase;
    public UnityEvent OnNoPlace;
    public NodeUI nodeUI;
    [SerializeField] private Text textDisplay;
    [SerializeField] private string prefix;
    [SerializeField] private string sufix;

    // attempt building chef
    public void BuildTurretOn (Node node){
        if (scrollInt == 2) return;
         if (scrollInt == 1)
        {
            PurchasePremiumChest();
        }
        if (scrollInt == 0)
        {
            PurchaseStandardChest();
        }
        if (node.tag!=placeTag){
            scrollInt = 2;
            Debug.Log("Cannot Place!");
            OnNoPlace.Invoke();
            return;
        }

        if (GameManager.tokenBank < turretToBuild.cost)
        {
            scrollInt = 2;
            Debug.Log("not enough money");
            OnNoMoney.Invoke();
            return;
        }

        GameManager.ReduceToken(turretToBuild.cost);
        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab,node.GetBuildPosition(),Quaternion.identity);
        OnPurchase.Invoke();
        node.turret = turret;
        Debug.Log("Money Left " + GameManager.tokenBank);
    }
    // If can build chef
    public bool CanBuild { get {return turretToBuild != null;}}
    
    // Select the node
    public void SelectNode(Node node){
        if (selectedNode == node){
            DeselectNode();
            return;
        }
        
        selectedNode = node;
        turretToBuild = null;

        nodeUI.SetTarget(node);
    }
    
    // unselects node
    public void DeselectNode(){
        selectedNode = null;
        nodeUI.Hide();
    }

    // purchases premium chest
    public void PurchasePremiumChest(){
        int roll = Random.Range(0,100);
        switch(roll){
            case (<10):
                turretToBuild=farmer;
                UpdateText("Common");
                break;
            case (<40):
                turretToBuild=ChooseRareTurret();
                UpdateText("Rare");
                break;
            case (<75):
                turretToBuild=ChooseUltraRareTurret();
                UpdateText("Ultra Rare");
                break;
            default:
                turretToBuild=ChooseLegendaryTurret();
                UpdateText("Legendary");
                break;
        }
        Debug.Log("Premium Purchased");
        turretToBuild.SetCost(premiumCost);
    }
    // purchases standard chest
    public void PurchaseStandardChest(){
        int roll = Random.Range(0,100);
        switch(roll){
            case (<60):
                turretToBuild=farmer;
                UpdateText("Common");
                break;
            case (<85):
                turretToBuild=ChooseRareTurret();
                UpdateText("Rare");
                break;
            case (<95):
                turretToBuild=ChooseUltraRareTurret();
                UpdateText("Ultra Rare");
                break;
            default:
                turretToBuild=ChooseLegendaryTurret();
                UpdateText("Legendary");
                break;
        }
        Debug.Log("Standard Purchased");
        turretToBuild.SetCost(standardCost);
    }
    // returns random raare chef
    public TurretBluePrint ChooseRareTurret(){
        int roll = Random.Range(0,3);
        if (roll==0) return coffee;
        if (roll==1) return doughnuts;
        return sandwich;
    }
    // returns random ultraraare chef
    public TurretBluePrint ChooseUltraRareTurret(){
        int roll = Random.Range(0,3);
        if (roll==0) return boba;
        if (roll==1) return korean;
        return pizza;
    }
    // returns random legendary chef
    public TurretBluePrint ChooseLegendaryTurret(){
        int roll = Random.Range(0,3);
        if (roll==0) return laksa;
        if (roll==1) return sushi;
        return indomie;
    }
    // updates cheff
    private void UpdateText(string displayText)
    {
        textDisplay.text = this.prefix + displayText + this.sufix;
    }

}