using Assets.Scripts.Helpers;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic.Components
{
    public class BlockHit : MoveableBlock
    {
        #region Serialised Fields

        [SerializeField] private int maxHits = -1; // -1 = infinite
        [SerializeField] private Sprite emptyBlock; // what to change to when maximum hits reaches 0
        [SerializeField] private GameObject rewardItem;

        #endregion

        #region Private Fields

        private SpriteRenderer spriteRenderer;
        private bool isAnimated;
        private bool isAnimatable;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            isAnimatable = true;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (isAnimatable && !isAnimated && collision.gameObject.CompareTag(Constants.TAG_PLAYER))
            {
                // if collision.transform = Mario, and transform is this block
                // if Mario collides with this block going up and the dot product
                // of both vectors is the same, then this is okay
                if (collision.transform.DotTest(transform, Vector2.up))
                {
                    Hit();
                }
            }
        }

        #endregion

        #region Private Methods

        private void Hit()
        {
            spriteRenderer.enabled = true; // To account for hidden blocks
            if (maxHits != -1) maxHits--; // Don't decrement forever

            if (maxHits == 0)
            {
                spriteRenderer.sprite = emptyBlock;
                isAnimatable = false;
            }

            if (rewardItem != null)
            {
                // Spawn the reward item at the current location of the block
                Instantiate(rewardItem, transform.position, Quaternion.identity);
            }

            StartCoroutine(Animate(0.125f, 0.5f));
        }

        private IEnumerator Animate(float duration, float distanceMultiplier)
        {
            isAnimated = true;
            yield return MoveAction(Vector3.up, distanceMultiplier, duration);
            isAnimated = false;
        }

        #endregion
    }
}
