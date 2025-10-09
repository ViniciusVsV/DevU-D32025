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

    public static void InvokeNotePlayed(MusicalNote note)
    {
        OnNotePlayed?.Invoke(note);
    }
}
