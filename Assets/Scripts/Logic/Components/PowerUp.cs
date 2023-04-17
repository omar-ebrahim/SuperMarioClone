using Assets.Scripts.Enums;
using Assets.Scripts.Helpers;
using Assets.Scripts.Logic.Players;
using UnityEngine;

namespace Assets.Scripts.Logic.Components
{
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

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.CompareTag(Constants.TAG_PLAYER))
            {
                Collect(collision.gameObject);
            }
        }

        private void Collect(GameObject player)
        {
            var playerInstance = player.GetComponent<Player>();
            switch (powerUpType)
            {
                case PowerUpType.Coin:
                    GameManager.Instance.AddCoins();
                    break;
                case PowerUpType.ExtraLife:
                    GameManager.Instance.IncrementLife();
                    break;
                case PowerUpType.MagicMushroom:
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
                    playerInstance.StarPower();
                    break;
                default:
                    break;
            }

            Destroy(gameObject);
        }
    }
}
