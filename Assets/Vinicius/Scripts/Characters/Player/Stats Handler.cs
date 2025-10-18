using System.Linq;
using UnityEngine;

namespace Characters.Player
{
    public class StatsHandler : MonoBehaviour
    {
        [SerializeField] private StateController playerController;
        [SerializeField] private string[] undodgeableTags;
        [SerializeField] private string[] dodgeableTags;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (undodgeableTags.Contains(other.tag))
                playerController.SetDie();

            else if (dodgeableTags.Contains(other.tag))
            {
                if (playerController.isDashing)
                {
                    Debug.Log("Desviou!");
                    return;
                }

                playerController.SetDie();
            }
        }
    }
}