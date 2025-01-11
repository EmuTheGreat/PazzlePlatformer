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
            RestartScene(); // Перезапуск сцены
        }
    }

    void RestartScene()
    {
        var persons = FindObjectsByType<CharacterParams>(FindObjectsSortMode.None);
        var personsController = FindObjectOfType<PersonsController>();
        foreach (var person in persons)
        {
            personsController.SetCharacterPosition(person.gameObject, person.startPosition.x, person.startPosition.y);
        }
    }
}
