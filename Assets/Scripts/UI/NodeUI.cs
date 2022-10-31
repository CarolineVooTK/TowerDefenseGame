using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NodeUI : MonoBehaviour{
    private Node target;
    public Text sellAmountTxt;
    public Text upgradeCostTxt;
    public Text upgradeLvlTxt;

    public GameObject ui;
    public void Start(){
        ui.SetActive(false);
    }
    public void SetTarget(Node _target){
        target= _target;
        Chef chef = _target.turret.gameObject.GetComponent<Chef>();
        // transform.position = target.GetBuildPosition()+new Vector3 (0,6,0);
        sellAmountTxt.text = "$" + chef.GetSellAmount();
        if (chef.fullyUpgraded){
            upgradeCostTxt.text = "MAXED";
            upgradeLvlTxt.text = "LEVEL";
        } else {
            upgradeCostTxt.text = "$" + chef.GetUpgradeAmount();
            upgradeLvlTxt.text = "UPGRADE (to lvl"+(chef.level+1)+")";

        }
        ui.SetActive(true);

    }
    public void Hide(){
        // Debug.Log("Hidden");

        ui.SetActive(false);
    }
    public void Sell(){
        target.Sell();
        BuildManager.instance.DeselectNode();
    }
    public void Upgrade(){
        target.UpgradeChef();
        BuildManager.instance.DeselectNode();
    }
}