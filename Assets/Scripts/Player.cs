using UnityEngine;

public class Player : MonoBehaviour
{
    #region Private Fields

    private DeathAnimation deathAnimation;

    #endregion

    #region Serialised fields

    [SerializeField]
    private PlayerSpriteRenderer smallMario;

    [SerializeField]
    private PlayerSpriteRenderer bigMario;

    #endregion

    #region Public Properties

    public bool Big => bigMario.enabled;
    public bool Small => smallMario.enabled;
    public bool Dead => deathAnimation.enabled;

    #endregion

    #region Unity Methods

    private void Awake()
    {
        deathAnimation = GetComponent<DeathAnimation>();
    }

    #endregion

    #region Public Methods

    public void Hit()
    {
        if (Big)
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
        // Logic to come later on.
    }

    //private void Grow() { }

    private void Death()
    {
        smallMario.enabled = false;
        bigMario.enabled = false;
        deathAnimation.enabled = true;
        GameManager.Instance.ResetLevel(3f);
    }

    #endregion
}
