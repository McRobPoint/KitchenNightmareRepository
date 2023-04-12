using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent 
{
    [SerializeField] protected GameObject kitchenObjectTargetPosition;    
    [SerializeField] protected GameObject selectedBaseCounterVisual;


    protected KitchenObject kitchenObject;

    public virtual void Interact(Player player) { }    
    


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

    public void SelectBaseCounter()
    {
        selectedBaseCounterVisual.SetActive(true);
    }
    public void DeselectBaseCounter()
    {
        selectedBaseCounterVisual.SetActive(false);
    }

    public bool HasKitchenObject()
    {
        return this.kitchenObject != null;
    }
}
