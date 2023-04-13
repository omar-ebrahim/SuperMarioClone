using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Helpers;
using UnityEngine;

public class Koopa : MonoBehaviour
{
    private bool shelled;
    private bool pushed;

    [SerializeField]
    protected Sprite shellSprite;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (shelled && collision.gameObject.CompareTag(Constants.TAG_PLAYER))
        {
            if (pushed)
            {
                var player = collision.gameObject.GetComponent<Player>();
                player.Hit();
            }
            else
            {
                Vector2 direction = new Vector2(transform.position.x - collision.transform.position.x, 0f);
                PushShell(direction);
            }
        }
        else if (!shelled && collision.gameObject.layer == LayerMask.NameToLayer(Constants.LAYER_SHELL))
        {
            Hit();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!shelled && collision.gameObject.CompareTag(Constants.TAG_PLAYER))
        {
            var player = collision.gameObject.GetComponent<Player>();

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

    // TODO: Move to base class
    private void Hit()
    {
        GetComponent<DeathAnimation>().enabled = true;
        GetComponent<AnimatedSprite>().enabled = false;
        Destroy(gameObject, 3f);
    }
}
