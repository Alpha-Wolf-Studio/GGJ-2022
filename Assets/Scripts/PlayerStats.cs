using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float moveForce = 0f;
    [SerializeField] private float jumpForce = 0f;
    [SerializeField] private float fallExtraGravity = 0f;
    [SerializeField] private bool doubleJump = false;
    [SerializeField] private bool attackEnabled = false;
    [SerializeField] private float interactionDistance = 0f;
    [SerializeField] private float interactionCooldown = 0f;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private BoxCollider2D boxCollider2d = null;

    public float MoveForce => moveForce;
    public float JumpForce => jumpForce;
    public float FallExtraGravity => fallExtraGravity;
    public float InteractDistance => interactionDistance;
    public float AttackCooldown => interactionCooldown;
    public bool DoubleJump => doubleJump;
    public bool AttackEnabled => attackEnabled;
    public SpriteRenderer SpriteRend => spriteRenderer;
    public BoxCollider2D BoxCollider2d => boxCollider2d;
}
