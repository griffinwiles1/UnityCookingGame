using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCounter : BaseCounter {

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            // Player has a KitchenObject to Destroy
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {
                // Player is holding a Plate

                if (plateKitchenObject.GetKitchenObjectSOList().Count == 0) {
                    // Plate is empty, Destroy it
                    player.GetKitchenObject().DestroySelf();
                } else {
                    // Plate has KitchenObjects on it, Clear them
                    plateKitchenObject.ClearPlate();
                }
            } else {
                // Player is not holding a Plate
                player.GetKitchenObject().DestroySelf();
            }
            
        }
    }

}
