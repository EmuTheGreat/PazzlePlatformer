using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MoveController : MonoBehaviour
{
    [SerializeField]
    private GameObject PersonsParent;
    private List<CharacterParams> persons;
    private List<Rigidbody2D> rigidbodies;
    private List<Collider2D> colliders;

    private float moveInput;

    private int count;

    void Start()
    {
        persons = GetChildren(PersonsParent);
        rigidbodies = GetRigidbodies(PersonsParent);
        colliders = GetColliders(PersonsParent);
        count = persons.Count;
    }

    void Update()
    {
        if (count != PersonsParent.transform.childCount)
        {
            persons = GetChildren(PersonsParent);
            rigidbodies = GetRigidbodies(PersonsParent);
            colliders = GetColliders(PersonsParent);
            count = rigidbodies.Count;

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

    // T GetColliders<T>(GameObject parent)

    List<Collider2D> GetColliders(GameObject parent) 
    {
        List<Collider2D> children = new List<Collider2D>();

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var childTransform = parent.transform.GetChild(i).GetComponent<Collider2D>();
            children.Add(childTransform);
        }

        return children;
    }

    List<CharacterParams> GetChildren(GameObject parent)
    {
        List<CharacterParams> children = new List<CharacterParams>();

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var childTransform = parent.transform.GetChild(i).GetComponent<CharacterParams>();
            children.Add(childTransform);
        }

        return children;
    }

    List<Rigidbody2D> GetRigidbodies(GameObject parent)
    {
        List<Rigidbody2D> rigidbodies = new List<Rigidbody2D>();

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            var childTransform = parent.transform.GetChild(i);
            rigidbodies.Add(childTransform.GetComponent<Rigidbody2D>());
        }

        return rigidbodies;
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
