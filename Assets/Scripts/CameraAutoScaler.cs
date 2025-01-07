using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraAutoScaler : MonoBehaviour
{
    public float paddingX = 1f; // �������������� ������
    public float paddingY = 1f; // ������������ ������

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        AdjustCamera();
    }

    public void AdjustCamera()
    {
        var levelEditor = FindObjectOfType<LevelEditor>();
        if (levelEditor == null)
        {
            Debug.LogError("LevelEditor �� ��������!");
            return;
        }

        float levelWidth = levelEditor.gridWidth * (levelEditor.tileSize.x + levelEditor.tileSpacing.x) - levelEditor.tileSpacing.x;
        float levelHeight = levelEditor.gridHeight * (levelEditor.tileSize.y + levelEditor.tileSpacing.y) - levelEditor.tileSpacing.y;

        levelWidth += paddingX * 2;
        levelHeight += paddingY * 2;

        // ������������� ������
        transform.position = new Vector3(0, 0, -10);

        // ������ ��������
        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = levelWidth / levelHeight;

        if (screenRatio >= targetRatio)
        {
            cam.orthographicSize = levelHeight / 2;
        }
        else
        {
            cam.orthographicSize = levelWidth / (2 * screenRatio);
        }
    }

    void OnValidate()
    {
        if (cam == null) cam = GetComponent<Camera>();
        AdjustCamera();
    }
}
