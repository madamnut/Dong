using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Score UI")]
    public Text playerScoreText;
    public Text opponentScoreText;

    [Header("Game Objects")]
    public Ball ball;
    public Transform playerPaddle;
    public Transform aiPaddle;

    private Vector2 ballInitialPos;
    private Vector2 playerPaddleInitialPos;
    private Vector2 aiPaddleInitialPos;

    private int playerScore = 0;
    private int opponentScore = 0;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        // 초기 위치 저장
        ballInitialPos = ball.transform.position;
        playerPaddleInitialPos = playerPaddle.position;
        aiPaddleInitialPos = aiPaddle.position;
        UpdateScoreUI();
    }

    public void ScorePoint(bool playerScored)
    {
        // 점수 업데이트
        if (playerScored)
            playerScore++;
        else
            opponentScore++;

        UpdateScoreUI();

        // 점수가 난 후 공을 멈추고 1초 후 초기 상태로 리셋
        StartCoroutine(ResetRound());
    }

    void UpdateScoreUI()
    {
        playerScoreText.text = playerScore.ToString();
        opponentScoreText.text = opponentScore.ToString();
    }

    IEnumerator ResetRound()
    {
        // 공 멈춤 처리 (BallController에서 처리한 것과 동일한 방식)
        ball.StopBall();

        yield return new WaitForSeconds(1f);

        // 공과 패들을 초기 위치로 복원
        ball.transform.position = ballInitialPos;
        playerPaddle.position = playerPaddleInitialPos;
        aiPaddle.position = aiPaddleInitialPos;

        // 공을 다시 발사
        ball.LaunchBall();
    }
}
