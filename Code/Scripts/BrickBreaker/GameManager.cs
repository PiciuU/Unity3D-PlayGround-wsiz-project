using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace BrickBreaker
{
    public class GameManager : MonoBehaviour
    {
        private AppManager _appManager;
        private GameObject _gameInterface;

        [Header("Environment")]
        public Paddle paddle;
        public Ball ball;
        public Brick[] bricks;

        [Header("User Interface")]
        public TextMeshProUGUI scoreText;
        public TextMeshProUGUI livesText;

        [Header("Game Properties")]
        public Status GameStatus;
        public GameModes GameMode;

        public int score = 0;
        public int lives = 3;

        public enum Status
        {
            INIT,
            RUNNING,
            PAUSED
        }

        public enum GameModes
        {
            CLASSIC,
            SPECIAL
        }

        private void Awake()
        {
            GameStatus = Status.INIT;
            paddle = FindObjectOfType<Paddle>();
            ball = FindObjectOfType<Ball>();
            _appManager = FindObjectOfType<AppManager>();
            _gameInterface = GameObject.FindGameObjectWithTag("GameInterface");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }

        // UI
        public void SetGameMode(int gameMode)
        {
            if (gameMode == 1)
            {
                GameMode = GameModes.CLASSIC;
                GameObject.Find("Layout").transform.Find("Classic").gameObject.SetActive(true);
            }
            else
            {
                GenerateLayout();
                GameMode = GameModes.SPECIAL;
            }
            _gameInterface.transform.Find("StartMenu").gameObject.SetActive(false);
            RunGame();
        }

        private void UpdateScoreUI()
        {
            scoreText.text = $"Score: {score}";
        }

        private void UpdateLivesUI()
        {
            livesText.text = $"Lives: {lives}";
        }

        public void PauseGame()
        {
            if (GameStatus == Status.RUNNING)
            {
                _gameInterface.transform.Find("PauseMenu").gameObject.SetActive(true);
                GameStatus = Status.PAUSED;
                Time.timeScale = 0f;
            }
            else if (GameStatus == Status.PAUSED)
            {
                _gameInterface.transform.Find("PauseMenu").gameObject.SetActive(false);
                GameStatus = Status.RUNNING;
                Time.timeScale = 1f;
            }
        }

        public void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level/Scenes/Menu");
        }


        // Gameplay

        public void RunGame()
        {
            if (Camera.main.aspect < 0.6)
            {
                float newScale = Mathf.Clamp(Camera.main.aspect * 2 - (Camera.main.aspect / 3), 0.5f, 1f);
                GameObject.Find("Layout").transform.localScale = new Vector3(newScale, newScale);
            }

            GameStatus = Status.RUNNING;
            Time.timeScale = 1f;
            score = 0;
            lives = 3;
            bricks = FindObjectsOfType<Brick>();
            UpdateScoreUI();
            UpdateLivesUI();
            ResetRound();
            paddle.SetControls(Utility.BoolToInt(_appManager.GetPreference("Gyroscope")));
        }

        public void OnBrickHit(Brick brick)
        {
            score += brick.points;
            UpdateScoreUI();

            if (IsBoardCleared())
            {
                GameOver(true);
            }
        }

        private bool IsBoardCleared()
        {
            foreach(Brick brick in bricks)
            {
                if (brick.gameObject.activeInHierarchy && !brick.unbreakable) return false;
            }

            return true;
        }

        public void DropHealth()
        {
            lives -= 1;
            UpdateLivesUI();

            if (lives > 0) ResetRound();
            else GameOver();
        }

        private void GameOver(bool playerHasWon = false)
        {
            ball.ResetPosition();
            paddle.DisableControls();
            GameStatus = Status.PAUSED;
            _gameInterface.transform.Find("GameOverMenu").gameObject.SetActive(true);
            _gameInterface.transform.Find("GameOverMenu").GetChild(2).GetComponent<TextMeshProUGUI>().text = $"Your Score: {score}";
            if (playerHasWon)
            {
                _gameInterface.transform.Find("GameOverMenu").GetChild(1).GetComponent<TextMeshProUGUI>().text = "Win";
                _gameInterface.transform.Find("GameOverMenu").GetChild(1).GetComponent<TextMeshProUGUI>().color = new Color32(0, 140, 27, 255);
            }
        }

        private void ResetRound()
        {
            paddle.ResetPosition();
            ball.ResetPosition();
            ball.Invoke(nameof(ball.AddStartingForce), 0.5f);
        }

        private void GenerateLayout()
        {
            GameObject.Find("Layout").transform.Find("Special").gameObject.SetActive(true);

            foreach(Transform row in GameObject.Find("Special/Bricks").transform)
            {
                int currentBricksInRow = 0;
                int maxBricksInRow = Random.Range(3, 6);

                do
                {
                    int randomIndex = Random.Range(0, 5);
                    Transform spot = row.GetChild(randomIndex);
                    if (spot.name == "Spot" && spot.gameObject.activeInHierarchy)
                    {
                        int brickType = Random.Range(0, 6);
                        GameObject brick = (GameObject)Instantiate(Resources.Load($"Prefab/Brick_{brickType}"), spot.transform.position, spot.transform.rotation);
                        brick.transform.parent = row.transform;
                        brick.name = "Brick";
                        spot.gameObject.SetActive(false);
                        currentBricksInRow++;
                    }
                } while (currentBricksInRow < maxBricksInRow);
            }
        }
    }
}
