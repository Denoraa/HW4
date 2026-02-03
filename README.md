# HW4
## Devlog
Control class: PlayerController : MonoSingleton<PlayerController>

Runs core player logic in Update() and Jump(): reads Input.GetKeyDown(KeyCode.Space), applies physics via _rb.velocity, and toggles _rb.simulated based on GameManager.Instance.CurrentGameState.
Emits signals instead of calling other systems: jNotify?.Invoke("JumpAnim") in Jump(), and gameOver?.Invoke() in OnCollisionEnter2D() / OnTriggerEnter2D() when hitting tags "Pillar" or "Ground".
Handles restart without UI coupling: subscribes to GameManager.Instance.onGameRestart in OnEnable() and resets transform.position in ResetPos().

View classes

PlayerAnimation: subscribes to PlayerController.Instance.jNotify in Start(), then updates visuals via _Animator.SetTrigger("Jump") / ResetTrigger("Jump") in Animations(string name).
UIManager: subscribes to PillarsManager.Instance.UpdateScoreUINotify and GameManager.Instance.gameOverNotify to update ScoreText (ScoreUpdate()) and show end-state UI (GameOverTextSpawn(), HighScoreUpdate()).

Events pipeline

Pillar.ScoreDetector() raises OnScoreNotify.
PillarsManager listens (PillarScorer.OnScoreNotify += ScoreGet) and raises OnScoreingNotify + UpdateScoreUINotify in ScoreGet().
GameManager.Start() subscribes (OnScoreingNotify += Scoreing, PlayerController.Instance.gameOver += GameEnd) and broadcasts gameOverNotify + onGameRestart.


Singleton role

MonoSingleton<T>.Instance provides access points (GameManager.Instance, PlayerController.Instance, PillarsManager.Instance, AudioManager.Instance)

## Open-Source Assets
If you added any other assets, list them here!
- [Brackey's Platformer Bundle](https://brackeysgames.itch.io/brackeys-platformer-bundle) - sound effects
- [2D pixel art seagull sprites](https://elthen.itch.io/2d-pixel-art-seagull-sprites) - seagull sprites