using UnityEngine;

public class PlatformController : RaycastController
{
    public LayerMask playerMask;
    public float speed;

    [HideInInspector]
    public bool move;

    float screenBottom;

    enum Place { Above, Under }

    public override void Start()
    {
        base.Start();
        move = true;
        screenBottom = -(Camera.main.aspect * Camera.main.orthographicSize);
    }

    void Update()
    {
        Vector3 velocity = new Vector3(0, -speed * Time.deltaTime);

        MovePlayer(velocity, Place.Under);

        if(move)
            transform.Translate(velocity);

        MovePlayer(velocity, Place.Above);

        if (transform.position.y < screenBottom - 1f)
            move = false;
    }

    void MovePlayer(Vector3 velocity, Place place)
    {
        UpdateRaycastOrigins();

        for (int i = 0; i < verticalRayCount; i++)
        {
            float rayLength = Mathf.Abs(velocity.y) + skinWidth;
            Vector2 rayOrigin = place == Place.Under ? raycastOrigins.bottomLeft + skinWidth * Vector2.down + Vector2.right * 0.2f : raycastOrigins.topLeft + skinWidth * Vector2.up + Vector2.right * 0.2f;
            rayOrigin +=  Vector2.right * (verticalRaySpacing * i) + Vector2.left * 0.2f;
            RaycastHit2D hit = place == Place.Under ? Physics2D.Raycast(rayOrigin, Vector2.down, rayLength + skinWidth * 5f, playerMask) : Physics2D.Raycast(rayOrigin, Vector2.up, rayLength + skinWidth * 4f, playerMask);

            Debug.DrawRay(rayOrigin, place == Place.Under ? Vector2.down * (rayLength + skinWidth * 5f) : Vector2.up * (rayLength + skinWidth * 4f), Color.red);

            if (hit)
            {
                rayLength = hit.distance;
                if (place == Place.Under)
                    hit.transform.Translate(velocity);
                else
                    hit.transform.Translate(velocity/5f);
            }
        }
    }
}