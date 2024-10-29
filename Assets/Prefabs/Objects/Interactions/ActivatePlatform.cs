using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlatform : MonoBehaviour, IInteractable
{
    [SerializeField]
    private GameObject platform; // Платформа, которую активируем
    [SerializeField]
    private bool makeVisible;

    public void Awake()
    {
        platform.SetActive(makeVisible);
    }

    public void Interact(GameObject character)
    {
        if (platform != null)
        {
            makeVisible = !makeVisible;
            //Debug.Log("Платформа активирована");
            platform.SetActive(makeVisible);
        }
    }
}
