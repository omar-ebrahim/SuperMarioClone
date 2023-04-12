using UnityEngine;

namespace Assets.Scripts.Helpers
{
    public static class Extensions
    {
        // The player should not be on the "Default" layer. If it is, then when you ray cast you collide with yourself
        private static LayerMask layerMask = LayerMask.GetMask(Constants.LAYER_DEFAULT);

        /// <summary>
        /// Checks if we have collided with another object
        /// </summary>
        /// <param name="rigidbody">The object to check (me).</param>
        /// <param name="direction">The direction to check a collision for.</param>
        public static bool RayCast(this Rigidbody2D rigidbody, Vector2 direction)
        {
            if (rigidbody.isKinematic) // physics engine not controlling this object
            {
                return false;
            }

            float radius = 0.25f;
            float distance = 0.375f; // distance to ray cast, positive value = raycast DOWN

            RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, layerMask);

            // Check we've collided with something and we've not collided with ourselves.
            return hit.collider != null && hit.rigidbody != rigidbody;
        }

        /// <summary>
        /// Gets the dot product of two vectors to see where we have collided.
        /// </summary>
        /// <param name="transform">The current object</param>
        /// <param name="other">The object we're colliding into</param>
        /// <param name="testDirection">The direction of travel</param>
        public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection)
        {
            // vector = direction between vector we're colliding into minus me
            Vector2 direction = other.position - transform.position;

            // 1 == the same, 0 = perpendicular, -1 = opposite
            return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
        }
    }
}
