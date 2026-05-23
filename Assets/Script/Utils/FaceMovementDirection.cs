using UnityEngine;
using UnityEngine.UIElements;

public class FaceMovementDirection : MonoBehaviour
{
    private Vector3 previousPosition;
    private Vector3 moveDirection;
    private Quaternion targeRotation; // quaternion có 4 giá trị x,y,z,w (w là lượng quay )
    private float rotationSpeed = 200;

    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        previousPosition -= new Vector3(GameManager.Instance.adjustedWorldSpeed,0);
        moveDirection = transform.position - previousPosition;

        targeRotation = Quaternion.LookRotation(Vector3.forward, moveDirection);
        transform.rotation = Quaternion.RotateTowards(transform.rotation,targeRotation,rotationSpeed * Time.deltaTime);

        previousPosition = transform.position;
    }
}
