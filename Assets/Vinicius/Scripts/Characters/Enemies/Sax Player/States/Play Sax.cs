using System.Collections;
using DG.Tweening;
using StateMachine;
using UnityEngine;

namespace Characters.Enemies.SaxPlayer.States
{
    public class PlaySax : BaseState
    {
        private StateController saxPlayerController => (StateController)controller;

        [Header("||===== Objects =====||")]
        [SerializeField] private GameObject markObject;
        [SerializeField] private CircleCollider2D damageTrigger;
        private Transform playerTransform;
        private Transform markTransform;
        private SpriteRenderer markSprite;

        [Header("||===== Parameters =====||")]
        [SerializeField] private float explosionDuration;
        [SerializeField] private float sizeMultiplier;
        [SerializeField] private float sizeIncreaseDuration;

        private int beatCounter => saxPlayerController.beatCounter;

        private Coroutine coroutine;

        private void Start()
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
            markTransform = markObject.transform;
            markSprite = markObject.GetComponent<SpriteRenderer>();
        }

        public override void StateEnter()
        {
            spriteRenderer.color = Color.magenta;

            // Posiciona a marca em cima do jogador
            markTransform.position = playerTransform.position;

            // Ativa o sprite renderer da marca
            markSprite.enabled = true;
        }

        public override void StateUpdate()
        {
            // A cada batida, aumenta o tamanho da marca
            // Na batida warmupBeats, ativa a explosão
            if (saxPlayerController.beatHappened)
            {
                if (beatCounter != 0)
                    markObject.transform.DOScale(markTransform.localScale * sizeMultiplier, sizeIncreaseDuration).SetEase(Ease.OutElastic);

                // Transição para Idle
                else
                {
                    coroutine = StartCoroutine(ExplosionRoutine());

                    saxPlayerController.SetIdle();
                }
            }
        }

        public override void StateExit()
        {
            // Foi interrompido antes de iniciar a corrotina de explosão
            if (coroutine == null)
            {
                markSprite.enabled = false;
                markTransform.localScale = Vector3.one;
            }
        }

        private IEnumerator ExplosionRoutine()
        {
            // Ativa o trigger de dano
            damageTrigger.enabled = true;

            // Temporario: muda a cor do sprite
            markSprite.color = Color.red;

            // Chama os efeitos

            yield return new WaitForSeconds(explosionDuration);

            // Desativa o trigger de dano
            damageTrigger.enabled = false;

            // Desativa o gameObject da marca
            markSprite.enabled = false;

            markSprite.color = Color.white;

            // Volta a marca ao tamnho normal
            markTransform.localScale = Vector3.one;

            coroutine = null;
        }
    }
}