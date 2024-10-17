using UnityEngine;

public class CharacterParams : MonoBehaviour
{
    public float movementSpeed;
    public float jumpForce;
    public SpriteRenderer spriteRenderer;
    public Vector2 colliderSize;
    public int movementDirection;

    private BoxCollider2D collider2D;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        collider2D = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        collider2D.size = new Vector2(1, 1);
    }

    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }

    public void SetJumpForce(float force)
    {
        jumpForce = force;
    }

    public void SetColliderSize(Vector2 size)
    {
        transform.localScale = size;
    }

    public void SetSprite(SpriteRenderer newSprite)
    {
        spriteRenderer.sprite = newSprite.sprite;
    }

    public void SetColor(SpriteRenderer newSprite)
    {
        spriteRenderer.color = newSprite.color;
    }

    public void SetMovementDirection(int direction)
    {
        movementDirection = direction;
    }

    public void ChangeMovementDirection()
    {
        movementDirection *= -1;
    }
}

