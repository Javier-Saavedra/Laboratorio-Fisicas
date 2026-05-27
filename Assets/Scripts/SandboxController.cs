using UnityEngine;
using UnityEngine.InputSystem;
 
public class SandboxController : MonoBehaviour
{
    [Header("Generadores de Fuerza")]
    public ExplosionForceGenerator explosion;
    public WindForceGenerator viento;
    public MagnetismForceGenerator magnetismo;
    public VortexForceGenerator vortice;
    public RepulsionForceGenerator repulsion;
 
    private void Start()
    {
        // Todas las fuerzas arrancan desactivadas excepto la explosión
        if (viento != null)     viento.enabled     = false;
        if (magnetismo != null) magnetismo.enabled  = false;
        if (vortice != null)    vortice.enabled     = false;
        if (repulsion != null)  repulsion.enabled   = false;
    }
 
    private void Update()
    {
        // SPACE → Explosión (pulso único)
        if (Keyboard.current.spaceKey.wasPressedThisFrame && explosion != null)
            explosion.Trigger();
 
        // 1 → Toggle Viento
        if (Keyboard.current.digit1Key.wasPressedThisFrame && viento != null)
            ToggleFuerza(viento, "Viento");
 
        // 2 → Toggle Magnetismo
        if (Keyboard.current.digit2Key.wasPressedThisFrame && magnetismo != null)
            ToggleFuerza(magnetismo, "Magnetismo");
 
        // 3 → Toggle Vórtice
        if (Keyboard.current.digit3Key.wasPressedThisFrame && vortice != null)
            ToggleFuerza(vortice, "Vórtice");
 
        // 4 → Toggle Repulsión
        if (Keyboard.current.digit4Key.wasPressedThisFrame && repulsion != null)
            ToggleFuerza(repulsion, "Repulsión");
    }
 
    private void ToggleFuerza(MonoBehaviour componente, string nombre)
    {
        componente.enabled = !componente.enabled;
        string estado = componente.enabled ? "ACTIVADO ✅" : "DESACTIVADO ❌";
        Debug.Log($"{nombre}: {estado}");
    }
}
 