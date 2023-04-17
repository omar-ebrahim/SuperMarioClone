using Assets.Scripts.Helpers;
using Assets.Scripts.Logic.Animations;
using Assets.Scripts.Logic.Movement;
using UnityEngine;

namespace Assets.Scripts.Logic.Behaviours.Enemy
{
    public class Goomba : BaseEnemy
    {

        protected override void EnemyCollisionFromAboveBehavior()
        {
            Flatten();
            // Remove from game after 1 second
            Destroy(gameObject, 1f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.LAYER_SHELL))
            {
                Hit();
            }
        }

        /// <summary>
        /// Swap the sprite to a flattened Goomba.
        /// </summary>
        private void Flatten()
        {
            // Disable the Goomba
            GetComponent<Collider2D>().enabled = false;
            GetComponent<EntityMovement>().enabled = false;
            GetComponent<AnimatedSprite>().enabled = false;

            // Swap the sprite
            GetComponent<SpriteRenderer>().sprite = flatSprite;
        }

        private void Hit()
        {
            GetComponent<DeathAnimation>().enabled = true;
            GetComponent<AnimatedSprite>().enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}