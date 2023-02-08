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
        if (!player.HasKitchenObject()) {
            // Player is empty handed
            if (platesSpawnedAmount > 0) {
                // There is at least one plate
                platesSpawnedAmount--;

                KitchenObject.SpawnKitchenObject(platesKitchenObjectSO, player);

                OnPlateRemoved?.Invoke(this, new EventArgs());
            }
        }
    }

}
