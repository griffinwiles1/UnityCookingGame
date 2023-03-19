using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingGameManager : MonoBehaviour {

    public static CookingGameManager Instance { get; private set; }

    private enum State {
        Closed,
        OpenCountdown,
        Open,
        BackoutCountdown,
    }

    private State state;
    private float gameCountdownTimer;
    private float gamePlayingTimer = 30f;

    private void Awake() {
        Instance = this;

        state = State.Closed;
    }

    private void Update() {
        switch (state) {
            case State.Closed:
                gameCountdownTimer = 5f;
                if (Input.GetKeyDown(KeyCode.Q)) {
                    state = State.OpenCountdown; 
                }
                break;

            case State.OpenCountdown:
                gameCountdownTimer -= Time.deltaTime;
                Debug.Log(gameCountdownTimer);
                if (gameCountdownTimer < 0f) {
                    state = State.Open;
                }
                if (Input.GetKeyDown(KeyCode.Q)) {
                    state = State.Closed;
                }
                break;

            case State.Open:
                gameCountdownTimer = 5f;
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer > 20f && Input.GetKeyDown(KeyCode.Q)) {
                    state = State.BackoutCountdown;
                }
                if (gamePlayingTimer < 0f) {
                    state = State.Closed;
                }
                break;

            case State.BackoutCountdown:
                gameCountdownTimer -= Time.deltaTime;
                Debug.Log(gameCountdownTimer);
                if (gameCountdownTimer < 0f) {
                    state = State.Closed;
                }
                if (Input.GetKeyDown(KeyCode.Q)) {
                    state = State.Open;
                }
                break;
        }
    }

    public bool IsGamePrepping() {
        return (state == State.Closed);
    }

    public bool IsGamePlaying() {
        return state == State.Open;
    }
}
