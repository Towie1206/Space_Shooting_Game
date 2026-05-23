using UnityEngine;

public class SquidCritter : Enemy
{
    [SerializeField] private Sprite[] sprites;
    private float moveSpeed;

    private Vector2 launchVelocity;
    private float launchTimer;

    public override void Start()
    {
        base.Start();
        hitSound = AudioManager.Instance.beetleHit;
        destroySound = AudioManager.Instance.beetleDestroy;
        destroyEffectPool = GameObject.Find("SquidCritterPopPool").GetComponent<ObjectPooler>();

        sR.sprite = sprites[Random.Range(0, sprites.Length)];
        moveSpeed = Random.Range(0.5f, 0.9f);
    }

    // Gọi từ SquidMorph sau khi spawn
    public void Launch(Vector2 direction, float speed, float duration)
    {
        launchVelocity = direction.normalized * speed;
        launchTimer = duration;
    }

    public override void Update()
    {
        base.Update();

        if (launchTimer > 0f)
        {
            // Đang bị đẩy ra xa — chưa đuổi player
            transform.position += new Vector3(launchVelocity.x, launchVelocity.y) * Time.deltaTime;
            launchTimer -= Time.deltaTime;
            return; // bỏ qua follow/rotate bên dưới
        }

        // Follow player bình thường
        transform.position = Vector3.MoveTowards(
            transform.position,
            PlayerController.Instance.transform.position,
            moveSpeed * Time.deltaTime
        );

        // Facing player
        Vector3 direction3D = PlayerController.Instance.transform.position - transform.position;
        float angle = Mathf.Atan2(direction3D.y, direction3D.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}