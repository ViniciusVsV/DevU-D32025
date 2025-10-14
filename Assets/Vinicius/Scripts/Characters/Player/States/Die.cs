using System.Collections;
using StateMachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Characters.Player.States
{
    public class Die : BaseState
    {
        private StateController playerController => (StateController)controller;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float respawnDelay;

        public override void StateEnter()
        {
            Debug.Log("Entrou no estado de morte!");

            //Desativa tudo
            playerController.enabled = false;

            rb.simulated = false;
            spriteRenderer.enabled = false;

            StartCoroutine(Routine());
        }

        private IEnumerator Routine()
        {
            yield return new WaitForSeconds(respawnDelay);

            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}