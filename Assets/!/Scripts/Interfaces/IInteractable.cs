using UnityEngine;

public interface IInteractable
{
    void Interact();
}

public interface IEquipment : IInteractable
{
    void Use();
    void Equip();
    void Unequip();
    GameObject GetEquipmentObject();
}

public interface IIngredient : IInteractable
{
    void Use();
    void Cook();
    void Drop();
}

public interface IUtility : IInteractable
{
    void InteractWithIngredient(GameObject ingredient);
}

public interface IIngredientSpanwer : IInteractable
{

}




