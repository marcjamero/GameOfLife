using System;
using UnityEngine;
using Model;
using View;

namespace Controller {
    public class Manager : MonoBehaviour {

        [SerializeField]
        private BoardDataScript boardData;
        [SerializeField]
        private BoardView view;

        public event Action<GridData> OnRefresh;

        private Ticker ticker;
        private Board board;

        public void OnStartTick() {
            ticker.Start();
        }

        public void OnStopTick() {
            ticker.Stop();
        }

        public void OnClearBoard() {
            if (!ticker.IsTicking) {
                board.Clear();
            }
        }

        public void OnToggleCellState(Vector2Int index) {
            if (!ticker.IsTicking) {
                board.ToggleCellState(index);
            }
        }

        private void Start() {
            ticker = new Ticker(boardData.TickSpeed);
            board = new Board(boardData);

            view.SetView(boardData, this);
            board.OnRefresh += RefreshView;
            ticker.OnTick += Tick;
        }

        private void Update() {
            ticker.Update();
        }

        private void RefreshView(GridData data) {
            OnRefresh?.Invoke(data);
        }

        private void Tick() {
            var gridData = board.Tick();
            OnRefresh?.Invoke(gridData);
        }
    }
}
