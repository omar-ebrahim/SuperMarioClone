using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [SerializeField]
    private KeyCode[] enterPipeKeycodes = new KeyCode[] { KeyCode.DownArrow, KeyCode.S };

    [SerializeField]
    private Transform connection;

    [SerializeField]
    private Vector3 enterDirection = Vector3.down;

    [SerializeField]
    private Vector3 exitDirection = Vector3.zero; // Not animating exit if .zero

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (connection != null && collision.CompareTag(Constants.TAG_PLAYER))
        {
            if (enterPipeKeycodes.Any(keyCode => Input.GetKey(keyCode)))
            {
                StartCoroutine(EnterPipe(collision.transform));
            }
        }
    }

    private IEnumerator EnterPipe(Transform player)
    {
        var playerMovement = player.GetComponent<Movement>();

        playerMovement.enabled = false;

        // transform = the pipe
        Vector3 enteredPosition = transform.position + enterDirection;
        Vector3 enteredScale = Vector3.one / 2f; // halve his size

        // This just moves the player into the pipe only
        yield return Move(player, enteredPosition, enteredScale);
        yield return new WaitForSeconds(1f);

        var isUnderground = connection.position.y < 0f;
        Camera.main.GetComponent<SideScrolling>().SetUnderGround(isUnderground);

        if (exitDirection != Vector3.zero)
        {
            // Exit another pipe
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
            player.GetComponent<Movement>().enabled = true;
        }

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
        player.GetComponent<PlayerMovement>().enabled = true;
    }
}
