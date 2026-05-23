using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected SpriteRenderer sR;
    private FlashWhite flash;
    protected ObjectPooler destroyEffectPool;

    [SerializeField] protected int lives;
    [SerializeField] protected int maxLives;
    [SerializeField] protected int damage;
    [SerializeField] protected int experienceToGive;

    protected AudioSource hitSound;
    protected AudioSource destroySound;

    protected float speedX = 0;
    protected float speedY = 0;

    public virtual void Awake()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    public virtual void OnEnable()
    {
        lives = maxLives;
    }
    public virtual void Start() //có thể truy cập từ bất cứ đâu và có thể bị ghi đè bởi các lớp con
    {
        flash = GetComponent<FlashWhite>();
    }

    public virtual void Update() // run first
    {
        transform.position += new Vector3(speedX * Time.deltaTime, speedY * Time.deltaTime);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player) player.TakeDamage(damage);
        }
    }  

    public virtual void TakeDamage(int damage)
    {
        lives -= damage;
        AudioManager.Instance.PlayModifiedSound(hitSound);

        if (lives > 0)
        {
            flash.Flash();
        }
        else
        {
            AudioManager.Instance.PlayModifiedSound(destroySound);
            GameObject destroyEffect = destroyEffectPool.GetPooledObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.transform.localScale = transform.localScale;
            destroyEffect.SetActive(true);

            PlayerController.Instance.getExperience(experienceToGive);

            flash.Reset();
            gameObject.SetActive(false);
            
        }
    }
}
