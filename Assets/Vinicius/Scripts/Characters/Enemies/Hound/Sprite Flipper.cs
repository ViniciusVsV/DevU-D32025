using UnityEngine;

namespace Characters.Enemies.Hound
{
    public class SpriteFlipper : MonoBehaviour
    {
        private Vector2 lastPosition;

        private void Awake()
        {
            lastPosition = transform.position;
        }

        void Update()
        {
            Vector2 moveDirection = ((Vector2)transform.position - lastPosition).normalized;
            Vector3 scale = transform.localScale;
            scale.x = Mathf.Abs(scale.x) * Mathf.Sign(moveDirection.x);
            transform.localScale = scale;

            lastPosition = transform.position;
        }
    }
}