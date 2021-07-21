using System;
using UnityEngine;

namespace Model {
    public class Ticker {

        public event Action OnTick;

        private float time;
        private float interval;
        private bool isTicking = true;

        public bool IsTicking { get { return isTicking; } }

        public Ticker(float interval) {
            this.interval = interval;
            time = interval;
        }

        public void Start() {
            isTicking = true;
            time = interval;
        }

        public void Stop() {
            isTicking = false;
        }

        public void Update() {
            if (isTicking) {
                time += Time.deltaTime;
                if (time >= interval) {
                    time -= interval;
                    OnTick?.Invoke();
                }
            }
        }
    }
}