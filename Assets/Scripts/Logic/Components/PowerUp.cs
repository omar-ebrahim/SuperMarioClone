using Assets.Scripts.Helpers;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private PowerUpType powerUpType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAG_PLAYER))
        {
            Collect(collision.gameObject);
        }
    }

    private void Collect(GameObject player)
    {
        switch (powerUpType)
        {
            case PowerUpType.Coin:
                GameManager.Instance.AddCoins();
                break;
            case PowerUpType.ExtraLife:
                GameManager.Instance.IncrementLife();
                break;
            case PowerUpType.MagicMushroom:
                var playerInstance = player.GetComponent<Player>();
                if (playerInstance.IsBig)
                {
                    GameManager.Instance.IncrementLife();
                }
                else
                {
                    playerInstance.Grow();
                }
                break;
            case PowerUpType.StarPower:
                break;
            default:
                break;
        }

        Destroy(gameObject);
    }
}
