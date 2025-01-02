using UnityEngine;

public enum IngredientCondition
{
    Raw,
    Rare,
    Medium,
    WellDone,
    Overdone
}
public enum IngredientType
{
    Grillable,
    Fryable,
    Cuttable
}

public class IngredientHandler : MonoBehaviour, IIngredient
{
    public float CookProgress { get; private set; } = 0f;
    public bool IsCooking { get; private set; } = false;
    [SerializeField] float cookProg;

    [SerializeField] PlayerInteraction playerInteraction;

    [SerializeField] IngredientCondition CurrentCondition = IngredientCondition.Raw;
    [SerializeField] IngredientType IngredientType;
    [SerializeField] float cookTime;

    [SerializeField] Material[] material_states;
    [SerializeField] private Renderer ObjRenderer;
    [SerializeField] private Rigidbody rb;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        playerInteraction = FindFirstObjectByType<PlayerInteraction>();
    }

    public void Interact()
    {
        Debug.Log($"The user has interacted {this.transform.name}. We want to destroy this current game object and tell the interaction system to instantiate a copy of this burgers state");

        rb.useGravity = false;
        rb.isKinematic = true;
        if(IsCooking)
        {
            IsCooking = false;
        }

        playerInteraction.InstantiateIngredient(this.gameObject);

    }

    public void Drop()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            transform.parent = null;
        }
    }

    public void StartCooking()
    {
        if (!IsCooking)
        {
            IsCooking = true;
        }
    }

    public void Update()
    {
        if (IsCooking)
        {
            UpdateCookingCondition(Time.deltaTime);
        }
    }

    public void StopCooking()
    {
        if (IsCooking)
        {
            IsCooking = false;
        }
    }

    public void UpdateCookingCondition(float deltaTime)
    {
        if (!IsCooking) return;

        CookProgress += deltaTime;

        float percentage = CookProgress / cookTime;

        if (percentage < 0.1f)
        {
            CurrentCondition = IngredientCondition.Raw;
            ObjRenderer.material = material_states[0];


        }
        else if (percentage < 0.25f)
        {
            CurrentCondition = IngredientCondition.Rare;
            ObjRenderer.material = material_states[1];
        }
        else if (percentage < 0.5f)
        {
            CurrentCondition = IngredientCondition.Medium;
            ObjRenderer.material = material_states[2];

        }
        else if (percentage < 0.75f)
        {
            CurrentCondition = IngredientCondition.WellDone;
            ObjRenderer.material = material_states[3];

        }
        else if (percentage < 1f)
        {
            CurrentCondition = IngredientCondition.Overdone;
            ObjRenderer.material = material_states[4];
            StopCooking();
        }
        else
        {
            Debug.Log("stop");
            StopCooking();
        }
        cookProg = CookProgress;
    }


    public IngredientCondition GetCondition()
    {
        return CurrentCondition;
    }

    public GameObject GetIngredientPreab()
    {
        return gameObject;
    }

    public void Use()
    {
        throw new System.NotImplementedException();
    }

    public void Cook()
    {
        throw new System.NotImplementedException();
    }
}
