using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractPoint : MonoBehaviour {

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }

    [SerializeField] private LayerMask countersLayerMask;

    private BaseCounter counter;
    private BaseCounter selectedCounter;

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Test Collision");
        if (collision.gameObject.TryGetComponent(out BaseCounter baseCounter)) {
            // Has ClearCounter
            if (baseCounter != selectedCounter) {
                SetSelectedCounter(baseCounter);
            }
        } else {
            SetSelectedCounter(null);
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (counter) {
            SetSelectedCounter(null);
        }
    }

    public void SetSelectedCounter(BaseCounter selectedCounter) {
        //this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });
    }
}
