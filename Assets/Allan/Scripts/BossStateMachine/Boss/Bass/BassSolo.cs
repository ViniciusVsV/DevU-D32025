using UnityEngine;
using System.Collections;

public class BassSolo : MonoBehaviour
{
    /*[Header("-----Moving Points-----")]
    [SerializeField] public Transform backgroundPosition;
    [SerializeField] public Transform exitPoint;
    [SerializeField] public Transform entryPoint;
    [SerializeField] public Transform attackPosition;
    

    [Header("-----Speed-----")]
    [SerializeField] public float moveSpeed;
    */
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

#region codigo antigo
    /*public void StartBassSolo()
    {
        StartCoroutine(MoveToAttackPosition());
    }

    public void StartReturnToBackground()
    {
        StartCoroutine(MoveToBackgroundPosition());
    }

    private IEnumerator StartBassSoloing()
    {
        
    }*/

    /*private IEnumerator MoveToAttackPosition()
    {
        isMoving = true;

        // aqui ele sai da tela
        while (Vector3.Distance(transform.position, exitPoint.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, exitPoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = exitPoint.position;

        // aqui ele vai pra posicao na mesma layer? do player e ainda vai ter que ir pra sposicao de ataque
        yield return new WaitForSeconds(0.5f);
        transform.position = entryPoint.position;

        // agora ele vai andar até a tela para solar em cima do player
        while (Vector3.Distance(transform.position, attackPosition.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, attackPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = attackPosition.position;

        isMoving = false;
    }
    private IEnumerator MoveToBackgroundPosition()
    {
        isMoving = true;

        // ele volta praa fora da tela
        while (Vector3.Distance(transform.position, entryPoint.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, entryPoint.position, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = entryPoint.position;

        // ele da tp pro ponto de saida
        yield return new WaitForSeconds(0.5f);
        transform.position = exitPoint.position;

        // ele volta a tocar a musica com a banda
        while (Vector3.Distance(transform.position, backgroundPosition.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, backgroundPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = backgroundPosition.position;

        isMoving = false;
    } */
#endregion
}