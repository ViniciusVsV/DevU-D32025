using DG.Tweening;
using Effects.Simple;
using UnityEngine;

namespace Effects.Complex.Enemies.VynilDisc
{
    public class AggroEffects : MonoBehaviour
    {
        public static AggroEffects Instance;

        [Header("Objects")]


        [Header("Parameters")]
        [SerializeField] private float duration;
        [SerializeField] private float riseDistance;
        [SerializeField] private float sizeIncrease;
        [SerializeField] private Ease ease;

        private Vector2 initialPos;

        [Header("Control Booleans")]
        public bool finishedPlaying;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {

        }

        public void ApplyEffects(Transform exclamationPoint)
        {
            //Chama efeito sonoro

            //Ativa o objeto de exclamação
            initialPos = exclamationPoint.localPosition;

            exclamationPoint.gameObject.SetActive(true);

            var sequence = DOTween.Sequence();

            sequence.Join(exclamationPoint.DOScale(Vector3.one * sizeIncrease, duration).SetEase(ease));
            sequence.Join(exclamationPoint.DOLocalMove(initialPos + Vector2.one * riseDistance, duration).SetEase(ease));

            sequence.OnComplete(() =>
            {
                exclamationPoint.gameObject.SetActive(false);

                exclamationPoint.localScale = Vector3.one;
                exclamationPoint.localPosition = initialPos;
            });
        }
    }
}