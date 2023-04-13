using Assets.Scripts.Helpers;
using UnityEngine;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField]
    protected Sprite flatSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG_PLAYER))
        {
            var player = collision.gameObject.GetComponent<Player>();

            // Player landed on Goomba's head
            if (collision.transform.DotTest(transform, Vector2.down))
            {
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

