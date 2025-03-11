using UnityEngine;

public class AIController : MonoBehaviour
{
    public Transform ball;              // 공 오브젝트의 Transform
    public float fixedSpeed = 5f;         // AI 바의 고정 이동 속도 (5)
    public float reactionThreshold = 0.1f; // 목표와의 최소 오차값
    public float topBoundary = 4.5f;      // 위쪽 벽의 y 좌표
    public float bottomBoundary = -4.5f;  // 아래쪽 벽의 y 좌표

    private Rigidbody2D rb;
    private bool canMoveUp = true;
    private bool canMoveDown = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Rigidbody2D ballRb = ball.GetComponent<Rigidbody2D>();
        Vector2 ballVelocity = ballRb.velocity;
        float targetY = 0f;

        // AI는 우측에 있으므로, 공이 오른쪽으로 이동 중이고, 공이 AI 바의 왼쪽에 있을 때 예측 진행
        if (transform.position.x > ball.position.x && ballVelocity.x > 0)
        {
            targetY = PredictBallYSingleBounce();
        }
        else
        {
            // 공이 AI 쪽으로 오지 않으면 중앙으로 복귀
            targetY = 0f;
        }

        // 현재 AI 바의 y와 목표 y의 차이가 reactionThreshold보다 크면 이동 결정
        float direction = 0f;
        if (Mathf.Abs(targetY - transform.position.y) > reactionThreshold)
        {
            direction = (targetY > transform.position.y) ? 1f : -1f;
        }

        // 플레이어와 동일하게, 벽에 닿으면 해당 방향으로 더 이상 움직이지 못하도록 제한
        if (direction > 0 && !canMoveUp)
            direction = 0;
        if (direction < 0 && !canMoveDown)
            direction = 0;

        // AI 바의 이동 속도는 고정 (5)
        float currentSpeed = fixedSpeed;
        Vector2 newPosition = rb.position + Vector2.up * direction * currentSpeed * Time.fixedDeltaTime;
        newPosition.y = Mathf.Clamp(newPosition.y, bottomBoundary, topBoundary);
        rb.MovePosition(newPosition);
    }

    // 공이 AI 바의 x 좌표에 도달할 때의 y 좌표를 한 번의 벽 반사를 고려해 예측하는 함수
    float PredictBallYSingleBounce()
    {
        float aiX = transform.position.x;  // AI 바의 x 좌표
        Vector2 pos = ball.position;
        Vector2 vel = ball.GetComponent<Rigidbody2D>().velocity;

        // AI 바의 x 좌표에 도달하는 데 걸리는 시간 계산
        float dtVertical = Mathf.Abs((aiX - pos.x) / vel.x);

        // 위 또는 아래 벽에 충돌할 시간 계산
        float dtBounce = float.PositiveInfinity;
        float boundaryY = pos.y;

        if (vel.y > 0)
        {
            dtBounce = (topBoundary - pos.y) / vel.y;
            boundaryY = topBoundary;
        }
        else if (vel.y < 0)
        {
            dtBounce = Mathf.Abs((bottomBoundary - pos.y) / vel.y);
            boundaryY = bottomBoundary;
        }

        // AI 바에 도달하기 전에 벽 충돌이 없으면 바로 예측
        if (dtVertical <= dtBounce)
        {
            return pos.y + vel.y * dtVertical;
        }
        else
        {
            // 한 번의 벽 반사가 발생하는 경우
            Vector2 posAfterBounce = new Vector2(pos.x + vel.x * dtBounce, boundaryY);
            Vector2 newVel = new Vector2(vel.x, -vel.y); // y 방향 반전
            float dtRemaining = dtVertical - dtBounce;
            return posAfterBounce.y + newVel.y * dtRemaining;
        }
    }

    // AI 바가 벽과 충돌 시 해당 방향 이동 제한
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            // 충돌한 벽의 위치에 따라 위쪽, 아래쪽 이동 제한
            if (collision.transform.position.y > transform.position.y)
                canMoveUp = false;
            else if (collision.transform.position.y < transform.position.y)
                canMoveDown = false;
        }
    }

    // 벽과의 충돌이 끝나면 다시 이동 가능하도록 복원
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boundary"))
        {
            if (collision.transform.position.y > transform.position.y)
                canMoveUp = true;
            else if (collision.transform.position.y < transform.position.y)
                canMoveDown = true;
        }
    }
}
