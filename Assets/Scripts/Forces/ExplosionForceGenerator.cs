using UnityEngine;

/// <summary>
/// CASO: Explosión radial
/// Empuja todas las partículas dentro del radio hacia afuera desde el centro.
/// Puede ser de un solo pulso (Trigger) o continua.
///
/// FÓRMULA: F = strength * (1 - dist/radius) * dirección_saliente
/// </summary>
public class ExplosionForceGenerator : MonoBehaviour, IForceGenerator
{
    [Header("Parámetros")]
    [Tooltip("Fuerza máxima en el epicentro.")]
    [Min(0f)] public float strength = 50f;

    [Tooltip("Radio de efecto. Partículas fuera no se ven afectadas.")]
    [Min(0.1f)] public float radius = 5f;

    [Tooltip("True = empuja continuamente. False = un solo pulso por Trigger().")]
    public bool continuous = false;

    private bool exploded = false;

    private void OnEnable()  { ParticleWorld.Register((IForceGenerator)this); }
    private void OnDisable() { ParticleWorld.Unregister((IForceGenerator)this); }

    /// <summary>Lanza un pulso de explosión. Llámalo desde un botón UI o tecla.</summary>
    public void Trigger() { exploded = false; }

    public void ApplyForces(float dt)
    {
        if (!continuous)
        {
            if (exploded) return;
            exploded = true;
        }

        Vector3 center = transform.position;

        foreach (Particle p in ParticleWorld.All)
        {
            Vector3 toParticle = p.Position - center;
            float dist = toParticle.magnitude;

            if (dist <= Mathf.Epsilon || dist > radius) continue;

            // Más fuerza cerca del centro, cae a 0 en el borde del radio
            float falloff = 1f - (dist / radius);
            Vector3 force = toParticle.normalized * strength * falloff;

            p.AddForce(force);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0.4f, 0f, 0.12f);
        Gizmos.DrawSphere(transform.position, radius);
        Gizmos.color = new Color(1f, 0.4f, 0f, 0.9f);
        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.15f);

#if UNITY_EDITOR
        UnityEditor.Handles.Label(
            transform.position + Vector3.up * (radius + 0.3f),
            $"💥 Explosion  str={strength:F0}  r={radius:F1}"
        );
#endif
    }
}
