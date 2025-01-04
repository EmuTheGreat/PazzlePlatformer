using UnityEngine;
using UnityEngine.TextCore.Text;


public class CharacterParams : MonoBehaviour
{
    public Vector2Int startPosition;
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

    private void OnValidate()
    {
        var personsController = FindObjectOfType<PersonsController>();
        personsController.SetCharacterPosition(gameObject, startPosition.x, startPosition.y);
        ApplySettings();
    }

    /// <summary>
    /// ���������� �������� ����� ������� ��������� ����������
    /// </summary>
    private void ApplySettings()
    {
        SetMovementSpeed(movementSpeed);
        SetJumpForce(jumpForce);
        SetMovementDirection(movementDirection);
        SetColliderSize(colliderSize);
    }

    /// <summary>
    /// ��������� ���������� �������
    /// </summary>
    public void SetStartPosition(Vector2Int position)
    {
        startPosition = position;
    }

    /// <summary>
    /// ��������� �������� ������������
    /// </summary>
    public void SetMovementSpeed(float speed)
    {
        movementSpeed = speed;
    }

    /// <summary>
    /// ��������� ���� ������
    /// </summary>
    public void SetJumpForce(float force)
    {
        jumpForce = force;
    }

    /// <summary>
    /// ��������� ������� ����������
    /// </summary>
    public void SetColliderSize(Vector2 size)
    {
        colliderSize = size;
        transform.localScale = colliderSize;
    }

    /// <summary>
    /// ��������� �������
    /// </summary>
    public void SetSprite(SpriteRenderer newSprite)
    {
        spriteRenderer.sprite = newSprite.sprite;
    }

    /// <summary>
    /// ��������� �����
    /// </summary>
    public void SetColor(SpriteRenderer newSprite)
    {
        spriteRenderer.color = newSprite.color;
    }

    /// <summary>
    /// ��������� ����������� ������������
    /// </summary>
    public void SetMovementDirection(int direction)
    {
        movementDirection = direction;
    }

    /// <summary>
    /// ����� ����������� ������������ 
    /// </summary>
    public void ChangeMovementDirection()
    {
        movementDirection *= -1;
    }
}

