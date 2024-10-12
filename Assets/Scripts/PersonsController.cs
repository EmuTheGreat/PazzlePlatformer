using System.Collections.Generic;
using UnityEngine;

public class PersonsController : MonoBehaviour
{
    [SerializeField]
    private GameObject personsList;
    [SerializeField]
    private GameObject playerPrefab;

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

            objects.Add(newCharacter);
        }
    }

    // ��������� ����� ���������
    public void ChangeCharacterSkin(GameObject character, Sprite newSkin)
    {
        CharacterParams characterParams = character.GetComponent<CharacterParams>();
        characterParams.SetSprite(newSkin);
    }

    // ��������� ����������� ��������
    public void SetCharacterMovementDirection(GameObject character, Vector2 direction)
    {
        
    }

    // ������� ������� �� ����� ����� LevelEditor
    public void SetCharacterPosition(GameObject character, int x, int y)
    {
        Vector2 position = LevelEditor.GetPositionFromGrid(x, y);
        CharacterParams characterParams = character.GetComponent<CharacterParams>();
        characterParams.transform.position = position;
    }
}

 