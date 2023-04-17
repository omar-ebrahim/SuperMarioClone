using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic.Components
{
    public abstract class MoveableBlock : MonoBehaviour
    {
        protected IEnumerator Move(Vector3 from, Vector3 to, float duration)
        {
            float elapsed = 0f;
            //float duration = 0.125f; // 1/8 of a second

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

        /// <summary>
        /// Moves the tranform in the given direction, over a set distance during a set period of time.
        /// </summary>
        /// <param name="direction">The direction to move.</param>
        /// <param name="distanceMultiplier">The distance to move in units.</param>
        /// <param name="duration">The total time in seconds to move.</param>
        protected IEnumerator MoveAction(Vector3 direction, float distanceMultiplier, float duration)
        {
            Vector3 restingPosition = transform.localPosition;
            Vector3 animatedPosition = restingPosition + direction * distanceMultiplier; // up and down 3 units
            yield return Move(restingPosition, animatedPosition, duration);
            yield return Move(animatedPosition, restingPosition, duration);
        }
    }
}
