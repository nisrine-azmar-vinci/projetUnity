using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI shellCounterText;

    public void UpdateShellCounter(int shellCount)
    {
        shellCounterText.text = shellCount.ToString();  // Met à jour le texte avec le nombre actuel
    }
}
