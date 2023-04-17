using Assets.Scripts.Helpers;
using Assets.Scripts.Logic.Movement;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Logic.Components
{
    public class FlagPole : MonoBehaviour
    {
        #region Serialised Fields

        [SerializeField] private Transform flag;
        [SerializeField] private Transform poleBottom;
        [SerializeField] private Transform castle;
        [SerializeField] private float speed = 6f;

        #endregion

        #region Unity Methods

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag(Constants.TAG_PLAYER))
            {
                // Move the flag and player down
                StartCoroutine(MoveTo(flag, poleBottom.position));
                StartCoroutine(LevelCompleteSequence(collision.transform));
            }
        }

        #endregion

        #region Private Methods

        private IEnumerator LevelCompleteSequence(Transform player)
        {
            player.GetComponent<BaseMovement>().enabled = false;
            yield return MoveTo(player, poleBottom.position);
            yield return MoveTo(player, player.position + Vector3.right);
            yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
            yield return MoveTo(player, castle.position);

            player.gameObject.SetActive(false); // Player disappears when entering castle

            yield return new WaitForSeconds(2f);

            GameManager.Instance.NextLevel();
        }

        private IEnumerator MoveTo(Transform subject, Vector3 destination)
        {
            while (Vector3.Distance(subject.position, destination) > 0.125f) // over 1/8 of a unit, they're still too far away
            {
                subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
                yield return null;
            }

            subject.position = destination;
        }

        #endregion
    }
}
