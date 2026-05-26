using UnityEngine;
using UnityEngine.InputSystem;
 
public class SandboxController : MonoBehaviour
{
    [Header("Conexión")]
    public ExplosionForceGenerator explosion;
 
    private void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Debug.Log("Space presionado");
            if (explosion != null)
                explosion.Trigger();
            else
                Debug.Log("Explosion es null - asigna el componente en el Inspector");
        }
    }
}
 