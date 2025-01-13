using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    private GameObject PersonsParent;
    private List<CharacterParams> persons = new();
    private List<Rigidbody2D> rigidbodies = new();
    private List<Collider2D> colliders = new();

    private float moveInput;
    private int count;

    private List<bool> isGrounded = new();

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float groundCheckHeight = 0.1f; // Высота области проверки

    private Camera mainCamera;

    void Start()
    {
        persons = GetObjects<CharacterParams>(PersonsParent);
        rigidbodies = GetObjects<Rigidbody2D>(PersonsParent);
        colliders = GetObjects<Collider2D>(PersonsParent);
        count = persons.Count;
        isGrounded = new List<bool>(new bool[count]);

        mainCamera = Camera.main;
        IgnoreCollision();
    }

    void Update()
    {
        if (count != PersonsParent.transform.childCount)
        {
            persons = GetObjects<CharacterParams>(PersonsParent);
            rigidbodies = GetObjects<Rigidbody2D>(PersonsParent);
            colliders = GetObjects<Collider2D>(PersonsParent);
            count = persons.Count;
            isGrounded = new List<bool>(new bool[count]);

            IgnoreCollision();
            Debug.Log(count);
        }

        moveInput = Input.GetAxis("Horizontal");

        for (int i = 0; i < persons.Count; i++)
        {
            Vector2 velocity = rigidbodies[i].velocity;
            velocity.x = moveInput * persons[i].movementSpeed * persons[i].movementDirection;
            rigidbodies[i].velocity = velocity;

            if (moveInput > 0)
            {
                persons[i].transform.localScale = new Vector3(Mathf.Abs(persons[i].transform.localScale.x), persons[i].transform.localScale.y, persons[i].transform.localScale.z);
            }
            else if (moveInput < 0)
            {
                persons[i].transform.localScale = new Vector3(-Mathf.Abs(persons[i].transform.localScale.x), persons[i].transform.localScale.y, persons[i].transform.localScale.z);
            }

            // Проверка, стоит ли персонаж на земле, с использованием ширины коллайдера
            float groundCheckWidth = colliders[i].bounds.size.x; // Ширина коллайдера
            Vector2 groundCheckPosition = new Vector2(colliders[i].bounds.center.x, colliders[i].bounds.min.y - groundCheckHeight / 2);
            Vector2 groundCheckSize = new Vector2(groundCheckWidth, groundCheckHeight);

            isGrounded[i] = Physics2D.OverlapBox(groundCheckPosition, groundCheckSize, 0f, groundLayer);

            RestrictMovementToCameraBounds(rigidbodies[i]);
        }

        if (Input.GetButtonDown("Jump"))
        {
            for (int i = 0; i < persons.Count; i++)
            {
                if (isGrounded[i])
                {
                    rigidbodies[i].velocity = new Vector2(rigidbodies[i].velocity.x, persons[i].jumpForce);
                }
            }
        }
    }

    private void RestrictMovementToCameraBounds(Rigidbody2D rb)
    {
        Vector3 viewPos = rb.transform.position;
        Vector3 minBounds = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 maxBounds = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0));

        viewPos.x = Mathf.Clamp(viewPos.x, minBounds.x + colliders[0].bounds.extents.x, maxBounds.x - colliders[0].bounds.extents.x);
        viewPos.y = Mathf.Clamp(viewPos.y, minBounds.y + colliders[0].bounds.extents.y, maxBounds.y - colliders[0].bounds.extents.y);

        rb.transform.position = viewPos;
    }

    List<T> GetObjects<T>(GameObject parent)
    {
        List<T> children = new List<T>();
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var childTransform = parent.transform.GetChild(i).GetComponent<T>();
            if (childTransform != null)
            {
                children.Add(childTransform);
            }
        }
        return children;
    }

    private void TurnOffCollision(bool flag)
    {
        for (int i = 0; i < count; i++)
        {
            Collider2D collider1 = colliders[i];
            for (int j = i + 1; j < count; j++)
            {
                Collider2D collider2 = colliders[j];
                Physics2D.IgnoreCollision(collider1, collider2, flag);
            }
        }
    }

    public void IgnoreCollision() => TurnOffCollision(true);

    public void EnableCollision() => TurnOffCollision(false);

    private void OnDrawGizmos()
    {
        if (colliders == null || colliders.Count == 0) return;

        Gizmos.color = Color.green;

        for (int i = 0; i < colliders.Count; i++)
        {
            float groundCheckWidth = colliders[i].bounds.size.x; // Ширина коллайдера
            Vector2 groundCheckPosition = new Vector2(colliders[i].bounds.center.x, colliders[i].bounds.min.y - groundCheckHeight / 2);
            Vector2 groundCheckSize = new Vector2(groundCheckWidth, groundCheckHeight);

            Gizmos.DrawWireCube(groundCheckPosition, groundCheckSize);
        }
    }
}
