using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;
public class UIManager : MonoBehaviour {

    #region Events
    public delegate void OnStartClick();
    public event OnStartClick StartButtonNofity;
    #endregion

    [SerializeField] private GameObject TitleText;
    [SerializeField] private GameObject StartButton;
    [SerializeField] private GameObject ScoreText;
    [SerializeField] private GameObject HighScoreText;
    [SerializeField] private GameObject GameOverText;


    void Start()
    {

        HighScoreUpdate();
        StartButton.GetComponent<Button>().onClick.AddListener(ClickOnStart);
        PillarsManager.Instance.UpdateScoreUINotify += ScoreUpdate;
        GameManager.Instance.gameOverNotify += GameOverTextSpawn;
        GameManager.Instance.gameOverNotify += HighScoreUpdate;



    }

    private void OnDisable()
    {
        PillarsManager.Instance.UpdateScoreUINotify -= ScoreUpdate;
        GameManager.Instance.gameOverNotify -= GameOverTextSpawn;
        GameManager.Instance.gameOverNotify -= HighScoreUpdate;
        GameManager.Instance.onGameRestart -= Menu;
    }
    void Menu()
    {
        TitleText.SetActive(true);
        StartButton.SetActive(true);
        GameOverText.SetActive(false);
    }

    private void ClickOnStart()
    {
        GameManager.Instance._gameState = GameState.GameON;
        PlayerController.Instance.playerRb.velocity = Vector2.zero;
        StartButtonNofity?.Invoke();
        TitleText.SetActive(false);
        StartButton.SetActive(false);


    }
    private void ScoreUpdate()
    {

        ScoreText.GetComponent<TextMeshProUGUI>().text = "Score: " + GameManager.Instance.Score.ToString();

    }
    private void GameOverTextSpawn()
    {

        GameOverText.SetActive(true);

    }
    private void HighScoreUpdate()
    {


        HighScoreText.GetComponent<TextMeshProUGUI>().text = "HighestScore: " + GameManager.Instance._ScoreData.HighScore.ToString();
        

    }
}
