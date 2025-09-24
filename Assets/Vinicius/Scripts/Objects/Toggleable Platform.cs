using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class ToggleablePlatform : MonoBehaviour, IRythmSyncable
{
    private Collider2D col;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Color newColor;
    public bool isEnabled;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (!isEnabled)
        {
            col.enabled = false;
            spriteRenderer.color = newColor;
        }
    }

    public void RespondToBeat()
    {
        if (isEnabled)
        {
            col.enabled = false;

            spriteRenderer.color = newColor;

            isEnabled = false;
        }

        else
        {
            col.enabled = true;
            spriteRenderer.color = Color.white;

            isEnabled = true;
        }
    }
}