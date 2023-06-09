using Assets.Scripts.Logic.Animations;
using Assets.Scripts.Logic.Movement;
using UnityEngine;

namespace Assets.Scripts.Logic.Players
{
    public class PlayerSpriteRenderer : MonoBehaviour
    {
        #region Serialised Fields

        [SerializeField] private Sprite idle, jump, slide;
        [SerializeField] private AnimatedSprite run;

        #endregion

        #region Private Fields

        private PlayerMovement playerMovement;

        #endregion

        #region Public Properties

        public SpriteRenderer SpriteRenderer { get; private set; }

        #endregion

        #region Unity Methods

        private void Awake()
        {
            SpriteRenderer = GetComponent<SpriteRenderer>();
            // The player movement script reference exists on the parent object
            playerMovement = GetComponentInParent<PlayerMovement>();
        }

        private void LateUpdate()
        {
            run.enabled = playerMovement.Running;

            if (playerMovement.Jumping)
            {
                SpriteRenderer.sprite = jump;
            }
            else if (playerMovement.Sliding)
            {
                SpriteRenderer.sprite = slide;
            }
            else if (!playerMovement.Running)
            {
                SpriteRenderer.sprite = idle;
            }
        }

        private void OnEnable()
        {
            SpriteRenderer.enabled = true;
        }

        private void OnDisable()
        {
            SpriteRenderer.enabled = false;
            run.enabled = false;
        }

        #endregion
    }
}
