using UnityEngine;
using System.Collections.Generic;

public class KeyObject : MonoBehaviour
{
    public List<GameObject> assignedCharacters;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (assignedCharacters.Contains(other.gameObject))
        {
            KeyLevelControl keyLevelControl = FindObjectOfType<KeyLevelControl>();
            if (keyLevelControl != null)
            {
                keyLevelControl.KeyCollected();
            }

            Destroy(gameObject);
        }
    }
}
