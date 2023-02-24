using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemoved;

    [SerializeField] private KitchenObjectSO platesKitchenObjectSO;

    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;
    private int platesSpawnedAmount;
    private int platesSpawnedAmountMax = 4;

    private KitchenObjectSO ingredientSO;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax ) {
            spawnPlateTimer = 0f;

            if (platesSpawnedAmount < platesSpawnedAmountMax) {
                platesSpawnedAmount++;

                OnPlateSpawned?.Invoke(this, new EventArgs());
            }
        }
    }

    public override void Interact(Player player) {
        if (player.HasKitchenObject()) {
            // Player is holding a KitchenObject, check if it's an empty Plate
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject) && plateKitchenObject.GetKitchenObjectSOList().Count == 0) {
                // Player is holding an Empty Plate
                // Allow them to put it away if there is room in the stack
                if (platesSpawnedAmount < platesSpawnedAmountMax) {
                    player.GetKitchenObject().DestroySelf();
                    platesSpawnedAmount++;

                    OnPlateSpawned?.Invoke(this, new EventArgs());
                }
            } else {
                // Player is not holding an Empty Plate

                // Clear Player's hands and spawn a Plate
                // Try to Add the Ingredient to the Plate
                ingredientSO = player.GetKitchenObject().GetKitchenObjectSO();
                player.GetKitchenObject().DestroySelf();

                KitchenObject.SpawnKitchenObject(platesKitchenObjectSO, player);
                if (player.GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                    Debug.Log(player.GetKitchenObject() + " + " + plateKitchenObject);
                } else {
                    Debug.Log("Bad");
                }
                                
                
                if (plateKitchenObject.TryAddIngredient(ingredientSO)) {
                    platesSpawnedAmount--;

                    OnPlateRemoved?.Invoke(this, new EventArgs());
                } else {
                    Debug.Log("Huh");
                    player.GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(ingredientSO, player);
                }
                

                ingredientSO = null;
            }
        } else {
            // Player is empty handed
            if (platesSpawnedAmount > 0) {
                // There is at least one plate
                platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(platesKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, new EventArgs());
            }
        }
    }

    public override void InteractAlternate(Player player) {
        // Allow the Player to speed up Plate production by interacting
    }
}
