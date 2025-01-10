using UnityEngine.SceneManagement;
using UnityEngine;

[ExecuteInEditMode]
public class KeyLevelControl : MonoBehaviour
{
    public int totalKeys;
    public int nextSceneIndex;
    public float levelTransitionDelay = 1.0f;

    private int keysRemaining;

    void Start()
    {
        keysRemaining = totalKeys;
    }

    public void KeyCollected()
    {
        keysRemaining--;
        if (keysRemaining <= 0)
        {
            Invoke("LoadNextLevel", levelTransitionDelay);
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}