using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class GrillManager : MonoBehaviour, IUtility
{
    [SerializeField] private Transform[] pattySockets;
    [SerializeField] private GameObject pattyObj;
    [SerializeField] int numberOfPattySockets;

    [SerializeField] private List<IngredientHandler> activeIngredients = new List<IngredientHandler>();
    private void Start()
    {
        if (pattySockets == null)
        {
            pattySockets = GetComponentsInChildren<Transform>();
        }
    }

    public void Interact()
    {
        Debug.Log("Interacting with grill");
    }

    public void InteractWithIngredient(GameObject ingredient)
    {
        if (numberOfPattySockets <= 9)
        {
            //GameObject newPatty = Instantiate(ingredient, pattySockets[numberOfPattySockets].position, Quaternion.identity);

            ingredient.transform.SetParent(pattySockets[numberOfPattySockets]);
            ingredient.transform.SetPositionAndRotation(pattySockets[numberOfPattySockets].position, Quaternion.identity);
            
            if(ingredient.GetComponent<IngredientHandler>() != null)
            {
                ingredient.GetComponent<IngredientHandler>().StartCooking();
                numberOfPattySockets++;
            }

            //IngredientHandler ingredientHandler = newPatty.GetComponent<IngredientHandler>();
            //newPatty.transform.SetParent(pattySockets[numberOfPattySockets]);
        }
    }

}
