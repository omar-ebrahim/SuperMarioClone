using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Logic.Behaviours.Enemies
{
    public class Goomba : BaseFlatEnemy
    {
        protected override void EnemyCollisionFromAboveBehavior()
        {
            Flatten();
            Destroy(gameObject, 1f);
        }

        protected override void TriggerBehaviour(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer(Constants.LAYER_SHELL))
            {
                Hit();
            }
        }
    }
}