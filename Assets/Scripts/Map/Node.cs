using UnityEngine;

public class Node : MonoBehaviour {

    public Color hoverColor;
    private Renderer rend;
    [Header("Optional")]
    public GameObject turret;
    public Vector3 positionOffset;
    [SerializeField] private ParticleSystem collisionParticles;
    public GameObject explosive;

    private Color startColour;
    BuildManager buildManager;

    // Set basis
    void Start (){
        rend = GetComponent<Renderer>();
        startColour = rend.material.color;
        buildManager = BuildManager.instance;
    }

    // Get offset position to place chef ontop of the node
    public Vector3 GetBuildPosition(){
        return transform.position +positionOffset;
    }
    
    // Apply buy/sell or building of chef
    public void OnMouseDown(){

        if (turret!=null){
            buildManager.SelectNode(this);
            return;
        }
        buildManager.DeselectNode();
        if (!buildManager.CanBuild) return;
        
        // build a turret
        buildManager.BuildTurretOn(this);
    }

    // Let the user know can place chef on this node
    public void OnMouseEnter (){
        if (!buildManager.CanBuild) return;
        rend.material.color = hoverColor;
    }

    public void OnMouseExit (){
        rend.material.color = startColour;
    }

    // Sell the chef on the node
    public void Sell(){
        GameManager.AddToken(turret.gameObject.GetComponent<Chef>().GetSellAmount());

        // Seperate sound effect 
        var soundEffect = Instantiate(this.explosive);
        soundEffect.transform.position = transform.position + new Vector3(0f, 1.6f, 0f);

        var particles = Instantiate(this.collisionParticles);
        particles.transform.position = transform.position + new Vector3(0f, 2f, 0f);
        Destroy(turret);
        turret= null;
    }
    // Upgrade the chef on the node
    public void UpgradeChef(){
        Chef chef = turret.gameObject.GetComponent<Chef>();
        if (chef.fullyUpgraded) return;
        if (GameManager.tokenBank<chef.GetUpgradeAmount()) return;
        GameManager.ReduceToken(chef.GetUpgradeAmount());
        chef.Upgrade();
    }
}