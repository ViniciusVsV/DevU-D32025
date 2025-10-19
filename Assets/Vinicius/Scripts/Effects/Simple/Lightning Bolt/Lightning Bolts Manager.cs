using System.Collections.Generic;
using UnityEngine;

namespace Effects.Simple.LightningBolt
{
    public class LightningBoltsManager : MonoBehaviour
    {
        [SerializeField] private LightningBolt lightningBoltPrefab;
        [SerializeField] private int poolSize = 10;

        private Queue<LightningBolt> pool = new();

        private void Awake()
        {
            for (int i = 0; i < poolSize; i++)
            {
                LightningBolt newBolt = Instantiate(lightningBoltPrefab, transform);

                newBolt.Initialize(this);

                newBolt.gameObject.SetActive(false);

                pool.Enqueue(newBolt);
            }
        }

        public void SummonBolt(Vector2 startPoint, Vector2 endPoint)
        {
            // Se a pool está vazia, criar um novo objeto para poder suportar a demanda
            if (pool.Count == 0)
            {
                LightningBolt newBolt = Instantiate(lightningBoltPrefab, transform);

                newBolt.Initialize(this);

                newBolt.ApplyEffect(startPoint, endPoint);

                return;
            }

            var bolt = pool.Dequeue();

            bolt.gameObject.SetActive(true);

            bolt.ApplyEffect(startPoint, endPoint);
        }

        public void ReturnBolt(LightningBolt bolt)
        {
            bolt.gameObject.SetActive(false);

            pool.Enqueue(bolt);
        }
    }
}