using UnityEngine;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    private Rigidbody2D rb;
    private Animator anim;

    private FlashWhite flashWhite;

    private Vector2 movement;
    [SerializeField] private float speed;
    public bool isBoosting = false;


    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float EnergyRegenRate;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [SerializeField] private ObjectPooler destroyEffectPool;

    [SerializeField] private ParticleSystem boostEffect;

    [SerializeField] private int experience;
    [SerializeField] private int currentLevel;
    [SerializeField] private int maxLevel;
    [SerializeField] private List<int> playerLevel; // kiểu như 1 list và trong list đấy nhập số exp người chơi cần để lên cấp 


    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        flashWhite = GetComponent<FlashWhite>();

        destroyEffectPool = GameObject.Find("Boom1Pool").GetComponent<ObjectPooler>();

        for(int i = playerLevel.Count; i < maxLevel; i ++)
        {
            playerLevel.Add(Mathf.CeilToInt(playerLevel[playerLevel.Count - 1 ] *1.1f + 15));
        }    


        energy = maxEnergy;
        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
        health = maxHealth;
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
        experience = 0;
        UIController.Instance.UpdateExperienceSlider(experience, playerLevel[currentLevel]);
    }

    void Update()
    {
        
        if (Time.timeScale > 0)
        { 
            float InputX = Input.GetAxisRaw("Horizontal");
            float InputY = Input.GetAxisRaw("Vertical");

            anim.SetFloat("moveX", InputX);
            anim.SetFloat("moveY", InputY);

            movement = new Vector2(InputX, InputY).normalized;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.LeftAlt))
            {
                EnterBoost();
            }
            else if(Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.LeftAlt))
            {
                ExitBoost();
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                PhaserWeapon.Instance.Shoot();
            }
        }

    }

    private void FixedUpdate()
    {
         rb.linearVelocity = new Vector2(movement.x * speed, movement.y * speed);

        if (isBoosting)
        {
            if (energy >= 0.5f)
            {
                energy -= 0.5f;
            }
            else
            {
                ExitBoost();
            }
        }
        else
        { 
            if(energy < maxEnergy)
            {
                energy += EnergyRegenRate;
            }
        }
        UIController.Instance.UpdateEnergySlider(energy, maxEnergy);
    }

    private void EnterBoost() 
    {
        if (energy > 10)
        {
            boostEffect.Play();
            AudioManager.Instance.PlaySound(AudioManager.Instance.fire);
            anim.SetBool("boosting", true);
            GameManager.Instance.SetWorldSpeed(7f);
            isBoosting = true;
        }
    }
    public void ExitBoost() 
    {
        anim.SetBool("boosting", false);
        GameManager.Instance.SetWorldSpeed(1f);
        isBoosting = false;
    }

    private void OnCollisionEnter2D(Collision2D collision) // gây sát thương
    {
        if(collision.gameObject.CompareTag("Obstacle"))
        {
            Asteroid asteroid = collision.gameObject.GetComponent<Asteroid>();
            if (asteroid) asteroid.TakeDamage(1, true);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy) enemy.TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        flashWhite.Flash();
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
        AudioManager.Instance.PlaySound(AudioManager.Instance.hit);
        if (health <= 0)
        {
            GameManager.Instance.SetWorldSpeed(0f);
            gameObject.SetActive(false);

            GameObject destroyEffect = destroyEffectPool.GetPooledObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.SetActive(true);

            GameManager.Instance.GameOver();
            AudioManager.Instance.PlaySound(AudioManager.Instance.ice);
        }
    }

    public void getExperience(int exp)
    {
        experience += exp;
        UIController.Instance.UpdateExperienceSlider(experience, playerLevel[currentLevel]);
        if(experience > playerLevel[currentLevel])
            LevelUp();
    }
    public void LevelUp()
    {
        experience -= playerLevel[currentLevel];
        if (currentLevel < maxLevel - 1 )
        {
            currentLevel++;
        }
        UIController.Instance.UpdateExperienceSlider(experience, playerLevel[currentLevel]);
        PhaserWeapon.Instance.LevelUp();
        maxHealth++;
        health = maxHealth;
        UIController.Instance.UpdateHealthSlider(health, maxHealth);
        //maxEnergy++;
    }
}
