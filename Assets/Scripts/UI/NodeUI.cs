using UnityEngine;
using System.Collections;

public class NodeUI : MonoBehaviour{
    private Node target;

    public GameObject ui;
    public void Start(){
        ui.SetActive(false);
    }
    public void SetTarget(Node _target){
        target= _target;
        // transform.position = target.GetBuildPosition()+new Vector3 (0,6,0);
        ui.SetActive(true);
    }
    public void Hide(){
        ui.SetActive(false);
    }
    public void Sell(){
        Debug.Log("SELL");
        target.Sell();
        BuildManager.instance.DeselectNode();
    }
}