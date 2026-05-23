using UnityEngine;

public class Whale : MonoBehaviour
{

    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position -= new Vector3(moveX, 0f, 0f);

        if (transform.position.x < -11f)
        {
            Destroy(gameObject);
        }
    }
}
