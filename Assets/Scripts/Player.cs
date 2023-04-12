using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    [SerializeField]float movementSpeed = 5;
    [SerializeField] float rotationSpeed = 7;
    [SerializeField] float interactionDistance = 4f;
    [SerializeField] LayerMask CLEAR_COUNTER_LAYER_MASK;
    [SerializeField] GameInput gameInput;
    [SerializeField] GameObject kitchenObjectTargetPosition;

    private bool isWalking;
    private float playerHeight = 2.0f;
    private float playerRadius = 0.7f;


    private BaseCounter selectedBaseCounter;
    private KitchenObject kitchenObject;


    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {        
        selectedBaseCounter?.Interact(this);                
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void CheckAlternativeDirection(ref Vector3 movementVector3, Vector3 alternativeDirection, float moveDistance, ref bool canMove)
    {        
        if (!canMove)
        {            
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, alternativeDirection, moveDistance);
            if (canMove)
            {
                movementVector3 = alternativeDirection.normalized;
            }
        }
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 movementVector2 = gameInput.GetInputVectorNormalized();
        if (movementVector2 == Vector2.zero)
        {
            return;
        }
        Vector3 movementVector3 = new Vector3(movementVector2.x, 0f, movementVector2.y);
        float moveDistance = Time.deltaTime * movementSpeed;

        isWalking = movementVector3 != Vector3.zero;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, movementVector3, moveDistance);
        transform.forward = Vector3.Slerp(transform.forward, movementVector3, Time.deltaTime * rotationSpeed);
        // Check if movement possible in direction: X
        if (!canMove)
        {
            CheckAlternativeDirection(ref movementVector3, new Vector3(movementVector3.x, 0f, 0f), moveDistance, ref canMove);
        }
        // Check if movement possible in direction: Z
        if (!canMove)
        {
            CheckAlternativeDirection(ref movementVector3, new Vector3(0f, 0f, movementVector3.z), moveDistance, ref canMove);
        }

        if (canMove)
        {
            transform.position += movementVector3 * moveDistance;
        }
        
    }

    private void HandleInteractions()
    {        
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactionDistance, CLEAR_COUNTER_LAYER_MASK))
        {            
            if (raycastHit.transform.TryGetComponent(out BaseCounter clearCounter))
            {
                if (selectedBaseCounter != clearCounter)
                {
                    
                    SelectClearCounter(clearCounter);
                }                
            }
            else
            {
                DeselectClearCounter();
            }
        }
        else
        {
            DeselectClearCounter();
        }        
    }

    private void DeselectClearCounter()
    {
        selectedBaseCounter?.DeselectBaseCounter();
        selectedBaseCounter = null;
    }

    private void SelectClearCounter(BaseCounter baseCounter)
    {
        selectedBaseCounter?.DeselectBaseCounter();
        selectedBaseCounter = baseCounter;
        selectedBaseCounter?.SelectBaseCounter();
    }


    public GameObject GetKitchenObjectTargetPosition()
    {
        return kitchenObjectTargetPosition;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;        
    }

    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }

    public void ClearKitchenObject()
    {
        this.kitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return this.kitchenObject != null;
    }
}
