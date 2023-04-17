using UnityEngine;

namespace Assets.Scripts.Logic.Animations
{
    public class SideScrolling : MonoBehaviour
    {
        #region Constants

        private const string TAG_PLAYER = "Player";

        #endregion

        #region Private Fields

        private Transform player;

        #endregion

        #region Serialised Fields

        [SerializeField]
        private float overgroundHeight = 7;

        [SerializeField]
        private float undergroundHeight = -7;

        #endregion

        #region Unity Methods

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

        #endregion

        #region Private Methods

        public void SetCameraYPosition(bool underground)
        {
            Vector3 cameraPosition = transform.position;
            cameraPosition.y = underground ? undergroundHeight : overgroundHeight;
            transform.position = cameraPosition;
        }

        #endregion
    }
}