using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingGameManager : MonoBehaviour {

    public static CookingGameManager Instance { get; private set; }

    private enum State {
        Closed,
        Preparation,
        DayCountdown,
        Day,
        DayOver,
    }

    private State state;
    private float preparationTimer = 1f;
    private float gameCountdownTimer = 3f;
    private float gamePlayingTimer = 10f;

    private void Awake() {
        Instance = this;

        state = State.Preparation;
    }

    private void Update() {
        switch (state) {
            case State.Preparation: 
                preparationTimer -= Time.deltaTime;
                if (preparationTimer < 0f) {
                    state = State.DayCountdown; 
                }
                break;

            case State.DayCountdown:
                gameCountdownTimer -= Time.deltaTime;
                if (gameCountdownTimer < 0f) {
                    state = State.Day;
                }
                break;

            case State.Day:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f) {
                    state = State.DayOver;
                }
                break;

            case State.DayOver:
                break;
        }
    }

    public bool IsGamePrepping() {
        return state == State.Preparation;
    }

    public bool IsGamePlaying() {
        return state == State.Day;
    }
}
