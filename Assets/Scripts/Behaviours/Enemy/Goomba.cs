using Assets.Scripts.Helpers;
using UnityEngine;

public class Goomba : BaseEnemy
{
    [SerializeField]
    private Sprite flatGoomba;

    protected override void EnemyCollisionFromAboveBehavior()
    {
        Flatten();
        // Remove from game after 1 second
        Destroy(gameObject, 1f);
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
        GetComponent<SpriteRenderer>().sprite = flatGoomba;
    }
}
