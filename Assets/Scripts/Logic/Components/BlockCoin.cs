using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic.Components
{
    public class BlockCoin : MonoBehaviour
    {
        // Use this for initialization
        void Start()
        {
            GameManager.Instance.AddCoins();
            StartCoroutine(Animate());
        }

        private IEnumerator Animate()
        {
            Vector3 restingPosition = transform.localPosition;
            Vector3 animatedPosition = restingPosition + Vector3.up * 3f; // up and down 3 units
            yield return Move(restingPosition, animatedPosition);
            yield return Move(animatedPosition, restingPosition);

            // Remove the coin from the stage as it's not required any more
            Destroy(gameObject);
        }

        private IEnumerator Move(Vector3 from, Vector3 to)
        {
            float elapsed = 0f;
            float duration = 0.3f; // ~1/3 of a second

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                transform.localPosition = Vector3.Lerp(from, to, t);
                elapsed += Time.deltaTime;

                yield return null;
            }

            // End position after the elapsed time might not perfectly match,
            // so set it to its end position at the end anyway
            transform.localPosition = to;
        }
    }
}
