using System;
using UnityEngine;

namespace Characters.Player
{
    public class GuitarController : MonoBehaviour
    {
        public static event Action<MusicalNote> OnNotePlayed;

        public void PlayNote(MusicalNote note)
        {
            OnNotePlayed?.Invoke(note);

            Debug.Log("Tocou " + note);
        }
    }
}