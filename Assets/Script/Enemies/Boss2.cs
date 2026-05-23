using UnityEngine;

public class Boss2 : Enemy
{
    private bool charing = true; //đang trong trạng thái lao nhanh

    private Animator anim;

    public override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        EnterIdleState();
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
        if (transform.position.y > 4 || transform.position.y < -4) speedY *= -1;
        if (transform.position.x > 7.5f)
        {
            EnterIdleState();
        }
        else if(transform.position.x < -9f)
        {
            EnterChargeState();
        }
    }

    private void EnterIdleState() 
    {
        if(charing) 
        {
            speedX = -2f;
            speedY = Random.Range(-2.2f, 2.2f);
            charing = false;
            anim.SetBool("charging", false);
        }
    }
    private void EnterChargeState() 
    {
        if (!charing)
        {
            speedX = Random.Range(7.5f,9);
            speedY = 0;
            charing = true;
            anim.SetBool("charging", true);
        }
    }
}
