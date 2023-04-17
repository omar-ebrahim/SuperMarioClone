using Assets.Scripts.Helpers;
using Assets.Scripts.Logic.Players;
using UnityEngine;

namespace Assets.Scripts.Logic.Behaviours.Enemy
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        [SerializeField]
        protected Sprite flatSprite;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Constants.TAG_PLAYER))
            {
                var player = collision.gameObject.GetComponent<Player>();

                if (player.IsStarPower)
                {
                    EnemyCollisionFromAboveBehavior();
                }
                else if (collision.transform.DotTest(transform, Vector2.down))
                {
                    // Player landed on Goomba's head
                    EnemyCollisionFromAboveBehavior();
                }
                else
                {
                    player.Hit();
                }
            }
        }

        protected abstract void EnemyCollisionFromAboveBehavior();
    }
}