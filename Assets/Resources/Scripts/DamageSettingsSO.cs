using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageSettings", menuName = "Settings/DamageSettings")]
public class DamageSettingsSO : ScriptableObject
{
    public int MeleeDamage;
    public int BrushRangeDamage;
    public int PickUpHealth;
}
