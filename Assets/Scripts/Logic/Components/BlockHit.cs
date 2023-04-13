using System.Collections;
using Assets.Scripts.Helpers;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    [SerializeField]
    private int maxHits = -1; // -1 = infinite

    [SerializeField]
    private Sprite emptyBlock; // what to change to when maximum hits reaches 0

    private SpriteRenderer spriteRenderer;
    private bool isAnimated;
    private bool isAnimatable;

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

    private void Hit()
    {
        if (maxHits != -1)
        {
            // Don't decrement forever
            maxHits--;
        }

        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
            isAnimatable = false;
        }

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        isAnimated = true;

        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f; // doesn't need to move much
        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        isAnimated = false;
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f; // 1/8 of a second

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.localPosition = Vector3.Lerp(from, to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        // End position after the elapsed time might not perfectly match,
        // so set it to its end position at the end anyway
        transform.localPosition = to;
    }
}
