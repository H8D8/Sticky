using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private void Update()
    {
        MoveThisToLeft();
        DestroyIfFar();
    }

    private void DestroyIfFar()
    {
        if (transform.position.x < GameManager.farObjectX)
        {
            DestroyThis();
        }
    }

    private void DestroyThis()
    {
        Destroy(gameObject);
    }

    private void MoveThisToLeft()
    {
        if (GameManager.gameRunning)
        {
            transform.Translate(Vector3.left * Time.deltaTime * GameManager.speed);
        }
    }
}
