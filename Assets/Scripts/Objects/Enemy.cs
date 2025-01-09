using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool isStatic;
    public float moveSpeed = 2f;
    public Vector2 pointA;
    public Vector2 pointB;

    private Vector2 nextPosition;

    void Start()
    {
        nextPosition = pointB;
    }

    void Update()
    {
        if (!isStatic)
        {
            transform.position = Vector2.MoveTowards(transform.position, nextPosition, moveSpeed * Time.deltaTime);
            if ((Vector2)transform.position == pointA)
            {
                nextPosition = pointB;
            }
            else if ((Vector2)transform.position == pointB)
            {
                nextPosition = pointA;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}