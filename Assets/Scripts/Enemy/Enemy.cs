using UnityEngine;

// Class for enemy object
public class Enemy : MonoBehaviour
{
    // Types of enemies
    public enum OPTIONS
    {
        averageJoe, marathonRunner, mukbanger, foodCritic, sumo, aristocrat
    }

    public OPTIONS type;
    [HideInInspector]
    public float usedSpeed;
    public float startingHunger;
    public float speed;
    public int tokensDropped;
    public float rotationSpeed;
    [SerializeField] private ParticleSystem collisionParticles;
    public GameObject explosive;

    private int wavePointIndex = 0;
    private int lastWave=0;
    public Transform target;
    private float _currentHunger;
    private int level;
    private float pctSlow;
    private bool isSlowed = false;
    private float slowDuration = 3f;
    private float currentSlowDuration=0;
    private Material[] rend;
    private Vector3 hit;
    private Color[] initial;
    public Transform partToRotate;

    // Check current hunger
    private float CurrentHunger
    {
        get => _currentHunger;
        set
        {
            _currentHunger = value;

            // Destroy object when full
            if (CurrentHunger <= 0)
            { 
                // Seperate sound effect 
                var soundEffect = Instantiate(this.explosive);
                soundEffect.transform.position = transform.position + new Vector3(0f, 1.6f, 0f);

                // Create collision particles in opposite direction to movement
                for (int i=0; i<level; i++)
                {
                    var particles = Instantiate(this.collisionParticles);
                    particles.transform.position = transform.position + new Vector3(0f, 2f, 0f);
                }
               
                // Add token from death
                GameManager.AddToken(tokensDropped);
                Destroy(gameObject);
            }
        }
    }

    // Reset values when start
    void Start()
    {
        ResetEnemy();
        target = WayPoints.points[0];
        rend = GetComponent<Renderer>().materials;
        int i=0;
        initial = new Color[rend.Length];
        foreach (var render in rend)
        {
                initial[i] = render.color;  
                Debug.Log(render.color);
                i++;              
        }
        InvokeRepeating ("reduceSlowDuration",0f,1f);
    }

    // Reset the statistics of the enemies
    public void ResetEnemy()
    {
        startingHunger = 50;
        speed = 5;
        tokensDropped = 5;
        level = 1;

        // Switch based on the type chosen and assign its respected values
        switch (type)
        {
            case OPTIONS.averageJoe:
                break;
            case OPTIONS.marathonRunner:
                startingHunger = 70;
                speed *= 1.4f;
                tokensDropped = 7;
                level = 2;
                break;
          case OPTIONS.sumo:
                startingHunger = 4500;
                speed *= 0.25f;
                tokensDropped = 50;
                level = 5;
                break;
            case OPTIONS.mukbanger:
                startingHunger = 6500;
                speed *= 0.6f;
                tokensDropped = 40;
                level = 3;
                break;
            case OPTIONS.foodCritic:
                startingHunger = 10000;
                speed *= 0.8f;
                tokensDropped = 100;
                level = 15;
                break;
            case OPTIONS.aristocrat:
                startingHunger = 25000;
                speed *= 0.5f;
                tokensDropped = 150;
                level = 20;
                break;
        }

        rotationSpeed = 720;
        CurrentHunger = startingHunger;
        usedSpeed = speed;
    }

    // Reset the statistics of the enemies
    public void BuffEnemy()
    {
        int wave = GameManager.waveNum;
        if (wave==lastWave) return;
        // Switch based on the type chosen and assign its respected values
        switch (type)
        {
            case OPTIONS.averageJoe:
                if (wave/5.0==0){
                    startingHunger *= 1.5f;
                    }
                break;
            case OPTIONS.marathonRunner:
                if (wave/5.0==0){
                    startingHunger *= 1.5f;
                    }
                break;
          case OPTIONS.sumo:
                if (wave/10.0==0){
                    startingHunger *= 1.6f;
                    }
                break;
            case OPTIONS.mukbanger:
                if (wave-5/10.0==0){
                    startingHunger *= 1.5f;
                    }
                break;
            case OPTIONS.foodCritic:
                if (wave-15/10.0==0){
                    startingHunger *= 1.5f;
                    }
                break;
            case OPTIONS.aristocrat:
                if (wave-27/10.0==0){
                    startingHunger *= 1.5f;
                    }
                break;
        }

        CurrentHunger = startingHunger;
        lastWave = GameManager.waveNum;
    }

    // Reduce hunger when given food
    public void ApplyFood(int damage)
    {
        CurrentHunger -= damage;
    }
    public void Slow(float pct,Vector3 _hit){
        isSlowed=true;
        currentSlowDuration=slowDuration;
        pctSlow=pct;
        hit = _hit;
    }
    private void IsSlowed(){
        int i=0;
        if (isSlowed==false || currentSlowDuration==0){
            usedSpeed=speed;
            foreach (var render in rend)
            {
                    render.color=initial[i];  
                    i++;              
            }

        } else {
            usedSpeed = speed *(1f - pctSlow);
            foreach (var render in rend)
            {
                render.color = initial[i]+new Color(1,0,0.5f)*new Color(0.3f,0.3f,0.3f);  
                i++;              
            }

        }
    }
    private Color ColorAvg(Color a, Color b){
        return new Color(a.r+(int)((b.r-a.r)/2),a.g+(int)((b.g-a.g)/2),a.b+(int)((b.b-a.b/2)));
    }
    private void reduceSlowDuration(){
        if (isSlowed==true){
            currentSlowDuration-=1;
        }
        if (currentSlowDuration<0){
            isSlowed=false;
        }
    }
    // Move enemy to another waypoint
    private void Update()
    {
        if (target==null){
            target = WayPoints.points[wavePointIndex];
        }
        IsSlowed();

           // Buff
        BuffEnemy();
        Vector3 dir = target.position - transform.position;
        transform.Translate(usedSpeed * Time.deltaTime * dir.normalized, Space.World);

        // Rotate object to a direction
        if (dir != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(dir);
            transform.rotation = rotation;
            if (isSlowed){
                Quaternion lookRotation = Quaternion.LookRotation(hit - transform.position);
                Vector3 rotation3 = lookRotation.eulerAngles;
                partToRotate.rotation = Quaternion.Euler(0f,rotation3.y,0f); 
                Debug.Log("rotate");
            }
        }

        // If distance is close as 0.2, assume it already reach the waypoint and update
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWayPoint();
        }
    }

    // Increase wavepoint
    private void GetNextWayPoint()
    {
        // When reach max of wavepoint, destroy item
        if (wavePointIndex >= WayPoints.points.Length - 1)
        {
            Destroy(gameObject);
        }
        else
        {
            wavePointIndex++;
            target = WayPoints.points[wavePointIndex];
        }
    }
}