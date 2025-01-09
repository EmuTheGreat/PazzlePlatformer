using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class PersonsController : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField] public GameObject personsList;
    [SerializeField] public List<GameObject> playerPrefabs = new List<GameObject>();

    [Header("Character Parameters")]
    public Vector2 objectPosition = new Vector2(0, 0);
    public Vector2 colliderSize = new Vector2(1, 1);
    public float movementSpeed = 5f;
    public float jumpForce = 10f;
    public bool movementDirectionReverse = false;

    public List<GameObject> persons = new List<GameObject>();
    public int selectedPrefabIndex = 0;
    public GameObject selectedCharacter;

    public void CreateCharacter()
    {
        if (playerPrefabs.Count > 0 && personsList != null)
        {
            GameObject selectedPrefab = playerPrefabs[selectedPrefabIndex];
            GameObject newCharacter = Instantiate(selectedPrefab, objectPosition, Quaternion.identity, personsList.transform);

            CharacterParams characterParams = newCharacter.GetComponent<CharacterParams>();

            SetCharacterPosition(newCharacter, (int)objectPosition.x, (int)objectPosition.y);

            characterParams.SetSprite(selectedPrefab.GetComponentInChildren<SpriteRenderer>().sprite);
            characterParams.SetStartPosition(new Vector2Int((int)objectPosition.x, (int)objectPosition.y));
            characterParams.SetMovementSpeed(movementSpeed);
            characterParams.SetJumpForce(jumpForce);
            characterParams.SetColliderSize(colliderSize);
            characterParams.SetMovementDirection(movementDirectionReverse ? -1 : 1);

            persons.Add(newCharacter);
        }
    }

    private void OnEnable()
    {
        persons = new List<GameObject>(FindObjectsOfType<CharacterParams>().Select(c => c.gameObject));
    }


    public void RemoveCharacter(GameObject character)
    {
        if (character != null && persons.Contains(character))
        {
            persons.Remove(character);
            DestroyImmediate(character);
        }
        else
        {
            Debug.LogWarning("Character not found or already removed.");
        }
    }

    public void ChangeSelectedCharacterSkin()
    {
        if (selectedCharacter != null && playerPrefabs.Count > 0)
        {
            CharacterParams characterParams = selectedCharacter.GetComponent<CharacterParams>();
            GameObject selectedSkinPrefab = playerPrefabs[selectedPrefabIndex];
            characterParams.SetSprite(selectedSkinPrefab.GetComponentInChildren<SpriteRenderer>().sprite);
        }
        else
        {
            Debug.LogWarning("No character selected or no skins available.");
        }
    }

    public void ChangeAllCharacterSkin()
    {
        if (playerPrefabs.Count > 0)
        {
            GameObject selectedSkinPrefab = playerPrefabs[selectedPrefabIndex];
            foreach (var person in persons)
            {
                CharacterParams characterParams = person.GetComponent<CharacterParams>();
                characterParams.SetSprite(selectedSkinPrefab.GetComponentInChildren<SpriteRenderer>().sprite);
            }
        }
    }

    public void SetCharacterPosition(GameObject character, int x, int y)
    {
        LevelEditor levelEditor = FindObjectOfType<LevelEditor>();
        if (levelEditor != null)
        {
            Vector2 position = levelEditor.GetPositionFromGrid(x, y);
            character.transform.position = position;
        }
        else
        {
            Debug.LogWarning("LevelEditor not found in the scene.");
        }
    }

    public void ClearCharacters()
    {
        foreach (var character in persons)
        {
            DestroyImmediate(character);
        }
        persons.Clear();
    }

    public void SelectPrefab(int index)
    {
        if (index >= 0 && index < playerPrefabs.Count)
        {
            selectedPrefabIndex = index;
        }
    }
}