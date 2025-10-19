using UnityEngine;
using System.Collections.Generic;
using Characters.Player;
using System;
using UnityEngine.Events;
using Characters.Player.States;

namespace Characters.Enemies
{
    public class NoteSequence : MonoBehaviour
    {
        [Header("Número de notas na sequência")]
        [SerializeField] public int sequenceSize;
        private int currentStep = 0;

        [System.Serializable]
        public class NoteAssetMapping
        {
            public MusicalNote note;
            public Sprite noteSprite;
        }

        [Header("Local dos pontos de vida")][SerializeField] private List<GameObject> notePoints;
        [Header("Nota e seu sprite")] public List<NoteAssetMapping> noteAssets;
        private List<NoteDisplay> activeNoteDisplays = new List<NoteDisplay>();
        private List<MusicalNote> requiredSequence = new List<MusicalNote>();

        public UnityEvent OnSequenceCompleted;
        public static event Action<Transform> OnSequenceCompletedEffects;

        public bool isActive;

        public void Activate()
        {
            if (isActive)
                return;

            isActive = true;

            GuitarController.OnNotePlayed += OnPlayedNote;

            GenerateSequence();
            SetupVisuals();
        }

        public void Deactivate()
        {
            isActive = false;

            GuitarController.OnNotePlayed -= OnPlayedNote;

            ClearVisuals();
        }

        private void ClearVisuals()
        {
            foreach (var display in activeNoteDisplays)
            {
                if (display != null)
                    display.SetUnhit();
            }

            foreach (var point in notePoints)
            {
                var renderer = point.GetComponentInChildren<SpriteRenderer>();
                if (renderer != null)
                    renderer.sprite = null;
            }
        }

        private void OnDisable()
        {
            GuitarController.OnNotePlayed -= OnPlayedNote;
        }

        private void OnPlayedNote(MusicalNote playedNote)
        {
            if (currentStep >= sequenceSize)
            {
                ResetSequence();
                return;
            }

            if (playedNote == requiredSequence[currentStep])
            {
                activeNoteDisplays[currentStep].SetAsHit();

                currentStep++;

                if (currentStep >= sequenceSize)
                    CompleteSequence();
            }

            else
                ResetSequence();
        }

        private void GenerateSequence()
        {
            currentStep = 0;

            requiredSequence.Clear();

            for (int i = 0; i < sequenceSize; i++)
            {
                MusicalNote randomNote = (MusicalNote)UnityEngine.Random.Range(0, 4);

                requiredSequence.Add(randomNote);
            }
        }

        private void SetupVisuals()
        {
            activeNoteDisplays.Clear();

            for (int i = 0; i < sequenceSize; i++)
            {
                GameObject placeholder = notePoints[i];
                MusicalNote noteForThisSlot = requiredSequence[i];
                Sprite slotSprite = GetStaticSpriteForNote(noteForThisSlot);

                NoteDisplay display = placeholder.GetComponent<NoteDisplay>();

                if (display != null && slotSprite != null)
                {
                    display.Initialize(slotSprite);
                    activeNoteDisplays.Add(display);
                }
            }
        }

        private Sprite GetStaticSpriteForNote(MusicalNote note)
        {
            foreach (var asset in noteAssets)
            {
                if (asset.note == note)
                    return asset.noteSprite;
            }

            return null;
        }

        private void ResetSequence()
        {
            currentStep = 0;

            foreach (var display in activeNoteDisplays)
                display.SetUnhit();
        }

        private void CompleteSequence()
        {
            OnSequenceCompleted.Invoke();
            OnSequenceCompletedEffects?.Invoke(transform);

            currentStep = 0;

            if (isActive)
            {
                GenerateSequence();
                SetupVisuals();
            }
        }
    }
}