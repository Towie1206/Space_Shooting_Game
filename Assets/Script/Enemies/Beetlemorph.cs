using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Beetlemorph : Enemy
{
    [SerializeField] private Sprite[] sprites;
    private float timer;
    private float frequency; // có thể gọi là chu kì 
    private float amplitude; // biên độ
    private float centerY;

    public override void OnEnable()
    {
        base.OnEnable();
        timer = 0;
        frequency = Random.Range(0.3f,1f);   // có thể gọi là chu kì 
        amplitude = Random.Range(0.8f,1.5f); // biên độ 
        centerY = transform.position.y;
    }

    public override void Start()
    {
        base.Start(); // Gọi phương thức Start() của lớp cha Enemy để đảm bảo rằng sR được khởi tạo đúng cách
        sR.sprite = sprites[Random.Range(0,sprites.Length)];
        destroyEffectPool = GameObject.Find("BeetlePopPool").GetComponent<ObjectPooler>();
        hitSound = AudioManager.Instance.beetleHit;
        destroySound = AudioManager.Instance.beetleDestroy;
        speedX = Random.Range(-0.8f , -1.5f);
    }
    public override void Update() // run later
    {
        base.Update();

        timer += Time.deltaTime; 
        float sine = Mathf.Sin(timer * frequency) * amplitude; //sine wave motion : là 1 dạng sóng hình học dao động lên xuống và trái phải 
        transform.position = new Vector3(transform.position.x,centerY + sine);
    }
}
