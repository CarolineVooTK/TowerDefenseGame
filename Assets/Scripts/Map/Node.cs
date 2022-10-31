using UnityEngine;

public class Node : MonoBehaviour {

    public Color hoverColor;
    private Renderer rend;
    [Header("Optional")]
    public GameObject turret;
    public Vector3 positionOffset;
    private Color startColour;
    BuildManager buildManager;

    void Start (){
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        buildManager = BuildManager.instance;
    }
    public Vector3 GetBuildPosition(){
        return transform.position +positionOffset;
    }
    public void OnMouseDown(){

        if (turret!=null){
            buildManager.SelectNode(this);
            return;
        }
        buildManager.DeselectNode();
        if (!buildManager.CanBuild) return;
        // build a turret
        buildManager.BuildTurretOn(this);
        // GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        // turret = (GameObject) Instantiate(turretToBuild,transform.position+positionOffset,transform.rotation);
    }
    public void OnMouseEnter (){
        if (!buildManager.CanBuild) return;
        rend.material.color = hoverColor;
    }

    public void OnMouseExit (){
        rend.material.color = startColour;
    }
    public void Sell(){
        GameManager.AddToken(50);
        Destroy(turret);
        turret= null;
    }
}