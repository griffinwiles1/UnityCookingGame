using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject {

    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdded;
    public class OnIngredientAddedEventArgs : EventArgs {
        public KitchenObjectSO kitchenObjectSO;
    }

    public event EventHandler OnIngredientRemoved;


    [SerializeField] private List<KitchenObjectSO> validKitchenObjectSOList;
    
    private List<KitchenObjectSO> kitchenObjectSOList;

    private void Awake() {
        kitchenObjectSOList = new List<KitchenObjectSO>();
    }

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO) {
        if (!validKitchenObjectSOList.Contains(kitchenObjectSO)) {
            // Not a valid ingredient
            Debug.Log("Not a valid Ingredient " + kitchenObjectSO.ToString());
            return false;
        }
        
        if (kitchenObjectSOList.Contains(kitchenObjectSO)) {
            // Already has this type
            Debug.Log("Already has Ingredient " + kitchenObjectSO.ToString());
            return false;
        } else {
            kitchenObjectSOList.Add(kitchenObjectSO);
                        
            OnIngredientAdded?.Invoke(this, new OnIngredientAddedEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });
            Debug.Log("KitchenObjectSO Added: " + kitchenObjectSO.ToString());
            return true;
        }
    }


    public List<KitchenObjectSO> GetKitchenObjectSOList() {
        return kitchenObjectSOList;
    }

    public void ClearPlate() {
        kitchenObjectSOList.Clear();
        OnIngredientRemoved?.Invoke(this, EventArgs.Empty);
    }
}
