using UnityEngine;

public class KeyObject : MonoBehaviour
{
    public string[] validCharacterTags;
    public string[] invalidCharacterTags;

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (var tag in validCharacterTags)
        {
            if (other.CompareTag(tag))
            {
                FindObjectOfType<KeyLevelControl>().KeyCollected();
                Destroy(other.gameObject);
                Destroy(gameObject);
                return;
            }
        }

        foreach (var tag in invalidCharacterTags)
        {
            if (other.CompareTag(tag))
            {
                return;
            }
        }
    }
}