using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4;
    public float minJumpHeight = 1;
    public float timeToJumpApex = .4f;
    public GameOverManager gom;
    public GroundSpawer gs;

    float accelerationTimeAirborne = .15f;
    float accelerationTimeGrounded = .1f;
    float moveSpeed = 6;

    float gravity;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;
    bool died;

    Controller2D controller;

    void Start()
    {
        controller = GetComponent<Controller2D>();

        died = false;

        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
    }

    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), 0);

        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
            velocity.y = maxJumpVelocity;

        if (Input.GetKeyUp(KeyCode.Space))
            if(velocity.y > minJumpVelocity)
                velocity.y = minJumpVelocity;

        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below) ? accelerationTimeGrounded : accelerationTimeAirborne);
        velocity.y += gravity * Time.deltaTime;
        if (!died)
        {
            controller.Move(velocity * Time.deltaTime);

            if (transform.position.y < -Camera.main.orthographicSize - 6f)
            {
                died = true;
                gom.GameOver();
            }
        }

        if (controller.collisions.above)
            velocity.y = 0;
        if (controller.collisions.below)
            velocity.y = 0;
    }
}