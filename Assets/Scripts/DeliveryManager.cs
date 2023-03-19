using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;


    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 8f;
    private int waitingRecipesMax = 6;


    private void Awake() {
        Instance = this;

        waitingRecipeSOList = new List<RecipeSO>();    
    }

    private void Update() {
        if (CookingGameManager.Instance.IsGamePlaying()) {
            spawnRecipeTimer -= Time.deltaTime;
            if (spawnRecipeTimer <= 0f) {
                spawnRecipeTimer = spawnRecipeTimerMax;

                if (waitingRecipeSOList.Count < waitingRecipesMax) {
                    RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];

                    waitingRecipeSOList.Add(waitingRecipeSO);

                    OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
                }
            }
        } else if (CookingGameManager.Instance.IsGamePrepping()) {
            waitingRecipeSOList.Clear();
            OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        for (int i = 0; i < waitingRecipeSOList.Count; i++) {
            RecipeSO waitingRecipeSO = waitingRecipeSOList[i];

            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // Has the same number of ingredients
                bool plateContentsMatchRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    // Cycle through all ingredients in the Recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        // Cycle through all ingredients on the Plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        // This Recipe ingredient was not found on the Plate
                        plateContentsMatchRecipe = false;
                    }
                }

                if (plateContentsMatchRecipe) {
                    // Player delivered the correct Recipe
                    Debug.Log("Player delivered the correct recipe!");
                    waitingRecipeSOList.RemoveAt(i);
                    // TODO give the Player $$$ plus Rep
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }

        // No matches found
        // Player did not deliver a correct recipe
        Debug.Log("What even are you doing?!");
    }


    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }

}
