using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    [SerializeField]
    private Sprite idle, jump, slide;

    [SerializeField]
    private AnimatedSprite run;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // The player movement script reference exists on the parent object
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        run.enabled = playerMovement.Running;

        if (playerMovement.Jumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (playerMovement.Sliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if (!playerMovement.Running)
        {
            spriteRenderer.sprite = idle;
            run.enabled = false;
        }
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }
}
