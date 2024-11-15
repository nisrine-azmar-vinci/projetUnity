using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI shellCounterText;
    public TextMeshProUGUI plantCounterText;

    public void UpdateShellCounter(int shellCount)
    {
        shellCounterText.text = "Shells: " + shellCount.ToString();
    }

    public void UpdatePlantCounter(int plantCount)
    {
        plantCounterText.text = "Flowers: " + plantCount.ToString();
    }
}
