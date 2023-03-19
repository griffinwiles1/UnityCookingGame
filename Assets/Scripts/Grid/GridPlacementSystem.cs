using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlacementSystem : MonoBehaviour {

    [SerializeField] private CounterSO placedCounterSO;
    [SerializeField] private Transform playerInteractPoint;
    
    private GridXZ<GridObject> grid;
    private CounterSO.Dir dir = CounterSO.Dir.Down;

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
        if (Input.GetKeyDown(KeyCode.P)) {
            grid.GetXZ(playerInteractPoint.position, out int x, out int z);

            List<Vector2Int> gridPositionList = placedCounterSO.GetGridPositionList(new Vector2Int(x, z), CounterSO.Dir.Down); 

            // Test can build
            bool canBuild = true;
            foreach (Vector2Int gridPosition in gridPositionList) {
                if (!grid.GetGridObject(gridPosition.x, gridPosition.y).CanBuild()) {
                    canBuild = false;
                    break;
                }
            }
            
            if (canBuild) {
                Vector2Int rotationOffset = placedCounterSO.GetRotationOffset(dir);
                Vector3 placedCounterSOWorldPosition = grid.GetWorldPosition(x, z) +
                    new Vector3(rotationOffset.x, 0, rotationOffset.y) * grid.GetCellSize();

                Transform builtTransform = 
                    Instantiate(
                        placedCounterSO.prefab,
                        placedCounterSOWorldPosition, 
                        Quaternion.Euler(0, placedCounterSO.GetRotationAngle(dir), 0)
                    );
                
                foreach (Vector2Int gridPosition in gridPositionList) {
                    grid.GetGridObject(gridPosition.x, gridPosition.y).SetTransform(builtTransform);
                }
            } else {
                // TODO - Swap with what's there
                Debug.Log("Already somethin here m8");
            }
            
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            dir = CounterSO.GetNextDir(dir);
            Debug.Log(dir);
        }
    }


    //TODO 

}
