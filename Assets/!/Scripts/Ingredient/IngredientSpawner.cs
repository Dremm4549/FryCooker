using UnityEngine;

public class IngredientSpawner : MonoBehaviour, IIngredientSpanwer
{
    [SerializeField] GameObject ingredientPrefab;

    [SerializeField] PlayerInteraction playerInteraction;

    private void Start()
    {
        playerInteraction = FindFirstObjectByType<PlayerInteraction>();
    }
    public void Interact()
    {
        if (playerInteraction != null && !playerInteraction.HasIngredient)
        {
            GameObject ingredientObject = Instantiate(ingredientPrefab);
            playerInteraction.InstantiateIngredient(ingredientObject);
            Debug.Log("We want to instantiate a fresh ingredient into the players hand");
        }
        else
        {
            Debug.Log("Already has ingredient");
        }
        
    }
}
