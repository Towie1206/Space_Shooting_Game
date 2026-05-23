using UnityEngine;
using UnityEngine.UIElements;

public class Boss1 : Enemy
{
    private Animator anim;
    private bool Charging;

    private float switchTimer;
    private float switchInterval;


    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    public override void OnEnable()
    {   
        base.OnEnable();
        EnterChargeState();
        AudioManager.Instance.PlaySound(AudioManager.Instance.BossSpawn);
    }
    public override void Start()
    {
        base.Start();
        hitSound = AudioManager.Instance.hitArmor;
        destroySound = AudioManager.Instance.boom2;
        destroyEffectPool = GameObject.Find("Boom3Pool").GetComponent<ObjectPooler>();
    }

    public override void Update()
    {
        base.Update();
        float playerPosition = PlayerController.Instance.transform.position.x;
        if (switchTimer > 0)
        {
            switchTimer -= Time.deltaTime;
        }
        else
        { 
            if(Charging && transform.position.x > playerPosition)
            {
                EnterPatrolState();
            }
            else
            {
                EnterChargeState();
            }
        }

        if (transform.position.y > 3 || transform.position.y < -3)
        {
            speedY *= -1;
        }
        else if (transform.position.x < playerPosition)
        {
            EnterChargeState();
        }

        bool boost = PlayerController.Instance.isBoosting;
        float moveX;
        if(boost && !Charging)
        {
            moveX = GameManager.Instance.worldSpeed * Time.deltaTime * -0.5f;
        }
        else
        {
            moveX = speedX *Time.deltaTime;
        }

        float moveY = speedY * Time.deltaTime;
        transform.position += new Vector3(moveX, moveY);

        if (transform.position.x < -11f)
        {
            gameObject.SetActive(false);
        }
    }

    void EnterPatrolState()
    {
        speedX = 0;
        speedY = Random.Range(-1f, 1f);
        switchInterval = Random.Range(5f,10f);
        switchTimer = switchInterval;
        Charging = false;
        anim.SetBool("Charging", false);
    }

    void EnterChargeState()
    {
        if(!Charging)AudioManager.Instance.PlaySound(AudioManager.Instance.bossCharge);
        speedX = -5f;
        speedY = 0;
        switchInterval = Random.Range(0.6f, 1.3f);
        switchTimer = switchInterval;
        Charging = true;
        anim.SetBool("Charging", true);
    }
    public override void OnCollisionEnter2D(Collision2D collision) //gây sát thương
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid) asteroid.TakeDamage(damage, false);
        }
    }
    
}
