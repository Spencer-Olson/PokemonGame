using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] LayerMask solidObjectsLayer;
    [SerializeField] LayerMask encounterLayer;
    [SerializeField] LayerMask interactableLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask fovLayer;
    [SerializeField] LayerMask portalLayer;

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

    public LayerMask FOVLayer
    {
        get => fovLayer;
    }
    public LayerMask PortalLayer
    {
        get => portalLayer;
    }

    public LayerMask TriggerableLayers
    {
        get => encounterLayer | fovLayer | portalLayer;
    }
}
