using UnityEngine;

public class Spatual : MonoBehaviour, IEquipment
{
    public EquipmentData data;

    public void Interact()
    {
        Debug.Log($"Picked up {data.equipmentName}");
    }

    public void Equip()
    {
        Debug.Log($"Equipped {data.equipmentName}");
        Destroy(gameObject);
    }

    public void Unequip()
    {
        Debug.Log($"Unequipped {data.equipmentName}");
    }

    public void Use()
    {
        Debug.Log($"Using {data.equipmentName} to flip patties!");
    }

    public GameObject GetEquipmentObject()
    {
        return gameObject;
    }
}
