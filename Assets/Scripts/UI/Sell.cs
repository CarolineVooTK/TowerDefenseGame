using UnityEngine;
using System.Collections;

public class Sell : MonoBehaviour{
    public NodeUI nodeUI;
    public void OnPointerDown(){
        nodeUI.Sell();
    }
    public void OnMouseEnter (){
        Debug.Log("Selllll");
        nodeUI.Sell();
    }
}