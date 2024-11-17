using UnityEngine;

[CreateAssetMenu(fileName = "NewPower", menuName = "Powers/Power")]
public class PowerScriptableObject : ScriptableObject
{
    public string powerName;
    public float flightBoostMultiplier = 1.5f;
}
