using UnityEngine;

public class SessionMemory : MonoBehaviour
{
    public static SessionMemory Instance;

    public bool mainMenuEffectsHappened;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this);
    }
}