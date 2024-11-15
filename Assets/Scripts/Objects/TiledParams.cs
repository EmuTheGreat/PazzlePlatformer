using UnityEngine;

public class TiledParams : MonoBehaviour
{
    public bool hasCollision = true;
    public bool isEnemy = false;
    public bool isKey = false;

    public void SetCollision(bool collision)
    {
        hasCollision = collision;
        Collider2D collider = GetComponent<Collider2D>();
        if (collider != null)
        {
            collider.enabled = collision;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnemy && other.CompareTag("Player"))
        {
            // Удаление игрока с уровня или уменьшение счетчика
        }
        else if (isKey && other.CompareTag("Player"))
        {
            // Логика перехода на следующий уровень
        }
    }
}
