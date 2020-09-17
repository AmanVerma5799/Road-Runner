using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;

    public float speed;
    public float distanceFactor = 1f;

    private float distanceMove;

    private bool gameJustStarted;

    public GameObject obstacleHolder;
    public GameObject[] obstacleList;

    [HideInInspector]
    public bool isObstacleActive;

    private Text scoreText;
    private Text starCountText;

    private int starCount, score;

    public GameObject pausePanel;
    public Animator pauseAnimator;
    public GameObject gameOverPanel;
    public Animator gameOverAnimator;

    public Text finalScoreText, finalStarScoreText, bestScoreText;

    void Awake()
    {
        MakeInstance();

        scoreText = GameObject.Find("Score").GetComponent<Text>();
        starCountText = GameObject.Find("Star Count").GetComponent<Text>();
    }

    void Start()
    {
        gameJustStarted = true;

        GetObstacles();
        StartCoroutine("SpawnObstacles");
    }

    void Update()
    {
        MoveCamera();
    }

    void MakeInstance()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(gameObject);
        }
    }

    void MoveCamera()
    {
        if(gameJustStarted)
        {
            if(!PlayerController.instance.isDead)
            {
                if (speed < 12f)
                {
                    speed += Time.deltaTime * 5f;
                }
                else
                {
                    speed = 12f;
                    gameJustStarted = false;
                }
            }
        }

        if(!PlayerController.instance.isDead)
        {
            Camera.main.transform.position += new Vector3(speed * Time.deltaTime, 0f, 0f);
            UpdateDistance();
        }
    }

    void UpdateDistance()
    {
        distanceMove += Time.deltaTime * distanceFactor;
        float round = Mathf.Round(distanceMove);

        score = (int)round;
        scoreText.text = score.ToString();

        if (round >= 30f && round < 60f)
        {
            speed = 14f;
        }
        else if(round >= 60f)
        {
            speed = 16f;
        }
    }

    void GetObstacles()
    {
        obstacleList = new GameObject[obstacleHolder.transform.childCount];
        for (int i = 0; i < obstacleList.Length; i++)
        {
            obstacleList[i] = obstacleHolder.GetComponentsInChildren<ObstacleHolder>(true)[i].gameObject;
        }
    }

    IEnumerator SpawnObstacles()
    {
        while(true)
        {
            if(!PlayerController.instance.isDead)
            {
                if(!isObstacleActive)
                {
                    if(Random.value <= 0.85f)
                    {
                        int randomIndex = 0;
                        do
                        {
                            randomIndex = Random.Range(0, obstacleList.Length);
                        }
                        while (obstacleList[randomIndex].activeInHierarchy);

                        obstacleList[randomIndex].SetActive(true);
                        isObstacleActive = true;
                    }
                }
            }
            yield return new WaitForSeconds(0.6f);
        }
    }

    public void UpdateStarCount()
    {
        starCount++;
        starCountText.text = starCount.ToString();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        pausePanel.SetActive(true);
        pauseAnimator.Play("SlideIn");
    }

    public void ResumeGame()
    {
        pauseAnimator.Play("SlideOut");
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Game");
    }

    public void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        gameOverAnimator.Play("SlideIn");

        finalScoreText.text = score.ToString();
        finalStarScoreText.text = starCount.ToString();

        if(GameManager.instance.scoreCount < score)
        {
            GameManager.instance.scoreCount = score;
        }

        bestScoreText.text = GameManager.instance.scoreCount.ToString();

        GameManager.instance.starCount += starCount;
        GameManager.instance.SaveGameData();
    }
}