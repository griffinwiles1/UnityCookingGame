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
                if (player.GetKitchenObject() is PlateKitchenObject) { 
                    // Player is holding a plate
                    PlateKitchenObject plateKitchenObject = player.GetKitchenObject() as PlateKitchenObject;
                    if (plateKitchenObject.TryAddIngerdient(GetKitchenObject().GetKitchenObjectSO())) {
                        GetKitchenObject().DestroySelf();
                    }
                }
            } else {
                // Player not carrying a KitchenObject
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}

