using UnityEngine;

public class FloatInSpace : MonoBehaviour
{
    void Update()
    {
        float moveX = GameManager.Instance.adjustedWorldSpeed;
        transform.position -= new Vector3(moveX, 0f, 0f);
        if (transform.position.x < -11)
        {
            gameObject.SetActive(false);
        }
    }
}
