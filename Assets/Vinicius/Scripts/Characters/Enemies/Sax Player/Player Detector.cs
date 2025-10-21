using UnityEngine;

namespace Characters.Enemies.SaxPlayer
{
    public class PlayerDetector : MonoBehaviour
    {
        [Header("||===== Objects =====||")]
        [SerializeField] private StateController saxPlayerController;
        [SerializeField] private NoteSequence noteSequence;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (!saxPlayerController.isDead)
                    noteSequence.Activate();
            }
        }
    }
}