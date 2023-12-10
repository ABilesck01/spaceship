using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthHelper : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private bool canHeal = false;

    public void OnHealInput(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canHeal) playerHealth.Heal();
    }

    public void OnPlayerReady(Transform t)
    {
        playerHealth = t.GetComponent<PlayerHealth>();
        canHeal = true;
    }
}
