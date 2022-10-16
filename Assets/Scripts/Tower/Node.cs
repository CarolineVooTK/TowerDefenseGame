using UnityEngine;

public class Node : MonoBehaviour {

    public Color hoverColor;
    private Renderer rend;
    [Header("Optional")]
    private GameObject turret;
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
    void OnMouseDown(){
        if (!buildManager.CanBuild) return;
        if (turret!=null){
            Debug.Log("Can't Build There!");
            return;
        }
        // build a turret
        buildManager.BuildTurretOn(this);
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject) Instantiate(turretToBuild,transform.position+positionOffset,transform.rotation);
    }
    void OnMouseEnter (){
        if (!buildManager.CanBuild) return;
        rend.material.color = hoverColor;
    }

    void OnMouseExit (){
        rend.material.color = startColour;
    }
}