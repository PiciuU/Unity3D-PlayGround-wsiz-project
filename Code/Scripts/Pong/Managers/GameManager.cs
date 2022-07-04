using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace Pong
{
    public class GameManager : MonoBehaviour
    {
        private AppManager _appManager;
        private GameObject _gameInterface;

        [Header("Game Properties")]
        public Status GameStatus;
        public GameModes GameMode;
        public GameDifficulties GameDifficulty;
        public SpecialModeManager _specialModeManager;

        [Header("Environment")]
        public Ball ball;
        public PlayerPaddle playerPaddle;
        public ComputerPaddle computerPaddle;

        public TextMeshProUGUI playerScoreText;
        public TextMeshProUGUI computerScoreText;

        private int playerScore;
        private int computerScore;

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

        public enum GameDifficulties
        {
            EASY,
            NORMAL,
            HARD
        }

        private readonly Dictionary<string, float> PlayerDifficultyEasy = new Dictionary<string, float>
    {
        { "speed", 10.0f },
        { "scale", 1.5f },
    };

        private readonly Dictionary<string, float> ComputerDifficultyEasy = new Dictionary<string, float>
    {
        { "speed", 6.0f },
        { "scale", 1.2f },
    };

        private readonly Dictionary<string, float> PlayerDifficultyNormal = new Dictionary<string, float>
    {
        { "speed", 10.0f },
        { "scale", 1.2f },
    };

        private readonly Dictionary<string, float> ComputerDifficultyNormal = new Dictionary<string, float>
    {
        { "speed", 10.0f },
        { "scale", 1.2f },
    };

        private readonly Dictionary<string, float> PlayerDifficultyHard = new Dictionary<string, float>
    {
        { "speed", 10.0f },
        { "scale", 1.2f },
    };

        private readonly Dictionary<string, float> ComputerDifficultyHard = new Dictionary<string, float>
    {
        { "speed", 15.0f },
        { "scale", 1.2f },
    };

        private void Awake()
        {
            GameStatus = Status.INIT;
            GameMode = GameModes.CLASSIC;
            GameDifficulty = GameDifficulties.NORMAL;
            _appManager = GameObject.FindGameObjectWithTag("AppManager").GetComponent<AppManager>();
            _gameInterface = GameObject.FindGameObjectWithTag("GameInterface");
            _gameInterface.transform.Find("StartMenu").gameObject.SetActive(true);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }

        public void SetGameMode(int gameMode)
        {
            if (gameMode == 1) GameMode = GameModes.CLASSIC;
            else GameMode = GameModes.SPECIAL;
            _gameInterface.transform.Find("StartMenu/GameModeMenu").gameObject.SetActive(false);
            _gameInterface.transform.Find("StartMenu/DifficultyModeMenu").gameObject.SetActive(true);
        }

        public void SetGameDifficulty(int gameDifficulty)
        {
            Dictionary<string, float> PlayerDifficultySettings = PlayerDifficultyHard;
            Dictionary<string, float> ComputerDifficultySettings = ComputerDifficultyHard;
            if (gameDifficulty == 1)
            {
                GameDifficulty = GameDifficulties.EASY;
                PlayerDifficultySettings = PlayerDifficultyEasy;
                ComputerDifficultySettings = ComputerDifficultyEasy;
            }
            else if (gameDifficulty == 2)
            {
                GameDifficulty = GameDifficulties.NORMAL;
                PlayerDifficultySettings = PlayerDifficultyNormal;
                ComputerDifficultySettings = ComputerDifficultyNormal;
            }
            else GameDifficulty = GameDifficulties.HARD;

            playerPaddle.SetProperties(PlayerDifficultySettings);
            computerPaddle.SetProperties(ComputerDifficultySettings);
            _gameInterface.transform.Find("StartMenu/DifficultyModeMenu").gameObject.SetActive(false);
            RunGame();
        }

        public void RunGame()
        {
            Time.timeScale = 1f;
            GameStatus = Status.RUNNING;
            _gameInterface.transform.Find("StartMenu").gameObject.SetActive(false);
            playerPaddle.SetControls(Utility.BoolToInt(_appManager.GetPreference("Gyroscope")));
            if (GameMode == GameModes.SPECIAL) _specialModeManager.Enable();
            ResetRound();
        }

        public void LoadMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Level/Scenes/Menu");
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

        public void PlayerScores()
        {
            playerScore += 1;
            this.playerScoreText.text = playerScore.ToString();
            this.ResetRound();
        }

        public void ComputerScores()
        {
            computerScore += 1;
            this.computerScoreText.text = computerScore.ToString();
            this.ResetRound();
        }

        private void ResetRound()
        {
            this.playerPaddle.ResetPosition();
            this.computerPaddle.ResetPosition();
            this.ball.ResetPosition();
            this.ball.AddStartingForce();
        }
    }
}
