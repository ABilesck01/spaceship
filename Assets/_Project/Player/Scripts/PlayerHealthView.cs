using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private Slider shieldBar;
    [SerializeField] private TextMeshProUGUI txtMedkit;
    [SerializeField] private AudioSource explosionAudio;

    public void OnSelectShip(Transform a)
    {
        a.GetComponent<PlayerHealth>().SetView(healthBar, shieldBar, txtMedkit, explosionAudio);
    }
}
