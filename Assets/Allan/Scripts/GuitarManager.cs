using System;
using UnityEngine;

public class GuitarManager : MonoBehaviour
{
    public static event Action<MusicalNote> OnNotePlayed;

    [System.Serializable]
    public class NoteKeyMapping
    {
        public MusicalNote note;
        public KeyCode key;
    }

    [Header("Aqui voce coloca as teclas das notas")]
    public NoteKeyMapping[] noteMappings;

    void Update()
    {
        foreach (var mapping in noteMappings)
        {
            if (Input.GetKeyDown(mapping.key))
            {
                Debug.Log("Nota tocada: " + mapping.note);
                OnNotePlayed?.Invoke(mapping.note);
            }
        }
    }
}
