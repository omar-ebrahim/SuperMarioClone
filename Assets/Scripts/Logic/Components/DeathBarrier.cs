using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Logic.Components
{
    public class DeathBarrier : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Constants.TAG_PLAYER))
            {
                collision.gameObject.SetActive(false);
                GameManager.Instance.ResetLevel(3f);
            }
            else
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
