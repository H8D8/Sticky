using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float customGravity;

    bool playerOnRoof;
    Rigidbody thisRigidBody;

    void Start()
    {
        thisRigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerInput();
    }

    void FixedUpdate()
    {
        thisRigidBody.AddForce(0, customGravity, 0, ForceMode.VelocityChange);
    }

    void PlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            customGravity = -customGravity;
            playerOnRoof = !playerOnRoof;
        }
    }
}
