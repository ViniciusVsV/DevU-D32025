using System;
using UnityEngine;

public class GuitarManager : MonoBehaviour
{
    public static event Action<MusicalNote> OnNotePlayed;

    public void PlayNote(MusicalNote note)
    {
        OnNotePlayed?.Invoke(note);
        Debug.Log("Tocou " + note);
    }
}
