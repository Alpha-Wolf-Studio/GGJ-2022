﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private bool yingEnabled = false;
    [SerializeField] private PlayerStats yingStats = null;
    [SerializeField] private PlayerStats yangStats = null;
    [SerializeField] private Rigidbody2D rigid = null;
    [SerializeField] private LayerMask jumpeableMask = default;
    [SerializeField] private LayerMask defaultMask = default;

    private PlayerStats currentStats = null;

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
                yingEnabled = !yingEnabled;
                SetStats();
            }
        }
    }

    private void SetStats()
    {
        currentStats = yingEnabled ? yingStats : yangStats;
        SetMeasures();

        yingStats.gameObject.SetActive(yingEnabled);
        yangStats.gameObject.SetActive(!yingEnabled);
    }

    private void SetMeasures()
    {
        Vector2 size = currentStats.BoxCollider2d.size;
        groundDistance = transform.localScale.y * size.y / 2 + 0.05f;
        halfWidth = transform.localScale.x * size.x / 2;
    }
}
