using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic.Components
{
    public class BlockCoin : MoveableBlock
    {
        void Start()
        {
            GameManager.Instance.AddCoins();
            StartCoroutine(Animate(0.3f, 3f));
        }

        private IEnumerator Animate(float duration, float distanceMultiplier)
        {
            yield return MoveAction(Vector3.up, distanceMultiplier, duration);

            // Remove the coin from the stage as it's not required any more
            Destroy(gameObject);
        }
    }
}
