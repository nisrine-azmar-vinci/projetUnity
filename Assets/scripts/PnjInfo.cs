using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPnjInfo", menuName = "PNJ/Info")]
public class PnjInfo : ScriptableObject
{
    public List<string> initialDialogue;         // Dialogue initial
    public List<string> lackOfPlantsDialogue;    // Dialogue quand les plantes sont insuffisantes
    public List<string> rewardDialogue;          // Dialogue de récompense
    public List<string> lackOfShellsDialogue;    // (Optionnel) Dialogue quand les coquillages sont insuffisants
}
