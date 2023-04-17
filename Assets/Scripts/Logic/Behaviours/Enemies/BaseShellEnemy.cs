using Assets.Scripts.Helpers;
using Assets.Scripts.Logic.Animations;
using Assets.Scripts.Logic.Movement;
using Assets.Scripts.Logic.Players;
using UnityEngine;

namespace Assets.Scripts.Logic.Behaviours.Enemies
{
    public abstract class BaseShellEnemy : BaseEnemy
    {
        private bool shelled;
        private bool pushed;

        [SerializeField]
        protected Sprite shellSprite;


        private void OnTriggerEnter2D(Collider2D collision)
        {
            TriggerBehaviour(collision);
        }

        /// <summary>
        /// Called when the player collides with the trigger in either the shelled/unshelled state.
        /// </summary>
        /// <param name="collision">The player or other object that's collided with them.</param>
        protected virtual void TriggerBehaviour(Collider2D collision)
        {
            if (collision.gameObject.CompareTag(Constants.TAG_PLAYER))
            {
                var player = collision.gameObject.GetComponent<Player>();
                if (player.IsStarPower)
                {
                    Hit();
                }
                else if (shelled)
                {
                    if (pushed)
                    {
                        if (player.IsStarPower)
                        {
                            Hit();
                        }
                        else
                        {
                            player.Hit();
                        }
                    }
                    else
                    {
                        Vector2 direction = new Vector2(transform.position.x - collision.transform.position.x, 0f);
                        PushShell(direction);
                    }
                }
            }
            else if (!shelled && collision.gameObject.layer == LayerMask.NameToLayer(Constants.LAYER_SHELL))
            {
                Hit();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionBehaviour(collision);
        }

        /// <summary>
        /// Called when the player collides with the shell enemy when NOT in the shell state.
        /// </summary>
        /// <param name="collision">The object that has collided with the shell enemy.</param>
        protected virtual void CollisionBehaviour(Collision2D collision)
        {
            if (collision.gameObject.CompareTag(Constants.TAG_PLAYER))
            {
                var player = collision.gameObject.GetComponent<Player>();

                if (player.IsStarPower)
                {
                    Hit();
                }
                else if (!shelled)
                {
                    // Player landed on Goomba's head
                    if (collision.transform.DotTest(transform, Vector2.down))
                    {
                        EnterShell();
                    }
                    else
                    {
                        player.Hit();
                    }
                }

            }
        }

        private void PushShell(Vector2 direction)
        {
            pushed = true;
            GetComponent<Rigidbody2D>().isKinematic = false;
            var entityMovement = GetComponent<EntityMovement>();
            entityMovement.SetDirection(direction.normalized);
            entityMovement.SetSpeed(12f);
            entityMovement.enabled = true;
            gameObject.layer = LayerMask.NameToLayer(Constants.LAYER_SHELL);
        }

        protected void EnterShell()
        {
            GetComponent<EntityMovement>().enabled = false;
            GetComponent<AnimatedSprite>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = shellSprite;
            shelled = true;
        }
    }
}
