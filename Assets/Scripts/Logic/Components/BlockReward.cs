using UnityEngine;
using System.Collections;

public class BlockReward : MonoBehaviour
{
	// Use this for initialization
	void Start()
	{
		StartCoroutine(Animate());
	}

	private IEnumerator Animate()
	{
		Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
		CircleCollider2D circleCollider = GetComponent<CircleCollider2D>();
		BoxCollider2D triggerCollider = GetComponent<BoxCollider2D>();
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        rigidbody.isKinematic = true;
		if (circleCollider != null) circleCollider.enabled = false; // coins do not have one
		triggerCollider.enabled = false;
		spriteRenderer.enabled = false;

		// Wait until the block has finished its animation
		yield return new WaitForSeconds(0.25f);

		spriteRenderer.enabled = true;

		float elapsed = 0f;
		float duration = 0.5f;

		Vector3 startPosition = transform.localPosition;
		Vector3 endPosition = transform.localPosition + Vector3.up; // .up = 1 unit exactly

		while (elapsed < duration)
		{
			float time = elapsed / duration;

			transform.localPosition = Vector3.Lerp(startPosition, endPosition, time);

			elapsed += Time.deltaTime;

			yield return null;
		}

        // End position after the elapsed time might not perfectly match,
        // so set it to its end position at the end anyway
        transform.localPosition = endPosition;

        rigidbody.isKinematic = false;
        if (circleCollider != null) circleCollider.enabled = true;
		triggerCollider.enabled = true;
    }
}

