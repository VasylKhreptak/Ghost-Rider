using UnityEngine;

public static class Rigidbody2DExtensions
{
    public static void AddExplosionForce(this Rigidbody2D rb, float force, Vector2 explosionPosition,
        float radius, AnimationCurve forceCurve, float upwardsModifier = 0.0f, ForceMode2D mode = ForceMode2D.Force)
    {
        var explosionDir = rb.position - explosionPosition;
        var explosionDistance = explosionDir.magnitude;

        if (upwardsModifier != 0)
        {
            explosionDir.y += upwardsModifier;
            explosionDir.Normalize();
        }

        rb.AddForce(Mathf.Lerp(0, force,
            forceCurve.Evaluate(explosionDistance / radius)) * explosionDir, mode);
    }
}
