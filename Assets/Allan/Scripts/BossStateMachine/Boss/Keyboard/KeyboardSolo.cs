using UnityEngine;
using System.Collections;

public class KeyboardSolo : MonoBehaviour
{
    [Header("-----Solo Settings-----")]
    [SerializeField] public float soloDuration;

    [Header("-----Note Spawner-----")]
    [SerializeField] public NoteSpawner noteSpawner;
    [Header("-----FX-----")]
    [SerializeField] public ParticleSystem onFX;

    public bool isSoloing;

    public void StartKeyboardSolo()
    {
        StartCoroutine(PlayingKeyboardSolo());
    }

    private IEnumerator PlayingKeyboardSolo()
    {
        Debug.Log("teclado");
        isSoloing = true;
        noteSpawner.enabled = true;
        onFX.Play();

        yield return new WaitForSeconds(soloDuration);

        noteSpawner.enabled = false;

        isSoloing = false;
    }
}