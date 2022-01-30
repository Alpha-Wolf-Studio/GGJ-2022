using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool yinEnabled = false;
    [SerializeField] private float switchCooldown = 0f;
    [SerializeField] private PlayerStats yinStats = null;
    [SerializeField] private PlayerStats yangStats = null;
    [SerializeField] private Rigidbody2D rigid = null;
    [SerializeField] private LayerMask jumpeableMask = default;
    [SerializeField] private LayerMask interactMask = default;
    [SerializeField] private LayerMask defaultMask = default;

    [Header("Audio"), Space]
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip switchYin = null;
    [SerializeField] private AudioClip switchYang = null;

    private PlayerStats currentStats = null;

    private bool switchEnabled = true;
    private bool isFalling = false;
    private bool isInteracting = false;
    private bool flipped = false;
    private bool inputEnabled = true;
    private float groundDistance = 0f;
    private float halfWidth = 0f;
    private bool firstJumpStarted = false;

    public Rigidbody2D Rigid => rigid;
    public bool Death { get; set; } = false;

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

            if (!flipped)
            {
                Flip();
            }
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            rigid.AddForce(Vector3.right * currentStats.MoveForce * Time.deltaTime, ForceMode2D.Force);

            if (flipped)
            {
                Flip();
            }
        }

        currentStats.Anim.SetFloat("Vel", Mathf.Abs(Input.GetAxis("Horizontal")));
    }

    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (!isFalling || (currentStats.DoubleJump && !firstJumpStarted))
            {
                currentStats.Anim.SetTrigger("Jump");

                if (currentStats.DoubleJump)
                {
                    firstJumpStarted = true;
                }
            }
        }
    }

    public void JumpForce()
    {
        rigid.AddForce(Vector3.up * currentStats.JumpForce, ForceMode2D.Impulse);
    }

    private void Attack()
    {
        if (isInteracting)
            return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IEnumerator Attacking()
            {
                float timer = 0f;
                Interact(currentStats.AttackEnabled);
                while (timer < currentStats.IteractionCooldown)
                {
                    timer += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }

                isInteracting = false;
            }

            isInteracting = true;

            if (currentStats.AttackEnabled)
            {
                currentStats.Anim.SetTrigger("Attack");
            }

            StartCoroutine(Attacking());
        }
    }

    private void Interact(bool yingState)
    {
        Vector3 dir = flipped ? Vector3.left : Vector3.right;
        RaycastHit2D hit2d = Physics2D.Raycast(transform.position, dir, currentStats.InteractDistance, interactMask);

        if (hit2d.collider)
        {
            if (yingState)
            {
                BreakeableObstacle breakeableObstacle = hit2d.collider.GetComponent<BreakeableObstacle>();
                if (breakeableObstacle)
                {
                    breakeableObstacle.Break();
                }
            }
            else
            {
                Interactable interactableObject = hit2d.collider.GetComponent<Interactable>();
                if (interactableObject)
                {
                    interactableObject.Interact();
                }
            }
        }
    }

    private void Flip()
    {
        flipped = !flipped;
        currentStats.SpriteRend.flipX = flipped;
    }

    private void ExtraGravity()
    {
        if (!CheckGround())
        {
            rigid.AddForce(Physics2D.gravity * currentStats.FallExtraGravity);
            isFalling = true;
        }
        else
        {
            if (isFalling)
            {
                isFalling = false;
                currentStats.Anim.SetTrigger("Land");
            }
            if (currentStats.DoubleJump)
            {
                firstJumpStarted = false;
            }
        }
    }

    private bool CheckGround()
    {
        return (Physics2D.Raycast(currentStats.transform.position - new Vector3(halfWidth, 0f, 0f), Vector2.down, groundDistance,
                    jumpeableMask) ||
                Physics2D.Raycast(currentStats.transform.position + new Vector3(halfWidth, 0f, 0f), Vector2.down, groundDistance,
                    jumpeableMask));
    }

    public void Switch()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (!Physics2D.BoxCast(transform.position, currentStats.Collider2d.size, 0f, Vector2.zero, 0f, defaultMask) && switchEnabled)
            {
                switchEnabled = false;
                Invoke(nameof(SwitchEnabled), switchCooldown);

                yinEnabled = !yinEnabled;
                GameManager.Get().InvertColor();
                SetStats();

                audioSource.clip = yinEnabled ? switchYang : switchYin;
                audioSource.Play();
            }
        }
    }

    private void SwitchEnabled() => switchEnabled = true;

    private void SetStats()
    {
        currentStats = yinEnabled ? yinStats : yangStats;
        currentStats.SpriteRend.flipX = flipped;

        yinStats.gameObject.SetActive(yinEnabled);
        yangStats.gameObject.SetActive(!yinEnabled);

        SetMeasures();
    }

    private void SetMeasures()
    {
        Vector2 size = currentStats.Collider2d.size;
        groundDistance = size.y / 2 + 0.05f;
        halfWidth = size.x / 2 - 0.1f;
    }

    public void Dead(bool death)
    {
        Death = death;
        currentStats.Anim.SetBool("Death", death);
        ChangeInputEnable(!death);
    }
}
