using System.Collections.Generic;
using UnityEngine;

public class PersonsController : MonoBehaviour
{
    [SerializeField]
    private GameObject personsList;
    [SerializeField]
    private GameObject playerPrefab;
    [SerializeField]
    private GameObject playerPrefabForChangeSkin;
    [HideInInspector]
    public int index;

    [Header("�������� ���������")]
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


    [Header("��������� ��������")]
    public List<GameObject> collisionObjects; // ������� ��� ��������� ������������
    public bool removeOnCollision = false;

    void Start()
    {
        //// ������������� ���� ����������
        //for (int i = 0; i < objects.Count; i++)
        //{
        //    AddCharacter(i, new Vector2(0, 0));
        //}
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
                person.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = 
                    playerPrefabForChangeSkin.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                
                //Temp
                person.transform.GetChild(0).GetComponent<SpriteRenderer>().color =
                    playerPrefabForChangeSkin.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
            }
        }
    }

    public void ChangeCharacterSkin()
    {
        if (playerPrefabForChangeSkin != null)
        {
            var persons = GetPersons();
            var person = persons[index];

            person.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite =
                    playerPrefabForChangeSkin.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;

            //Temp
            person.transform.GetChild(0).GetComponent<SpriteRenderer>().color =
                playerPrefabForChangeSkin.transform.GetChild(0).GetComponent<SpriteRenderer>().color;
        }
    }

    // ��������� ����� ���������
    public void ChangeCharacterSkin(GameObject character, Sprite newSkin)
    {
        CharacterParams characterParams = character.GetComponent<CharacterParams>();
        characterParams.SetSprite(newSkin);
    }

    // ��������� ����������� ��������
    public void ChangeMovementDirection(GameObject character)
    {
        CharacterParams characterParams = character.GetComponent<CharacterParams>();
        characterParams.ChangeMovementDirection();
    }

    public void ChangeMovementDirection(GameObject character, int direction)
    {
        CharacterParams characterParams = character.GetComponent<CharacterParams>();
        characterParams.SetMovementDirection(direction);
    }

    // ������� ������� �� ����� ����� LevelEditor
    public void SetCharacterPosition(GameObject character, int x, int y)
    {
        Vector2 position = LevelEditor.GetPositionFromGrid(x, y);
        CharacterParams characterParams = character.GetComponent<CharacterParams>();
        characterParams.transform.position = position;
    }

    private List<GameObject> GetPersons()
    {
        var result = new List<GameObject>();
        for (int i = 0; i < personsList.transform.childCount; i++)
        {
            result.Add(personsList.transform.GetChild(i).gameObject);
        }

        return result;
    }
}

 