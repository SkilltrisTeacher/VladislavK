using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]private float speed = 1f;
    [SerializeField] private float jumpForse = 100f;
    [SerializeField] private KeyCode jumpButton = KeyCode.Space;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Collider2D feetCollider;
    private Rigidbody2D myRigibody2D;
    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        myRigibody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void SwitchAnimation(float playerInput)
    {
        playerAnimator.SetBool("Run", playerInput != 0);
    }

    private void Update()
    {
        float playerInput = Input.GetAxis("Horizontal");
        Move(playerInput);
        SwitchAnimation(playerInput);
        Flip(playerInput);
        bool isGrounded = feetCollider.IsTouchingLayers(groundLayer);
        if (Input.GetKeyDown(jumpButton) && isGrounded)
        {
            jump();
        }
    }

    private void Move(float direction)
    {
        Vector2 velocity = myRigibody2D.velocity;
        myRigibody2D.velocity = new Vector2(speed * direction, velocity.y);
    }

    private void jump()
    {
        Vector2 jumpVector = new Vector2(0f, jumpForse);
        myRigibody2D.AddForce(jumpVector);
    }

    private void Flip(float direction)
    {
        if (direction > 0) spriteRenderer.flipX = false;
        if (direction < 0) spriteRenderer.flipX = true;
    }
}