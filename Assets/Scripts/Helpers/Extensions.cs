using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class Extensions
    {
        // The player should not be on the "Default" layer. If it is, then when you ray cast you collide with yourself
        private static LayerMask layerMask = LayerMask.GetMask(Constants.LAYER_DEFAULT);

        public static bool RayCast(this Rigidbody2D rigidbody, Vector2 direction)
        {
            if (rigidbody.isKinematic) // physics engine not controlling this object
            {
                return false;
            }

            float radius = 0.25f;
            float distance = 0.375f; // distance to ray cast, positive value = raycast DOWN

            RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction, distance, layerMask);

            // Check we've collided with something and we've not collided with ourselves.
            return hit.collider != null && hit.rigidbody != rigidbody;
        }
    }
}