using UnityEngine;

public class PlayerSpriteRenderer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private PlayerMovement playerMovement;

    [SerializeField]
    private Sprite idle, jump, run, slide;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        // The player movement script reference exists on the parent object
        playerMovement = GetComponentInParent<PlayerMovement>();
    }

    private void LateUpdate()
    {
        if (playerMovement.Jumping)
        {
            spriteRenderer.sprite = jump;
        }
        else if (playerMovement.Sliding)
        {
            spriteRenderer.sprite = slide;
        }
        else if (playerMovement.Running)
        {
            spriteRenderer.sprite = run;
        }
        else
        {
            spriteRenderer.sprite = idle;
        }
    }
}
