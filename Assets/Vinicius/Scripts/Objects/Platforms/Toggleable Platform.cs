using UnityEngine;

namespace Objects.Platforms
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class ToggleablePlatform : MonoBehaviour, IRythmSyncable
    {
        private Collider2D col;
        private SpriteRenderer spriteRenderer;

        [Header("||===== Objects =====||")]
        [SerializeField] private BoxCollider2D killTrigger;

        [Header("||===== Parameters =====||")]
        [SerializeField] private Color newColor;
        public bool isEnabled;

        private void Awake()
        {
            col = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            TogglePlatform();
        }

        private void TogglePlatform()
        {
            if (isEnabled)
            {
                killTrigger.enabled = true;
                col.enabled = true;

                spriteRenderer.color = Color.white;
            }

            else
            {
                killTrigger.enabled = false;
                col.enabled = false;
                
                spriteRenderer.color = newColor;
            }
        }

        public void RespondToBeat()
        {
            isEnabled = !isEnabled;
            TogglePlatform();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (col == null) col = GetComponent<Collider2D>();
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

            TogglePlatform();
        }
#endif
    }
}