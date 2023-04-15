using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Fields

    private DeathAnimation deathAnimation;

    private PlayerSpriteRenderer activeRenderer;

    private CapsuleCollider2D capsuleCollider;

    #endregion

    #region Serialised fields

    [SerializeField]
    private PlayerSpriteRenderer smallMario;

    [SerializeField]
    private PlayerSpriteRenderer bigMario;

    #endregion

    #region Public Properties

    public bool IsBig => bigMario.enabled;
    public bool IsSmall => smallMario.enabled;
    public bool IsDead => deathAnimation.enabled;
    public bool IsStarPower { get; private set; }

    #endregion

    #region Unity Methods

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        activeRenderer = smallMario;
    }

    #endregion

    #region Public Methods

    public void Hit()
    {
        if (IsStarPower || IsDead) return;

        if (IsBig)
        {
            Shrink();
        }
        else
        {
            Death();
        }
    }

    public void StarPower(float duration = 10)
    {
        IsStarPower = true;

        // Play animation
        StartCoroutine(StarPowerAnimation(duration));

        IsStarPower = false;
    }

    #endregion

    #region Private Methods

    private IEnumerator StarPowerAnimation(float duration = 10)
    {
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0)
            {
                // change the sprite colour.
                // The actual star power uses multiple sprites per renderer with different colours.
                // Here, we'll just randomise the sprite colour
                activeRenderer.SpriteRenderer.color = Random.ColorHSV(0f, 1f, 0f, 1f, 1f, 1f);
            }

            yield return null;
        }

        // Set the sprite back
        activeRenderer.SpriteRenderer.color = Color.white;
    }

    private void Shrink()
    {
        SetBig(false);
    }

    public void Grow()
    {
        SetBig(true);
    }

    private void Death()
    {
        smallMario.enabled = false;
        bigMario.enabled = false;
        deathAnimation.enabled = true;
        GameManager.Instance.ResetLevel(3f);
    }

    private void SetBig(bool big)
    {
        smallMario.enabled = !big;
        bigMario.enabled = big;

        // What to finally show in the animation
        activeRenderer = big ? bigMario : smallMario;

        if (big)
        {
            capsuleCollider.size = new Vector2(1f, 2f);
            capsuleCollider.offset = new Vector2(0f, 0.5f);
        }
        else
        {
            capsuleCollider.size = new Vector2(1f, 1f);
            capsuleCollider.offset = new Vector2(0f, 0f);
        }

        StartCoroutine(ScaleAnimation());
    }

    private IEnumerator ScaleAnimation()
    {
        float elapsed = 0f;
        float duration = 0.5f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            if (Time.frameCount % 4 == 0) // Every 4 frames, swap renderer
            {
                smallMario.enabled = !smallMario.enabled;
                bigMario.enabled = !smallMario.enabled;
            }

            yield return null;
        }

        smallMario.enabled = false;
        bigMario.enabled = false;

        activeRenderer.enabled = true;
    }

    #endregion
}
