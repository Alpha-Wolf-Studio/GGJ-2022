using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float moveForce = 0f;
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private float fallExtraGravity = 0f;
    [SerializeField] private bool doubleJump = false;
    [SerializeField] private bool attackEnabled = false;
    [SerializeField] private float interactionDistance = 0f;
    [SerializeField] private float interactionCooldown = 0f;
    [SerializeField] private Animator animator = null;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private CapsuleCollider2D collider2d = null;
    [SerializeField] private UnityEvent event1 = null;

    public float MoveForce => moveForce;
    public float JumpForce => jumpForce;
    public float FallExtraGravity => fallExtraGravity;
    public float InteractDistance => interactionDistance;
    public float IteractionCooldown => interactionCooldown;
    public bool DoubleJump => doubleJump;
    public bool AttackEnabled => attackEnabled;
    public Animator Anim => animator;
    public SpriteRenderer SpriteRend => spriteRenderer;
    public CapsuleCollider2D Collider2d => collider2d;

    public void CallEvent1()
    {
        event1?.Invoke();
    }
}
