using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private static string OPEN_CLOSE = "OpenClose";
    [SerializeField] private ContainerCounter containerCounter;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();        
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedKitchenObject += ContainerCounter_OnPlayerGrabbedKitchenObject;
    }

    private void ContainerCounter_OnPlayerGrabbedKitchenObject(object sender, System.EventArgs e)
    {
        animator.SetTrigger("OpenClose");
    }
}
