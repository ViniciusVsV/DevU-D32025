using UnityEngine;

namespace Characters.Enemies
{
    public class NoteDisplay : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void Initialize(Sprite sprite)
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }
            spriteRenderer.sprite = sprite;
            SetUnhit();
        }

        public void SetAsHit()
        {
            this.spriteRenderer.color = Color.gray;
        }

        public void SetUnhit()
        {
            this.spriteRenderer.color = Color.white;
        }
    }
}