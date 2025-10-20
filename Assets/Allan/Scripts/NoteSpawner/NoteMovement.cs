using UnityEngine;

public class NoteMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb;
    [HideInInspector] public Vector2 direcaoMovimento;
    private SpriteRenderer sr;
    [SerializeField] public bool canRotate;
    [SerializeField] private float rotationSpeed;
    private int random;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        random = Random.Range(-1, 1);
        if (random < 0) random = -1;
        else random = 1;


    }
    void Start()
    {
        if (direcaoMovimento.x < 0)
        {
            sr.flipX = true;
        }

        Destroy(gameObject, 7);
    }

    void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime * random);

        transform.Translate(direcaoMovimento * moveSpeed * Time.deltaTime, Space.World);
    }
}
