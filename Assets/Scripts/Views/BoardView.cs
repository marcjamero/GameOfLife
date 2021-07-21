using System.Collections.Generic;
using UnityEngine;
using Controller;
using Model;

namespace View {
    public class BoardView : MonoBehaviour {

        private const float PIXEL_PER_UNIT = 100.0f;

        [SerializeField]
        private CellView cellPrefab;

        private Manager manager;
        private BoardDataScript boardData;
        private List<CellView> cells = new List<CellView>();

        public void SetView(BoardDataScript boardData, Manager manager) {
            ClearChildren();
            this.boardData = boardData;
            var size = boardData.GridSize;
            var cellSize = boardData.CellSize * PIXEL_PER_UNIT;
            var spacing = boardData.Spacing * (int)PIXEL_PER_UNIT;
            float offsetX = (float)size.x * (cellSize.x + spacing) * -0.5f + ((cellSize.x + spacing) * 0.5f);
            float posX = offsetX;
            float posY = (float)size.y * (cellSize.y + spacing) * 0.5f + ((cellSize.y + spacing) * -0.5f);

            for (int y = 0; y < size.y; y++) {
                for (int x = 0; x < size.x; x++) {
                    var cell = Instantiate(cellPrefab, transform, false);
                    cell.SetView(new Vector2Int(x, y), boardData, manager);
                    cell.transform.localPosition = new Vector3(posX, posY, 0f);
                    cells.Add(cell);

                    posX += cellSize.x + spacing;
                }
                posX = offsetX;
                posY -= cellSize.y + spacing;
            }

            this.manager = manager;
            manager.OnRefresh += OnRefresh;
        }

        private void OnRefresh(GridData data) {
            var size = data.Size;
            for (int y = 0; y < size.y; y++) {
                for (int x = 0; x < size.x; x++) {
                    SetCellState(data.GetData(x, y), x, y, boardData);
                }
            }
        }

        private void SetCellState(CellDataType type, int x, int y, BoardDataScript boardData) {
            cells[y * boardData.GridSize.x + x].SetState(type, boardData);
        }

        private void ClearChildren() {
            var children = GetComponentsInChildren<Transform>();
            foreach (var child in children) {
                if (child != transform) {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
