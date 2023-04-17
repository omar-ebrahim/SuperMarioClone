using Assets.Scripts.Logic.Animations;
using UnityEngine;

namespace Assets.Scripts.Logic.Behaviours.Enemies
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        protected void Hit()
        {
            GetComponent<DeathAnimation>().enabled = true;
            GetComponent<AnimatedSprite>().enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}