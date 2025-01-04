using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PersonsController : MonoBehaviour
{
    [Header("Character Settings")]
    [SerializeField]
    public GameObject personsList;
    [SerializeField]
    public List<GameObject> playerPrefabs = new List<GameObject>();

    [Header("Character Parameters")]
    public Vector2 objectPosition = new Vector2(0, 0);
    public Vector2 colliderSize = new Vector2(1, 1);
    public float movementSpeed = 5f;
    public float jumpForce = 10f;
    public bool movementDirectionReverse = false;

    private List<GameObject> objects = new List<GameObject>();
    public int selectedPrefabIndex = 0;

    private void OnValidate()
    {
        if (personsList == null)
        {
            personsList = new GameObject("PersonsList");
        }
    }

    public void CreateCharacter()
    {
        if (playerPrefabs.Count > 0 && personsList != null)
        {
            GameObject selectedPrefab = playerPrefabs[selectedPrefabIndex];
            GameObject newCharacter = Instantiate(selectedPrefab, objectPosition, Quaternion.identity, personsList.transform);

            CharacterParams characterParams = newCharacter.GetComponent<CharacterParams>();

            SetCharacterPosition(newCharacter, (int)objectPosition.x, (int)objectPosition.y);

            characterParams.SetStartPosition(new Vector2Int((int)objectPosition.x, (int)objectPosition.y));
            characterParams.SetMovementSpeed(movementSpeed);
            characterParams.SetJumpForce(jumpForce);
            characterParams.SetColliderSize(colliderSize);
            characterParams.SetMovementDirection(movementDirectionReverse ? -1 : 1);

            objects.Add(newCharacter);
        }
    }

    public void RemoveCharacter(GameObject character)
    {
        if (objects.Contains(character))
        {
            objects.Remove(character);
            DestroyImmediate(character);
        }
    }

    public void ChangeAllCharacterSkin()
    {
        if (playerPrefabs.Count > 0)
        {
            GameObject selectedSkinPrefab = playerPrefabs[selectedPrefabIndex];
            foreach (var person in objects)
            {
                CharacterParams characterParams = person.GetComponent<CharacterParams>();
                characterParams.SetSprite(selectedSkinPrefab.transform.GetChild(0).GetComponent<SpriteRenderer>());
            }
        }
    }

    public void SetCharacterPosition(GameObject character, int x, int y)
    {
        LevelEditor levelEditor = FindObjectOfType<LevelEditor>();
        Vector2 position = levelEditor.GetPositionFromGrid(x, y);
        character.transform.position = position;
    }

    public void ClearCharacters()
    {
        foreach (var character in objects)
        {
            DestroyImmediate(character);
        }
        objects.Clear();
    }

    public void SelectPrefab(int index)
    {
        if (index >= 0 && index < playerPrefabs.Count)
        {
            selectedPrefabIndex = index;
        }
    }
}

