using UnityEngine;
using UnityEngine.UI;
using Controller;

namespace View {

    [RequireComponent(typeof(Image), typeof(Button))]
    public class CellView : MonoBehaviour {

        private Image image;
        private Button button;
        private Manager manager;
        private Vector2Int index;

        public void SetView(Vector2Int index, BoardDataScript boardData, Manager manager) {
            this.index = index;
            this.manager = manager;
            transform.localScale = new Vector3(boardData.CellSize.x, boardData.CellSize.y, 1f);
        }

        public void SetState(CellDataType type, BoardDataScript boardData) {
            image.color = type == CellDataType.Alive ? boardData.LiveColor : Color.white;
        }

        private void Start() {
            image = GetComponent<Image>();
            button = GetComponent<Button>();

            image.color = Color.white;
            button.onClick.AddListener(() => {
                manager.OnToggleCellState(index);
            });
        }
    }
}
