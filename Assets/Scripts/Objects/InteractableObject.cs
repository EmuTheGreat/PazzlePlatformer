using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private List<MonoBehaviour> interactionBehaviour; // Скрипт, реализующий IInteractable

    private List<IInteractable> interactable;

    void Awake()
    {
        interactable = new List<IInteractable>();
        foreach (var interaction in interactionBehaviour)
        {
            interactable.Add(interaction as IInteractable);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (interactable.Count > 0)
        {
            foreach (var interaction in interactable)
            {
                if (collision.CompareTag("Player") && interactable != null)
                {
                    interaction.Interact(collision.gameObject);
                }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactable.Count > 0)
        {
            foreach (var interaction in interactable)
            {
                if (collision.CompareTag("Player") && interactable != null)
                {
                    interaction.Interact(collision.gameObject);
                }
            }
        }
    }
}

public interface IInteractable
{
    void Interact(GameObject character);
}

