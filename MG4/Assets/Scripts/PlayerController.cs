using UnityEngine;
using static GameManager;





public class PlayerController : MonoSingleton<PlayerController>
{


    #region Events 
    public delegate void Player_JumpListener(string AnimName);
    public event Player_JumpListener jNotify;
    public delegate void GameOver();
    public event GameOver gameOver;
    #endregion

    private float _jumpForce = 3f;
    private Rigidbody2D _rb;

    private Vector2 Reset_position;

    void Start()
    {

        _rb = GetComponent<Rigidbody2D>();

        _rb.simulated = false;
        Reset_position = this.transform.position;
    }

    private void OnEnable()
    {
        GameManager.Instance.onGameRestart += ResetPos;
    }
    void Update()
    {

        if (GameManager.Instance.CurrentGameState == GameManager.GameState.GameON)
        {
            _rb.simulated = true;
            Jump();

        }
        else
        {
            _rb.simulated = false;
        }

    }

    void ResetPos()
    {
        this.transform.position = Reset_position;
    }

    private void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioManager.Instance.PlaySFX(true);
            _rb.velocity = new Vector2(0, _jumpForce);
            jNotify?.Invoke("JumpAnim");


        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.CompareTag("Pillar") )
        {
            if (GameManager.Instance._gameState != GameState.GameOff)
                gameOver?.Invoke();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (GameManager.Instance._gameState != GameState.GameOff)
                gameOver?.Invoke();
        }
    }
    public Rigidbody2D playerRb
    {

        get
        {

            return _rb;

        }

    }







}
