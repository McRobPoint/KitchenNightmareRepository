using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    private const string OPEN_CLOSE = "OpenClose";
    public event EventHandler OnPlayerGrabbedKitchenObject;

    [SerializeField] protected KitchenObjectSO kitchenObjectSO;
    

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject())
        {
            GameObject kitchenObjectGO = SpawnKitchenObject();
            KitchenObject kitchenObject = kitchenObjectGO.GetComponent<KitchenObject>();
            kitchenObject.SetKitchenObjectParent(player);

            OnPlayerGrabbedKitchenObject?.Invoke(this, EventArgs.Empty);
        }
        
    }

    public GameObject SpawnKitchenObject()
    {
        GameObject kitchenObjectGO = Instantiate(kitchenObjectSO.prefab, kitchenObjectTargetPosition.transform);
        kitchenObjectGO.transform.localPosition = Vector3.zero;
        return kitchenObjectGO;
    }
}
