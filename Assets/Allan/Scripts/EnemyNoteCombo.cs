using UnityEngine;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

public class EnemyNoteCombo : MonoBehaviour
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

    [Header("Local dos pontos de vida")] [SerializeField] private List<GameObject> notePoints;
    [Header("Nota e seu sprite")] public List<NoteAssetMapping> noteAssets;
    private List<NoteDisplay> activeNoteDisplays = new List<NoteDisplay>();
    private List<MusicalNote> requiredSequence = new List<MusicalNote>();
    
    void Awake()
    {
        GenerateSequence();
        SetupVisuals();
    }

    void OnEnable()
    {
        GuitarManager.OnNotePlayed += OnPlayedNote;
    }

    void OnDisable()
    {
        GuitarManager.OnNotePlayed -= OnPlayedNote;
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
            Debug.Log("Nota certa.");
            activeNoteDisplays[currentStep].SetAsHit();
            currentStep++;
            if (currentStep >= sequenceSize)
            {
                EnemyDefeat();
            }
        }
        else
            ResetSequence();
    }
    private void GenerateSequence()
    {
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
            {
                return asset.noteSprite;
            }
        }
        Debug.LogError("Não foi encontrado um sprite para a nota: " + note);
        return null;
    }

    private void ResetSequence()
    {
        currentStep = 0;
        Debug.Log("Errou, sequencia resetada.");

        foreach (var display in activeNoteDisplays)
        {
            display.SetUnhit();
        }
    }
    private void EnemyDefeat()
    {
        Debug.Log("inimigo: tomei gap");
        gameObject.SetActive(false);
    }
}
