using UnityEngine;

namespace Characters.Enemies.SaxPlayer
{
    public class FlipDetector : MonoBehaviour
    {
        [SerializeField] private StateController saxPlayerController;
        [SerializeField] private Transform spriteTransform;

        [SerializeField] private Transform checkPoint;
        [SerializeField] private float rayDistance;
        [SerializeField] private LayerMask terrainLayers;

        private bool hasGroundAhead;
        private bool hasWallAhead;

        private void Update()
        {
            hasGroundAhead = Physics2D.Raycast(checkPoint.position, Vector2.down, rayDistance, terrainLayers);
            hasWallAhead = Physics2D.Raycast(transform.position, Vector2.right * saxPlayerController.moveDirection, rayDistance, terrainLayers);

            Debug.DrawRay(checkPoint.position, Vector2.down * rayDistance, Color.yellow);
            Debug.DrawRay(transform.position, rayDistance * saxPlayerController.moveDirection * Vector2.right, Color.red);

            if (!hasGroundAhead || hasWallAhead)
            {
                spriteTransform.localScale = new Vector3(
                    spriteTransform.localScale.x * -1,
                    spriteTransform.localScale.y,
                    spriteTransform.localScale.z
                );

                saxPlayerController.isFacingRight = !saxPlayerController.isFacingRight;

                saxPlayerController.moveDirection *= -1;
            }
        }
    }
}