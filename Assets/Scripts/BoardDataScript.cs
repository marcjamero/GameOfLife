using UnityEngine;

[CreateAssetMenu(fileName="Board Data", menuName="Game/Board Data")]
public class BoardDataScript : ScriptableObject {

    [SerializeField]
    private Vector2 cellSize;
    [SerializeField]
    private Vector2Int gridSize;
    [SerializeField]
    private float spacing;
    [SerializeField]
    private float tickSpeed;
    [SerializeField]
    private Color liveColor;
    [SerializeField]
    private Vector2Int[] initialState;

    public Vector2 CellSize { get { return cellSize; } }
    public Vector2Int GridSize { get { return gridSize; } }
    public float Spacing { get { return spacing; } }
    public float TickSpeed { get { return tickSpeed; } }
    public Color LiveColor { get { return liveColor; } }
    public Vector2Int[] InitialState { get { return initialState; } }
}
