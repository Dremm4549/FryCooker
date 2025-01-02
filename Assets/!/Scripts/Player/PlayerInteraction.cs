using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private IEquipment currentEquipment;

    [SerializeField] float interactionRange = 3.0f;

    [SerializeField] Transform ingredientHolder;
    [SerializeField] Transform equipmentHolder;
    public bool HasIngredient { get; private set; }
    public bool HasEquipment { get; private set; }

    [SerializeField] GameObject spatulaTool;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Interact button
        {
            InteractWithObject();
        }

        if (Input.GetKeyDown(KeyCode.Space)) // Use equipment button
        {
            UseEquipment();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (HasIngredient)
            {
                IngredientHandler ingredient = ingredientHolder.GetComponentInChildren<IngredientHandler>();
                if (ingredient != null)
                {
                    HasIngredient = false;
                    ingredient.Drop();
                }
                //DropIngredient();
            }
        }

        Debug.DrawRay(transform.position, transform.forward * interactionRange, Color.red);
    }

    private void InteractWithObject()
    {
        // Cast a ray forward from the player's position
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange))
        {
            // Try to get an interactable object
            var interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (interactable is IEquipment equipment)
                {
                    interactable.Interact();
                    Debug.Log("equip");
                    EquipTool(equipment);
                }
                else if(interactable is IIngredient ingredient)
                {
                    HasIngredient = true;
                    interactable.Interact();
                    Debug.Log("INGREDIENT");
                    //EquipIngredient(ingredient);
                }
                else if(interactable is IUtility utility)
                {
                    if(HasIngredient)
                    {
                        //ingredientHolder.GetChild(0).gameObject.SetActive(false);
                        Debug.Log("???");
                        HasIngredient = false;
                        utility.InteractWithIngredient(ingredientHolder.GetChild(0).gameObject);
                    }
                }
                else if( interactable is IIngredientSpanwer ingredientSpanwer)
                {
                    interactable.Interact();
                }
            }
        }
    }

    void EquipIngredient(IIngredient ingredient)
    {
        if (!HasIngredient)
        {
            HasIngredient = true;

            //// Access the ingredient's prefab and component before instantiating
            //GameObject ingredientPrefab = ingredient.GetIngredientPreab();
            //IngredientHandler ingredientHandler = ingredientPrefab.GetComponent<IngredientHandler>();

            //// Stop cooking before instantiating the prefab
            //if (ingredientHandler != null)
            //{
            //    Debug.Log("Destory the child gaameobject");
            //    ingredientHandler.StopCooking();
            //}

            //currentIngredient = ingredient;
            //Debug.Log(ingredientHandler.GetCondition());

            //// Instantiate the ingredient
            //GameObject ingredientObj = Instantiate(ingredientPrefab, ingredientHolder);

            //ingredientObj.transform.localPosition = Vector3.zero;
            //ingredientObj.transform.localRotation = Quaternion.identity;

            //IngredientHandler newIngredientHandler = ingredientObj.GetComponent<IngredientHandler>();
            //if (currentIngredient != null)
            //{
            //    IngredientHandler currentIngredientHandler = currentIngredient.GetIngredientPreab().GetComponent<IngredientHandler>();
            //}

            //// Enable IngredientHandler on the new instantiated object
            //ingredientObj.SetActive(true);
            //ingredientObj.GetComponent<IngredientHandler>().enabled = true;
        }
    }

    private void EquipTool(IEquipment equipment)
    {
        // Unequip the current equipment if there is any
        if (currentEquipment != null)
        {
            Destroy(currentEquipment.GetEquipmentObject());
        }

        if(!HasEquipment)
        {
            HasEquipment = !HasEquipment;
            
            currentEquipment = equipment;
            
            currentEquipment.Equip();

            GameObject equipmentObj = Instantiate(currentEquipment.GetEquipmentObject(), equipmentHolder);

            equipmentObj.transform.localPosition = Vector3.zero;
            equipmentObj.transform.localRotation = Quaternion.identity;
        }
    }

    public void InstantiateIngredient(GameObject ingredientPrefab)
    {
        // Instantiate the ingredient into the ingredient holder

        if(ingredientPrefab == null)
        {
            Debug.LogError("ingredient prefab is null");
            return;
        }

        ingredientPrefab.transform.SetParent(ingredientHolder);
        ingredientPrefab.transform.SetPositionAndRotation(ingredientHolder.transform.position, Quaternion.identity);
        
        HasIngredient = true;
    }

    void DropIngredient()
    {
        Destroy(ingredientHolder.GetChild(0).gameObject);
    }

    private void UseEquipment()
    {
        if (currentEquipment != null)
        {
            currentEquipment.Use();
        }
        else
        {
            Debug.Log("No equipment equipped to use!");
        }
    }

    void InteractWithUtility(IUtility utility)
    {
        if (HasIngredient)
        {
            foreach (Transform child in ingredientHolder)
            {
                Destroy(child.gameObject);
            }
        }
    } 
}
