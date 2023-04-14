using UnityEngine;

using System.Collections; 

public class DeathAnimation : MonoBehaviour
{
    [SerializeField]
    private Sprite deadSprite;

    [SerializeField]
    private SpriteRenderer SpriteRenderer;

    #region Unity Methods

    private void Reset()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        UpdateSprite();
        DisablePhysics();
        StartCoroutine(nameof(Animate));
    }

    #endregion

    #region Private Methods

    private void UpdateSprite()
    {
        SpriteRenderer.enabled = true;
        // When player dies, they fall away in front of everything else
        SpriteRenderer.sortingOrder = 10;

        if (deadSprite != null)
        {
            SpriteRenderer.sprite = deadSprite;
        }
    }

    private void DisablePhysics()
    {
        foreach (var collider in GetComponents<Collider2D>())
        {
            collider.enabled = false;
        }

        // Stop applying physics to the rigidbody
        GetComponent<Rigidbody2D>().isKinematic = true;
        GetComponent<Movement>().enabled = false;
    }

    private IEnumerator Animate()
    {
        float elapsed = 0f;
        float duration = 4f;

        float jumpVelocity = 10f;
        float gravity = -36f;

        Vector3 velocity = Vector3.up * jumpVelocity;

        while (elapsed < duration)
        {
            if (elapsed > 0.5f) // wait 1/2 second before animating
            {
                transform.position += velocity * Time.deltaTime; // d = vt
                velocity.y += gravity * Time.deltaTime;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }
    }

    #endregion
}
