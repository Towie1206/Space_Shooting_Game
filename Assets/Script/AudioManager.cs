using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource ice;
    public AudioSource fire;
    public AudioSource hit;
    public AudioSource pause;
    public AudioSource unpause;
    public AudioSource boom2;
    public AudioSource hitRock;
    public AudioSource shoot;
    public AudioSource squished;
    public AudioSource burn;
    public AudioSource hitArmor;
    public AudioSource bossCharge;
    public AudioSource BossSpawn;
    public AudioSource beetleDestroy;
    public AudioSource beetleHit;
    public AudioSource locustDestroy;
    public AudioSource locustHit;
    public AudioSource locustCharge;
    public AudioSource popSpawn;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
           Instance = this;
        }
    }

    public void PlaySound(AudioSource Sound)
    {
        Sound.Stop();
        Sound.Play();
    }

    public void PlayModifiedSound(AudioSource Sound)
    {
        Sound.pitch = Random.Range(0.7f, 1.3f);
        Sound.Stop();
        Sound.Play();
    }

}
