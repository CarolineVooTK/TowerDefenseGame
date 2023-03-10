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
    public int damage;
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
    private bool isLegend=false;

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

    // Reset values when start + store material colours
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
                i++;              
        }
        InvokeRepeating ("reduceSlowDuration",0f,1f);
        BuffEnemy();
    }

    // Reset the statistics of the enemies
    public void ResetEnemy()
    {
        startingHunger = 50;
        speed = 5;
        tokensDropped = 5;
        level = 1;
        damage = 1;
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
                damage = 2;
                break;
          case OPTIONS.sumo:
                startingHunger = 4500;
                speed *= 0.25f;
                tokensDropped = 50;
                level = 5;
                damage = 5;
                break;
            case OPTIONS.mukbanger:
                startingHunger = 10500;
                speed *= 0.6f;
                tokensDropped = 70;
                level = 3;
                damage = 7;
                break;
            case OPTIONS.foodCritic:
                startingHunger = 20000;
                speed *= 0.8f;
                tokensDropped = 100;
                level = 10;
                damage = 15;
                break;
            case OPTIONS.aristocrat:
                startingHunger = 50000;
                speed *= 0.5f;
                tokensDropped = 250;
                level = 20;
                damage = 39;
                break;
        }

        rotationSpeed = 720;
        CurrentHunger = startingHunger;
        usedSpeed = speed;
    }

    // Buff the enemy according to wave
    public void BuffEnemy()
    {
        int wave = GameManager.waveNum;
        // Switch based on the type chosen and assign its respected values
        switch (type)
        {
            case OPTIONS.averageJoe:
                if (wave-4/2.0==0){
                    CurrentHunger = startingHunger * 2f * wave;
                    }
                break;
            case OPTIONS.marathonRunner:
                if (wave-4/2.0==0){
                    CurrentHunger = startingHunger * 2f * wave;
                    }
                break;
          case OPTIONS.sumo:
                if (wave-5/2.0==0){
                    CurrentHunger = startingHunger * 3f * wave;
                    }
                break;
            case OPTIONS.mukbanger:
                if (wave-5/2.0==0){
                    CurrentHunger = startingHunger * 3f * wave;
                    }
                break;
            case OPTIONS.foodCritic:
                if (wave-15/2.0==0){
                    CurrentHunger = startingHunger * 3f * wave;
                    }
                break;
            case OPTIONS.aristocrat:
                if (wave-23/2.0==0){
                    CurrentHunger = startingHunger * 2f * wave;
                    }
                break;
        }

        CurrentHunger = startingHunger;
        lastWave = GameManager.waveNum;
    }

    // Reduce hunger when given food
    public void ApplyFood(float damage)
    {
        CurrentHunger -= damage;
    }
    // Slow enemy - pink loooove effects
    public void Slow(float pct,Vector3 _hit){
        isSlowed=true;
        currentSlowDuration=slowDuration;
        pctSlow=pct;
        hit = _hit;
        isLegend=false;
    }
    // Slow enemy - golden loooove effects
    public void Slow(float pct,Vector3 _hit,bool Legendary){
        isSlowed=true;
        currentSlowDuration=slowDuration;
        pctSlow=pct;
        hit = _hit;
        isLegend=true;
    }
    // Applies Color to enemy if slowed
    private void IsSlowed(){
        int i=0;
        if (isSlowed==false || currentSlowDuration==0){
            usedSpeed=speed;
            foreach (var render in rend)
            {
                    render.color=initial[i]-new Color(0.1f,0.1f,0.1f);  
                    i++;              
            }

        } else {
            usedSpeed = speed *(1f - pctSlow);
            foreach (var render in rend)
            {
                render.color = initial[i]+new Color(1,0,0.5f)*new Color(0.3f,0.3f,0.3f);  
                if (isLegend) render.color = initial[i]+Color.yellow-new Color(0.1f,0.1f,0.1f);  
                i++;              
            }

        }
    }
    // Reduces slow duration by seconds
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