using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool gameRunning;
    public static int health;
    public static int score;
    public static int speed;
    public static float farObjectX;

    [Header("Obstacles")]
    public Vector3 groundSpawnPosition;
    public GameObject[] obstacles;
    public float[] spawnDelays;

    [Header("UI Elements")]
    public TMP_Text scoreText;
    public TMP_Text healthText;
    public GameObject menuObject;
    public TMP_Text finalScoreText;

    public static void ChangeHealth(int value)
    {
        health += value;
        if (health <= 0)
        {
            gameRunning = false;
        }
    }

    public static void ChangeScore(int value)
    {
        score += value;
    }

    private void Awake()
    {
        AssignStaticVar();
    }

    private void Start()
    {
        StartCoroutine(IncScore1PerSec());
        StartCoroutine(SpawnObstacles());
    }

    private void Update()
    {
        UpdateUI();
    }

    private IEnumerator IncScore1PerSec()
    {
        while (gameRunning)
        {
            yield return new WaitForSeconds(1);
            ChangeScore(1);
        }
    }

    private IEnumerator SpawnObstacles()
    {
        while (gameRunning)
        {
            GameObject randomObstacle = obstacles[Random.Range(0, obstacles.Length)];
            int obstaclesIndex = System.Array.IndexOf(obstacles, randomObstacle);
            bool obstacleOnGround = Random.Range(0, 2) == 0 ? true : false;

            if (obstacleOnGround)
            {
                Instantiate(randomObstacle, groundSpawnPosition, Quaternion.identity);

            }
            else
            {
                Vector3 roofPos = new Vector3(groundSpawnPosition.x, groundSpawnPosition.y
                - (2 * groundSpawnPosition.y), groundSpawnPosition.z);

                Instantiate(randomObstacle, roofPos, Quaternion.Euler(180f, 0f, 0f));
            }

            yield return new WaitForSeconds(spawnDelays[obstaclesIndex]);
        }
    }

    void AssignStaticVar()
    {
        gameRunning = true;
        health = 5;
        score = 0;
        speed = 10;
        farObjectX = -groundSpawnPosition.x;
    }

    private void PlayerLost()
    {
        scoreText.gameObject.SetActive(false);
        healthText.gameObject.SetActive(false);
        menuObject.SetActive(true);
        finalScoreText.text = $"Final Score: {score}";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void UpdateUI()
    {
        if (gameRunning)
        {
            scoreText.text = $"Score: {score}";
            healthText.text = $"Health: {health}";
        }
        else
        {
            PlayerLost();
        }
    }
}
