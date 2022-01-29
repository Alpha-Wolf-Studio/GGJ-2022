using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveForce = 0f;
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private float fallExtraGravity = 0f;
    [SerializeField] private LayerMask jumpeableMask = default;

    private bool death = false;
    private float groundDistance = 0f;
    private float halfWidth = 0f;
    private Rigidbody2D rigid = null;

    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        Bounds bounds = GetComponent<BoxCollider2D>().bounds;
        groundDistance = bounds.extents.y + 0.05f;
        halfWidth = bounds.extents.x;
    }

    private void Update()
    {
        if (death)
            return;

        Move();
        Jump();
        ExtraGravity();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rigid.AddForce(Vector3.left * moveForce * Time.deltaTime, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rigid.AddForce(Vector3.right * moveForce * Time.deltaTime, ForceMode2D.Force);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckGround())
            {
                rigid.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void ExtraGravity()
    {
        if (!CheckGround())
        {
            rigid.AddForce(Physics2D.gravity * fallExtraGravity);
        }
    }

    private bool CheckGround()
    {
        return (Physics2D.Raycast(transform.position - new Vector3(halfWidth, 0f, 0f), Vector2.down, groundDistance,
                    jumpeableMask) ||
                Physics2D.Raycast(transform.position + new Vector3(halfWidth, 0f, 0f), Vector2.down, groundDistance,
                    jumpeableMask));
    }
}
