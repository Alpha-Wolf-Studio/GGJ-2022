using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool yinEnabled = true;
    [SerializeField] private PlayerStats yinStats = null;
    [SerializeField] private PlayerStats yangStats = null;
    [SerializeField] private Rigidbody2D rigid = null;
    [SerializeField] private LayerMask jumpeableMask = default;
    [SerializeField] private LayerMask breakeableMask = default;
    [SerializeField] private LayerMask defaultMask = default;

    private PlayerStats currentStats = null;

    private bool isAttacking = false;
    private bool inputEnabled = true;
    private float groundDistance = 0f;
    private float halfWidth = 0f;
    private bool firstJumpStarted = false;

    public void ChangeInputEnable(bool state) => inputEnabled = state;

    private void Start()
    {
        SetStats();
    }

    private void Update()
    {
        if (!inputEnabled)
            return;

        Switch();
        Move();
        Jump();
        Attack();
        ExtraGravity();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            rigid.AddForce(Vector3.left * currentStats.MoveForce * Time.deltaTime, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rigid.AddForce(Vector3.right * currentStats.MoveForce * Time.deltaTime, ForceMode2D.Force);
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckGround() || currentStats.DoubleJump && !firstJumpStarted)
            {
                rigid.AddForce(Vector3.up * currentStats.JumpForce, ForceMode2D.Impulse);

                if (currentStats.DoubleJump)
                {
                    firstJumpStarted = true;
                }
            }
        }
    }

    private void Attack()
    {
        if (!currentStats.AttackEnabled && !isAttacking)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IEnumerator Attacking()
            {
                float timer = 0f;
                while (timer < currentStats.AttackCooldown)
                {
                    timer += Time.deltaTime;

                    RaycastHit2D hit2d = Physics2D.Raycast(transform.position, transform.right, currentStats.AttackDistance, breakeableMask);
                    if (hit2d.collider)
                    {
                        BreakeableObstacle breakeableObstacle = hit2d.collider.GetComponent<BreakeableObstacle>();
                        if (breakeableObstacle)
                        {
                            breakeableObstacle.Break();
                        }
                    }

                    yield return new WaitForEndOfFrame();
                }

                isAttacking = false;
            }

            isAttacking = true;
            StartCoroutine(Attacking());
        }
    }

    private void ExtraGravity()
    {
        if (!CheckGround())
        {
            rigid.AddForce(Physics2D.gravity * currentStats.FallExtraGravity);
        }
        else
        {
            if (currentStats.DoubleJump)
            {
                firstJumpStarted = false;
            }
        }
    }

    private bool CheckGround()
    {
        return (Physics2D.Raycast(transform.position - new Vector3(halfWidth, 0f, 0f), Vector2.down, groundDistance,
                    jumpeableMask) ||
                Physics2D.Raycast(transform.position + new Vector3(halfWidth, 0f, 0f), Vector2.down, groundDistance,
                    jumpeableMask));
    }

    public void Switch()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!Physics2D.BoxCast(transform.position, currentStats.BoxCollider2d.size, 0f, Vector2.zero, 0f, defaultMask))
            {
                yinEnabled = !yinEnabled;
                GameManager.Get().InvertColor();
                SetStats();
            }
        }
    }

    private void SetStats()
    {
        currentStats = yinEnabled ? yinStats : yangStats;
        SetMeasures();

        yinStats.gameObject.SetActive(yinEnabled);
        yangStats.gameObject.SetActive(!yinEnabled);
    }

    private void SetMeasures()
    {
        Vector2 size = currentStats.BoxCollider2d.size;
        groundDistance = transform.localScale.y * size.y / 2 + 0.05f;
        halfWidth = transform.localScale.x * size.x / 2;
    }
}
