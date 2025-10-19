using UnityEngine;
using System.Collections.Generic;
using Characters.Player;
using System;
using UnityEngine.Events;
using System.Collections;

namespace Characters.Enemies
{
    public class NoteSequence : MonoBehaviour
    {
        [Serializable]
        public class NoteAssetMapping
        {
            public MusicalNote note;
            public Sprite noteSprite;
        }

        [Header("||===== Objects =====||")]
        [SerializeField] private List<GameObject> notePoints;
        [SerializeField] private List<NoteAssetMapping> noteAssets;

        private List<NoteDisplay> noteDisplays = new();
        private List<MusicalNote> requiredSequence = new();

        [Header("||===== Parameters =====||")]
        [SerializeField] private int sequenceSize;
        [SerializeField] private float setupDelay;

        private int sequenceCounter = 0;

        [Header("||===== Booleans =====||")]
        public bool isActive;
        public bool finishedSetup;

        public UnityEvent OnSequenceCompleted;
        public static event Action<Transform> OnSequenceCompletedEffects;

        public void Activate()
        {
            if (isActive)
                return;

            isActive = true;

            GuitarController.OnNotePlayed += PlayNote;

            StartCoroutine(Setup());
        }

        public void Deactivate()
        {
            isActive = false;

            GuitarController.OnNotePlayed -= PlayNote;

            foreach (var display in noteDisplays)
                display.Deactivate();
        }

        private void PlayNote(MusicalNote playedNote)
        {
            if (!finishedSetup)
                return;

            if (sequenceCounter >= sequenceSize)
            {
                ResetSequence();
                return;
            }

            if (playedNote == requiredSequence[sequenceCounter])
            {
                noteDisplays[sequenceCounter].SetAsHit();

                sequenceCounter++;

                if (sequenceCounter >= sequenceSize)
                    CompleteSequence();
            }

            else
                ResetSequence();
        }

        private void CompleteSequence()
        {
            OnSequenceCompleted.Invoke();
            OnSequenceCompletedEffects?.Invoke(transform);

            sequenceCounter = 0;

            if (isActive)
            {
                finishedSetup = false;
                StartCoroutine(Setup());
            }
        }

        private void ResetSequence()
        {
            bool callEffects = sequenceCounter > 0;

            sequenceCounter = 0;

            foreach (var display in noteDisplays)
                display.SetAsUnhit(callEffects);
        }

        private IEnumerator Setup()
        {
            sequenceCounter = 0;

            requiredSequence.Clear();

            for (int i = 0; i < sequenceSize; i++)
            {
                MusicalNote randomNote = (MusicalNote)UnityEngine.Random.Range(0, 4);

                requiredSequence.Add(randomNote);
            }

            noteDisplays.Clear();

            for (int i = 0; i < sequenceSize; i++)
            {
                GameObject placeholder = notePoints[i];
                MusicalNote noteForThisSlot = requiredSequence[i];
                Sprite slotSprite = noteAssets.Find(a => a.note == noteForThisSlot).noteSprite;

                NoteDisplay display = placeholder.GetComponent<NoteDisplay>();

                if (display != null && slotSprite != null)
                {
                    display.Activate(slotSprite);

                    noteDisplays.Add(display);

                    yield return new WaitForSeconds(0.2f);
                }
            }

            finishedSetup = true;
        }
    }
}