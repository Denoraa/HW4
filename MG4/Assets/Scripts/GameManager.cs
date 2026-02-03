using UnityEngine;
using UnityEngine.SceneManagement;
using static GameManager;

public class GameManager : MonoSingleton<GameManager>
{

    private int _Score;
    public HighScoreData _ScoreData;

    public delegate void GameOver();
    public event GameOver gameOverNotify;

    public delegate void GameRestart();
    public event GameRestart onGameRestart;

    public GameState _gameState = GameState.None;



    public enum GameState
    {


        None,
        GameON,
        GameOff

    }

    private void Start()
    {
        PillarsManager.Instance.OnScoreingNotify += Scoreing;
        PlayerController.Instance.gameOver += GameEnd;
    }

    private void Update()
    {



        WhenGameOver();


    }


    private void OnDisable()
    {
        PillarsManager.Instance.OnScoreingNotify -= Scoreing;
        PlayerController.Instance.gameOver -= GameEnd;
    }
    private void GameStart()
    {


        _gameState = GameState.GameON;


    }
    private void GameEnd()
    {

        _gameState = GameState.GameOff;
        UpdateHighScore();
        gameOverNotify?.Invoke();
        Time.timeScale = 0;

    }
    private void WhenGameOver()
    {

        if (Input.anyKeyDown&&_gameState==GameState.GameOff)
        {
            Time.timeScale = 1;
            _gameState=GameState.None;
            onGameRestart?.Invoke();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

    }

    private void Scoreing()
    {

        _Score++;

    }
    private void UpdateHighScore()
    {

        if (_Score > _ScoreData.HighScore)
        {

            _ScoreData.HighScore = _Score;

        }
    }

    public GameState CurrentGameState
    {

        get
        {

            return _gameState;

        }
      
    }
    public int Score
    {

        get
        {

            return _Score;

        }

    }


}
