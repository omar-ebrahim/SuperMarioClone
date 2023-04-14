using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Fields

    private DeathAnimation deathAnimation;

    private PlayerSpriteRenderer finalActiveRenderer;

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

    #endregion

    #region Unity Methods

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    #endregion

    #region Public Methods

    public void Hit()
    {
        if (IsBig)
        {
            Shrink();
        }
        else
        {
            Death();
        }
    }

    #endregion

    #region Private Methods

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
        finalActiveRenderer = big ? bigMario : smallMario;

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

        finalActiveRenderer.enabled = true;
    }

    #endregion
}
