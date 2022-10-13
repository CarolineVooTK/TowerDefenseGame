public class Chef : MonoBehaviour{
    public Transform target;
    public float range = 15f;
    public string enemyTag = "Enemy";
    void Start () {
        InvokeRepeating ("UpdateTarget",0f,0.5f);
    }

    void UpdateTarget () {
        GameObject[] enemies = GameObject.FindGameObjectWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position,enemy.transform.position);    
            if (distance < shortestDistance){
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <=range){
            target = nearestEnemy.tranform;
        }
    }

    void Update() {
        if (target == null) return;
    }

    void OnDrawGizmoSelected () {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);

    }
}