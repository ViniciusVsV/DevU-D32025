using System.Linq;
using UnityEngine;

namespace Characters.Player
{
    public class StatsHandler : MonoBehaviour
    {
        [SerializeField] private StateController playerController;
        [SerializeField] private string[] dangerousTags;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (playerController.isDashing)
            {
                Debug.Log("Desviou!");
                return;
            }

            if (dangerousTags.Contains(other.tag))
            {
                Debug.Log("Morreu!");
                playerController.SetDie();
            }
        }
    }
}