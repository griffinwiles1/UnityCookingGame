using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {


    public override void Interact(Player player) {
         if (!HasKitchenObject()) {
            // There is no KitchenObject here
            if (player.HasKitchenObject()) {
                // Player is carrying a KitchenObject
                player.GetKitchenObject().SetKitchenObjectParent(this);
            } else {
                // Player not carrying a KitchenObject
            }
        } else {
            // There is a KitchenObject here
            if (player.HasKitchenObject()) {
                // Player is carrying a KitchenObject
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) { 
                    // Player is holding a Plate
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    } 
                } else {
                    // Player is not holding a Plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        // Counter is holding a Plate
                        if (plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO())) {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } else {
                // Player not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}

