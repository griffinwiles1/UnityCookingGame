using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementSystem : MonoBehaviour {

    [SerializeField] private Transform counterPrefab;
    [SerializeField] private Transform playerInteractPoint;
    private GridXZ<GridObject> grid;

    private void Awake() {

        int gridWidth = 10;
        int gridHeight = 10;
        float cellSize = 1.5f;
        grid = new GridXZ<GridObject>(gridWidth, gridHeight, cellSize, new Vector3(0, 0, 0), (GridXZ<GridObject> g, int x, int z) => new GridObject(g, x, z));
    }

    public class GridObject { 
        
        private GridXZ<GridObject> grid;
        private int x;
        private int z;
        private Transform transform;

        public GridObject(GridXZ<GridObject> grid, int x, int z) {
            this.grid = grid;
            this.x = x;
            this.z = z;
        }

        // TODO - Need to SetTransform for what's already placed
        public void SetTransform(Transform transform) {
            this.transform = transform;
            grid.TriggerGridObjectChanged(x, z);
        }

        public void ClearTransform(Transform transform) {
            transform = null;
        }

        public bool CanBuild() {
            return transform == null;
        }

        public override string ToString() {
            return x + ", " + z + "\n" + transform;
        }
    }

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            grid.GetXZ(playerInteractPoint.position, out int x, out int z);

            GridObject gridObject = grid.GetGridObject(x, z);
            if (gridObject.CanBuild()) {
                Transform builtTransform = Instantiate(counterPrefab, grid.GetWorldPosition(x, z), Quaternion.identity);
                gridObject.SetTransform(builtTransform);
            } else {
                // TODO - Swap with what's there
                Debug.Log("Already somethin here m8");
            }
            
        }
    }
}
