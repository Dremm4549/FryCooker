using UnityEngine;

[CreateAssetMenu(fileName ="New Equipment", menuName ="Eqipment/Item")]
public class EquipmentData : ScriptableObject
{
    public string equipmentName;
    public Sprite icon;
    public float cooldown;
}
