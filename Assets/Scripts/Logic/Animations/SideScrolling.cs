using UnityEngine;

public class SideScrolling : MonoBehaviour
{
    private const string TAG_PLAYER = "Player";

    private Transform player;

    // Initialisation
    private void Awake()
    {
        // We only have one player, so okay to find by this tag.
        player = GameObject.FindWithTag(TAG_PLAYER).transform;
    }

    // Called just before the scene is rendered
    // We want this function to track Mario's position AFTER the player has moved
    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;

        // Get the max of either the camera's Xpos, or the player's Xpos. Camera can only pan forwards
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x);

        transform.position = cameraPosition; // transform is the Transform attached to THIS game object
    }
}
