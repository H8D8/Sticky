using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float customGravity;

    private Rigidbody thisRigidBody;

    private void Awake()
    {
        thisRigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        thisRigidBody.linearVelocity = new Vector3(0, customGravity, 0);
    }

    private void Update()
    {
        PlayerInput();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Side"))
        {
            ChangeGravity();
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(collision.gameObject);
            GameManager.ChangeHealth(-1);
        }
    }

    private void ChangeGravity()
    {
        thisRigidBody.linearVelocity = Vector3.zero;
        if (GameManager.gameRunning)
        {
            customGravity = -customGravity;
            thisRigidBody.AddForce(0, customGravity, 0, ForceMode.VelocityChange);
        }
    }

    private void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && GameManager.gameRunning)
        {
            ChangeGravity();
        }
    }
}
