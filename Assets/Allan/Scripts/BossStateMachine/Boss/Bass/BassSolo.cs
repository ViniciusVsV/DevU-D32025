using UnityEngine;
using System.Collections;

public class BassSolo : MonoBehaviour
{
    [Header("-----Solo Settings-----")]
    [SerializeField] public float soloDuration;

    [Header("-----Note Spawner-----")]
    [SerializeField] public NoteSpawner noteSpawner;

    public bool isSoloing;

    public void StartBassSolo()
    {
        StartCoroutine(PlayingBassSolo());
    }

    private IEnumerator PlayingBassSolo()
    {
        Debug.Log("baixo");
        isSoloing = true;
        noteSpawner.enabled = true;

        yield return new WaitForSeconds(soloDuration);

        noteSpawner.enabled = false;

        isSoloing = false;
    }
}