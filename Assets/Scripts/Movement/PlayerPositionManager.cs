using System.Collections;
using UnityEngine;

public class PlayerPositionManager : MonoBehaviour
{
    public static PlayerPositionManager Instance;

    private bool hasSavedPosition = false;
    Vector3 savedPosition;
    Quaternion savedRotation;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // persiste entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveTransform(Transform transform)
    {
        hasSavedPosition = true;
        savedPosition = transform.position;
        savedRotation = transform.rotation;
    }

    public (Vector3 position, Quaternion rotation)? getTransformPosition()
    {
        if (hasSavedPosition)
            return (savedPosition, savedRotation);

        return null;
    }
}


