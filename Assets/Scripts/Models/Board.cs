using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Model {
    public class Board {

        public event Action<GridData> OnRefresh;

        private BoardDataScript data;
        private GridData gridData;
        private bool skipTick = true;

        public Board(BoardDataScript data) {
            this.data = data;
            gridData = new GridData(data.GridSize);
            if (data.InitialState == null || data.InitialState.Length == 0) {
                RandomizeLives(gridData);
            }
            else {
                ApplyInitialState(data.InitialState, gridData);
            }
        }

        public GridData Tick() {
            if (skipTick) {
                skipTick = false;
                return gridData;
            }

            var tempGrid = (CellDataType[])gridData.Data.Clone();
            var size = gridData.Size;
            for (int y = 0; y < size.y; y++) {
                for (int x = 0; x < size.x; x++) {
                    var index = y * size.x + x;
                    var isAlive = tempGrid[index] == CellDataType.Alive;
                    var lives = GetLiveCountFromNeighbours(new Vector2Int(x, y), tempGrid, size);
                    if (!isAlive) {
                        gridData.SetData(x, y, lives == 3 ? CellDataType.Alive : tempGrid[index]);
                    }
                    else if (lives < 2 || lives > 3) {
                        gridData.SetData(x, y, CellDataType.Dead);
                    }
                }
            }
            return gridData;
        }

        public void Clear() {
            gridData.Clear();
            OnRefresh?.Invoke(gridData);
        }

        public void ToggleCellState(Vector2Int index) {
            gridData.SetData(index.x, index.y, gridData.GetData(index.x, index.y) == CellDataType.Alive ? CellDataType.Dead : CellDataType.Alive);
            OnRefresh?.Invoke(gridData);
        }

        private void ApplyInitialState(Vector2Int[] data, GridData gridData) {
            gridData.Clear();
            foreach (var index in data) {
                gridData.SetData(index.x, index.y, CellDataType.Alive);
            }
        }

        private void RandomizeLives(GridData gridData) {
            var size = gridData.Size;
            for (int y = 0; y < size.y; y++) {
                for (int x = 0; x < size.x; x++) {
                    gridData.SetData(x, y, Random.Range(0, 2) == 1 ? CellDataType.Alive : CellDataType.Dead);
                }
            }
        }

        private int GetLiveCountFromNeighbours(Vector2Int index, CellDataType[] grid, Vector2Int gridSize) {
            var count = 0;
            for (int y = index.y - 1; y <= index.y + 1; y++) {
                for (int x = index.x - 1; x <= index.x + 1; x++) {
                    if (x < 0 || x >= gridSize.x ||
                       (y < 0 || y >= gridSize.y)) {
                        continue;
                    }
                    count += grid[y * gridSize.x + x] == CellDataType.Alive ? 1 : 0;
                }
            }
            count -= grid[index.y * gridSize.x + index.x] == CellDataType.Alive ? 1 : 0;
            return count;
        }
    }
}
