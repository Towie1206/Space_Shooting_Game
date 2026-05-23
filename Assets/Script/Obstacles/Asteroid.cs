using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private FlashWhite flashWhite;

    [SerializeField] private ObjectPooler destroyEffectPool;

    private int lives;
    private int maxLives = 5;
    private int damage = 1;
    private int experienceToGive = 1 ;

    [SerializeField] private Sprite[] asteroidSprites;

    float pushX;
    float pushY;

    void OnEnable()
    {
        lives = maxLives;
        transform.rotation = Quaternion.identity;
        pushX = Random.Range(-1f, 0f);
        pushY = Random.Range(-1f, 1f);
        if(rb) rb.linearVelocity = new Vector2(pushX, pushY);
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        flashWhite = GetComponent<FlashWhite>();
        destroyEffectPool = GameObject.Find("Boom2Pool").GetComponent<ObjectPooler>();

        pushX = Random.Range(-1f, 0f);
        pushY = Random.Range(-1f, 1f);
        if (rb) rb.linearVelocity = new Vector2(pushX, pushY);

        spriteRenderer.sprite = asteroidSprites[Random.Range(0, asteroidSprites.Length)];

        float randomScale = Random.Range(0.5f, 1.5f);
        transform.localScale = new Vector3(randomScale, randomScale, 1f);

        lives = maxLives;
    }

    

    private void OnCollisionEnter2D(Collision2D collision) // gây sát thương
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController) playerController.TakeDamage(damage);

        }
    }

    public void TakeDamage(int damage, bool giveExperience)
    {
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.hitRock);
        lives -= damage;
        if (lives > 0)
        { 
            flashWhite.Flash();
        }
        else if (lives <= 0)
        {
            GameObject destroyEffect = destroyEffectPool.GetPooledObject();
            destroyEffect.transform.position = transform.position;
            destroyEffect.transform.rotation = transform.rotation;
            destroyEffect.transform.localScale = transform.localScale;
            destroyEffect.SetActive(true);

            AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.boom2);
            flashWhite.Reset();
            gameObject.SetActive(false);
            if(giveExperience)PlayerController.Instance.getExperience(experienceToGive);
        }
    }

}
