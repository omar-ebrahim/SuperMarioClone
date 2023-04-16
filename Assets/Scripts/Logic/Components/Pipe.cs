using System.Collections;
using System.Linq;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    #region Serialised Fields

    [SerializeField]
    private KeyCode[] enterPipeKeycodes = new KeyCode[] { KeyCode.DownArrow, KeyCode.S };

    [SerializeField]
    private Transform connection;

    [SerializeField]
    private Vector3 enterDirection = Vector3.down;

    [SerializeField]
    private Vector3 exitDirection = Vector3.zero; // Not animating exit if .zero

    #endregion

    #region Unity Methods

    private void OnTriggerStay2D(Collider2D other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if (enterPipeKeycodes.Any(x => Input.GetKey(x)))
            {
                StartCoroutine(Enter(other.transform));
            }
        }
    }

    #endregion

    #region Pivate Methods

    private IEnumerator Enter(Transform player)
    {
        var playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.enabled = false;

        Vector3 enteredPosition = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one * 0.5f;

        yield return Move(player, enteredPosition, enteredScale);
        yield return new WaitForSeconds(1f);

        var sideScrolling = Camera.main.GetComponent<SideScrolling>();
        var isUnderground = connection.position.y < 0f;

        sideScrolling.SetCameraYPosition(isUnderground);

        if (exitDirection != Vector3.zero)
        {
            player.position = connection.position - exitDirection;
            var finalPosition = connection.position + exitDirection;
            yield return Move(player, finalPosition, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }

        playerMovement.enabled = true;
    }

    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale, t);
            elapsed += Time.deltaTime;

            yield return null;
        }

        player.position = endPosition;
        player.localScale = endScale;
    }

    #endregion
}
