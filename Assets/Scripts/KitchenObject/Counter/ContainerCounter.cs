using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter {

    public event EventHandler OnPlayerGrabbedObject;


    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            // Player is carrying a KitchenObjectSO

            if (player.GetKitchenObject().GetKitchenObjectSO() == kitchenObjectSO) {
                // Player is holding the same type of KitchenObjectSO
                // Allow them to return it to the Container
                player.GetKitchenObject().DestroySelf();

                OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
            } else if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                // Player is carrying a Plate
                if (plateKitchenObject.TryAddIngredient(kitchenObjectSO)) {
                    // Add the KitchenObjectSO to the Plate
                    OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
                }
            }        
        } else {
            // Player is not carrying anything
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        
        // TODO Can return same food object
    }
}
