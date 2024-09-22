using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidObjectsLayer;
    [SerializeField] LayerMask encounterLayer;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask playerLayer;

    public static GameLayers i {get; set;}

    private void Awake()
    {
        i = this;
    }

    public LayerMask SolidLayer
    {
        get =>  solidObjectsLayer;
    }
    public LayerMask EncounterLayer
    {
        get => encounterLayer;
    }
    public LayerMask InteractableLayer
    {
        get => interactableLayer; 
    }
    public LayerMask PlayerLayer
    {
        get => playerLayer;
    }
}
