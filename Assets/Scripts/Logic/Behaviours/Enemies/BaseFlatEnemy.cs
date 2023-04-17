using Assets.Scripts.Helpers;
using Assets.Scripts.Logic.Animations;
using Assets.Scripts.Logic.Movement;
using Assets.Scripts.Logic.Players;
using UnityEngine;

namespace Assets.Scripts.Logic.Behaviours.Enemies
{
    public abstract class BaseFlatEnemy :BaseEnemy
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


        /// <summary>
        /// Swap the sprite to a flattened Goomba.
        /// </summary>
        protected void Flatten()
        {
            // Disable the Goomba
            GetComponent<Collider2D>().enabled = false;
            GetComponent<EntityMovement>().enabled = false;
            GetComponent<AnimatedSprite>().enabled = false;

            // Swap the sprite
            GetComponent<SpriteRenderer>().sprite = flatSprite;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerBehaviour(collision);
        }

        protected abstract void EnemyCollisionFromAboveBehavior();
        protected abstract void TriggerBehaviour(Collider2D collision);
    }
}
