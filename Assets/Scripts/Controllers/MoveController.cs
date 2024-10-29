using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Unity.VisualScripting.Metadata;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    private GameObject PersonsParent;
    private List<CharacterParams> persons = new();
    private List<Rigidbody2D> rigidbodies = new();
    private List<Collider2D> colliders = new();

    private float moveInput;

    private int count;

    void Start()
    {
        persons = GetObjects<CharacterParams>(PersonsParent);
        rigidbodies = GetObjects<Rigidbody2D>(PersonsParent);
        colliders = GetObjects<Collider2D>(PersonsParent);
        count = persons.Count;

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

            IgnoreCollision();
            Debug.Log(count);
        }
        moveInput = Input.GetAxis("Horizontal");

        for (int i = 0; i < persons.Count; i++)
        {
            rigidbodies[i].velocity = new Vector2(moveInput * persons[i].movementSpeed * persons[i].movementDirection, rigidbodies[i].velocity.y);
        }

        if (Input.GetButtonDown("Jump"))
        {
            for (int i = 0; i < persons.Count; i++)
            {
                rigidbodies[i].velocity = new Vector2(rigidbodies[i].velocity.x, persons[i].jumpForce);
            }
        }
    }

    List<T> GetObjects<T>(GameObject parent)
    {
        List<T> children = new List<T>();
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var childTransform = parent.transform.GetChild(i).GetComponent<T>();
            children.Add(childTransform);
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
}
