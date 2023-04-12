using Assets.Scripts.Helpers;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: Extract this out to a base class, as other enemies can also reuse this
        if (collision.gameObject.CompareTag(Constants.TAG_PLAYER))
        {
            // Player landed on Goomba's head
            if (collision.transform.DotTest(transform, Vector2.down))
            {
                EnemyCollisionFromAboveBehavior();
            }
        }
    }

    protected abstract void EnemyCollisionFromAboveBehavior();
}

