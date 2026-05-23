using UnityEngine;

public class SquidMorph : Enemy
{
    [SerializeField] private Sprite[] sprites;
    private float moveSpeed;
    private float timer;
    private float timeInterval;
    [SerializeField] private ObjectPooler squid;

    public override void Start()
    {
        base.Start();
        squid = GameObject.Find("SquidCritterPool").GetComponent<ObjectPooler>();
        timeInterval = 2f;
        hitSound = AudioManager.Instance.hitArmor;
        destroySound = AudioManager.Instance.boom2;
        destroyEffectPool = GameObject.Find("SquidCritterPopPool").GetComponent<ObjectPooler>();

        hitSound = AudioManager.Instance.beetleHit;
        destroySound = AudioManager.Instance.beetleDestroy;

        sR.sprite = sprites[Random.Range(0, sprites.Length)];
        moveSpeed = Random.Range(0.5f, 0.9f);
    }
    public override void Update()
    {
        base.Update();
        timer += Time.deltaTime;
        if(timer >= timeInterval) ShotCritter();
        //follow player
        //transform.position = Vector3.MoveTowards(transform.position, PlayerController.Instance.transform.position, moveSpeed * Time.deltaTime);
        //facing player
        Vector3 direction = PlayerController.Instance.transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void ShotCritter()
    {
        AudioManager.Instance.PlayModifiedSound(AudioManager.Instance.popSpawn);
        timer -= timeInterval;

        GameObject spawnedSquid = squid.GetPooledObject();
        spawnedSquid.transform.position = transform.position;
        spawnedSquid.SetActive(true);

        // Hướng "lên trên" của SquidMorph (đang xoay về phía player)
        Vector2 ejectDir = transform.up;

        SquidCritter critter = spawnedSquid.GetComponent<SquidCritter>();
        if (critter != null)
            critter.Launch(ejectDir, speed: 4f, duration: 0.5f);
    }
}
