using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NodeUI : MonoBehaviour{
    private Node target;
    public Text sellAmountTxt;

    public GameObject ui;
    public void Start(){
        ui.SetActive(false);
    }
    public void SetTarget(Node _target){
        target= _target;
        // transform.position = target.GetBuildPosition()+new Vector3 (0,6,0);
        sellAmountTxt.text = "$" + _target.turret.gameObject.GetComponent<Chef>().sellAmount;
        ui.SetActive(true);

    }
    public void Hide(){
        // Debug.Log("Hidden");

        ui.SetActive(false);
    }
    public void Sell(){
        Debug.Log("SELL");
        target.Sell();
        BuildManager.instance.DeselectNode();
    }
}