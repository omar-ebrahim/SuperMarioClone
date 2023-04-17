using UnityEngine;

namespace Assets.Scripts.Logic.Animations
{
    public class AnimatedSprite : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private float frameRate = 1f / 6f; // 6fps as default

        private SpriteRenderer spriteRenderer;
        private int frame;

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

        private void Animate()
        {
            frame++;

            if (frame >= sprites.Length) // Reached the end of the sprites array
            {
                frame = 0;
            }

            spriteRenderer.sprite = sprites[frame];
        }
    }
}
