using System.Collections.Generic;
using UnityEngine;

public class NoteSpawner : MonoBehaviour
{
    public enum PosicaoDoSpawner { Cima, Baixo, Esquerda, Direita }
    [SerializeField] private List<GameObject> listaDePrefabs;

    private BoxCollider2D areaDeSpawn;

    private Bounds bounds;
    private Vector2 posicaoSpawn;
    private Vector2 direcaoMovimento;

    private float posY;
    private float posX;

    [SerializeField] private PosicaoDoSpawner posSpawner;

    [SerializeField] private float intervaloInicial;
    [SerializeField] private float intervaloMinimo;
    private float cronometroParaSpawn;

    void Awake()
    {
        areaDeSpawn = GetComponent<BoxCollider2D>();

    }

    private void Start()
    {
        cronometroParaSpawn = intervaloInicial;
    }

    void Update()
    {
        cronometroParaSpawn -= Time.deltaTime;

        if (cronometroParaSpawn <= Mathf.Epsilon)
        {
            Spawn();

            float tempoDecorrido = Time.timeSinceLevelLoad;

            float intervaloVariavel = (intervaloInicial - intervaloMinimo) / (1f + Mathf.Log(tempoDecorrido + 1f));
            float intervaloAtual = intervaloMinimo + intervaloVariavel;

            cronometroParaSpawn = intervaloAtual;
        }
    }

    void Spawn()
    {
        int indiceAleatorio = Random.Range(0, listaDePrefabs.Count);

        GameObject prefabSorteado = listaDePrefabs[indiceAleatorio];

        bounds = areaDeSpawn.bounds;

        direcaoMovimento = Vector2.zero;

        if(posSpawner == PosicaoDoSpawner.Cima || posSpawner == PosicaoDoSpawner.Baixo)
        {
            posX = Random.Range(bounds.min.x, bounds.max.x);
            posicaoSpawn = new Vector2(posX, transform.position.y);
        }
        else
        {
            posY = Random.Range(bounds.min.y, bounds.max.y);
            posicaoSpawn = new Vector2(transform.position.x, posY);
        }

         switch (posSpawner)
            {
            case PosicaoDoSpawner.Cima:
                direcaoMovimento = Vector2.down;
                break;
            case PosicaoDoSpawner.Baixo:
                direcaoMovimento = Vector2.up;
                break;
            case PosicaoDoSpawner.Direita:
                direcaoMovimento = Vector2.left;
                break;
            case PosicaoDoSpawner.Esquerda:
                direcaoMovimento = Vector2.right;
                break;

            }

        float inclination = Random.Range(0, 360);
        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, inclination));


        GameObject newNote = Instantiate(prefabSorteado, posicaoSpawn, rotation);

        NoteMovement scriptDoInimigo = newNote.GetComponent<NoteMovement>();

        if (scriptDoInimigo != null)
        {
            scriptDoInimigo.direcaoMovimento = direcaoMovimento;
        }
    }
}
