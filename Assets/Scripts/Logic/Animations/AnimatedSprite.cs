using UnityEngine;

namespace Assets.Scripts.Logic.Animations
{
    public class AnimatedSprite : MonoBehaviour
    {
        #region Serialised Fields

        [SerializeField] private Sprite[] sprites;
        [SerializeField] private float frameRate = 1f / 6f; // 60fps as default

        #endregion

        #region Private fields

        private SpriteRenderer spriteRenderer;
        private int frame;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnEnable()
        {
            InvokeRepeating(nameof(this.Animate), frameRate, frameRate);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }

        #endregion

        #region Private Methods

        private void Animate()
        {
            frame++;

            if (frame >= sprites.Length) // Reached the end of the sprites array
            {
                frame = 0;
            }

            spriteRenderer.sprite = sprites[frame];
        }

        #endregion
    }
}
