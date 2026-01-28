using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{

    //Imports


    //Usual Variables
    private int _Score;
    //public HighScoreData _ScoreData;

    //Unique Variables
    public delegate void GameOver();
    public event GameOver gameOverNotify;
    public static GameManager instance;
    private GameState _gameState = GameState.None;



    public enum GameState
    {


        None,
        GameON,
        GameOff

    }

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;

        }

    }

    private void Start()
    {
        //PillarsManager.instance.OnScoreingNotify += Scoreing;
        //UIManager.instance.StartButtonNofity += GameStart;
        //PlayerController.instance.gameOver += GameEnd;

    }

    private void Update()
    {



        WhenGameOver();


    }


    private void OnDisable()
    {
        //PillarsManager.instance.OnScoreingNotify -= Scoreing;
        //UIManager.instance.StartButtonNofity -= GameStart;
        //PlayerController.instance.gameOver -= GameEnd;

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
            SceneManager.LoadScene("MainScene");

        }

    }

    private void Scoreing()
    {

        _Score++;
        Debug.Log(_Score);


    }
    private void UpdateHighScore()
    {

        //if (_Score > _ScoreData.HighScore)
        //{

        //    _ScoreData.HighScore = _Score;

        //}



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
