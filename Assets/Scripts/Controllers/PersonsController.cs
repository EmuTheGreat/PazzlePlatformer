using System;
using System.Collections.Generic;
using UnityEngine;

public class PersonsController : MonoBehaviour
{
    [SerializeField]
    private GameObject personsList;
    [SerializeField]
    private GameObject collideObjectsList;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject playerPrefabForChangeSkin;

    public List<GameObject> objects;
    public Vector2 objectPosition = new Vector2(0, 0);
    public Vector2 colliderSize = new Vector2(1, 1);

    public float movementSpeed = 5f;
    public float jumpForce = 10f;

    public KeyCode moveLeftKey = KeyCode.A;
    public KeyCode moveRightKey = KeyCode.D;
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode specialActionKey = KeyCode.F;

    public bool movementDirectionReverse = false;


    public List<GameObject> collisionObjects; // Объекты для возможных столкновений
    public bool removeOnCollision = false;

    void Start()
    {

    }

    public void CreateCharacter()
    {
        if (playerPrefab != null)
        {
            GameObject newCharacter = Instantiate(playerPrefab, objectPosition, Quaternion.identity, personsList.transform);
            CharacterParams characterParams = newCharacter.GetComponent<CharacterParams>();

            characterParams.SetMovementSpeed(movementSpeed);
            characterParams.SetJumpForce(jumpForce);
            characterParams.SetColliderSize(colliderSize);
            characterParams.SetMovementDirection(movementDirectionReverse ? -1: 1);

            objects.Add(newCharacter);
        }
    }

    public void ChangeAllCharacterSkin()
    {
        if (playerPrefabForChangeSkin != null)
        {
            var persons = GetPersons();

            foreach (var person in persons)
            {
                person.GetComponent<CharacterParams>()
                    .SetSprite(playerPrefabForChangeSkin.transform.GetChild(0).GetComponent<SpriteRenderer>());

                //Temp
                person.GetComponent<CharacterParams>()
                    .SetColor(playerPrefabForChangeSkin.transform.GetChild(0).GetComponent<SpriteRenderer>());
            }
        }
    }

    [HideInInspector]
    public int index;

    public void ChangeCharacterSkin()
    {
        if (playerPrefabForChangeSkin != null)
        {
            var persons = GetPersons();
            try
            {
                var person = persons[index];

                person.GetComponent<CharacterParams>()
                    .SetSprite(playerPrefabForChangeSkin.transform.GetChild(0).GetComponent<SpriteRenderer>());

                //Temp
                person.GetComponent<CharacterParams>()
                    .SetColor(playerPrefabForChangeSkin.transform.GetChild(0).GetComponent<SpriteRenderer>());
            }
            catch (ArgumentOutOfRangeException e) { }
        }
    }

    public void ChangeCharacterSkin(GameObject character, SpriteRenderer newSkin)
    {
        CharacterParams characterParams = character.GetComponent<CharacterParams>();
        characterParams.SetSprite(newSkin);
    }

    // Direction change
    public void ChangeMovementDirection(GameObject character, int direction = 1)
    {
        CharacterParams characterParams = character.GetComponent<CharacterParams>();
        characterParams.SetMovementDirection(direction);
    }

    // Set position by LevelEditor
    public void SetCharacterPosition(GameObject character, int x, int y)
    {
        Vector2 position = LevelEditor.GetPositionFromGrid(x, y);
        CharacterParams characterParams = character.GetComponent<CharacterParams>();
        characterParams.transform.position = position;
    }

    public List<GameObject> GetPersons()
    {
        var result = new List<GameObject>();
        for (int i = 0; i < personsList.transform.childCount; i++)
        {
            result.Add(personsList.transform.GetChild(i).gameObject);
        }

        return result;
    }
}

 