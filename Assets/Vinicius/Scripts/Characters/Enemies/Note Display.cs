using System;
using Effects.Simple;
using JetBrains.Annotations;
using UnityEngine;

namespace Characters.Enemies
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class NoteDisplay : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;
        private NoteMovement noteMovement;

        private Vector2 initialPosition;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            initialPosition = transform.localPosition;
        }

        private void Start()
        {
            noteMovement = NoteMovement.Instance;
        }

        public void Activate(Sprite sprite)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();

            spriteRenderer.sprite = sprite;
            spriteRenderer.color = Color.white;

            noteMovement.ApplyActivatedEffect(initialPosition, transform);
        }

        public void Deactivate()
        {
            noteMovement.ApplyDeactivatedEffect(initialPosition, transform);
        }

        public void SetAsHit()
        {
            noteMovement.ApplyHitEffect(initialPosition, transform, spriteRenderer);
        }

        public void SetAsUnhit(bool callEffects)
        {
            if (callEffects)
                noteMovement.ApplyUnhitEffect(initialPosition, transform, spriteRenderer);
        }
    }
}