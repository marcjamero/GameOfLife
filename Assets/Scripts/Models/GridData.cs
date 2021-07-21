using UnityEngine;

namespace Model {
    public class GridData {

        public Vector2Int Size { private set; get; }
        public CellDataType[] Data { private set; get; }

        public GridData(Vector2Int size) {
            Size = size;
            Data = new CellDataType[size.x * size.y];
        }

        public CellDataType GetData(int x, int y) {
            if (x >= 0 && x < Size.x &&
                y >= 0 && y < Size.y) {
                return Data[y * Size.x + x];
            }
            return 0;
        }

        public void SetData(int x, int y, CellDataType data) {
            if (x >= 0 && x < Size.x &&
                y >= 0 && y < Size.y) {
                    Data[y * Size.x + x] = data;
            }
        }

        public void Clear() {
            var length = Data.Length;
            for (int i = 0; i < length; i++) {
                Data[i] = CellDataType.Dead;
            }
        }
    }
}
