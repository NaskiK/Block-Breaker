using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;         // Normal block
    public GameObject powerUpBlockPrefab;  // Block that spawns 2 balls

    public int rows = 5;
    public int columns = 7;
    public float spacing = 0.1f;
    public Vector2 startPosition = new Vector2(-3.5f, 4f);

    [Range(0f, 1f)] public float powerUpChance = 0.15f;

    private Color[] rainbowStops = new Color[]
    {
        new Color(1f, 0.6f, 0.6f),   // Soft Red
        new Color(1f, 0.8f, 0.6f),   // Peachy Orange
        new Color(1f, 1f, 0.6f),     // Light Yellow
        new Color(0.7f, 1f, 0.7f),   // Soft Green
        new Color(0.7f, 0.85f, 1f),  // Light Blue
        new Color(0.8f, 0.7f, 1f),   // Lavender
        new Color(1f, 0.7f, 1f)      // Soft Pink/Violet
    };

    void Start()
    {
        SpawnBlocks();
    }

    void SpawnBlocks()
    {
        Vector2 size = blockPrefab.GetComponent<SpriteRenderer>().bounds.size;

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                Vector2 position = new Vector2(
                    startPosition.x + col * (size.x + spacing),
                    startPosition.y - row * (size.y + spacing)
                );

                // Decide whether to spawn a power-up block
                GameObject prefabToUse = Random.value < powerUpChance ? powerUpBlockPrefab : blockPrefab;

                GameObject block = Instantiate(prefabToUse, position, Quaternion.identity, transform);

                // Set rainbow color based on row
                SpriteRenderer sr = block.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    float t = (float)row / (rows - 1);
                    sr.color = InterpolateRainbowColor(t);
                }
            }
        }
    }

    Color InterpolateRainbowColor(float t)
    {
        int segmentCount = rainbowStops.Length - 1;
        float scaledT = t * segmentCount;
        int index = Mathf.FloorToInt(scaledT);
        float lerpT = scaledT - index;

        if (index >= segmentCount) return rainbowStops[^1];
        return Color.Lerp(rainbowStops[index], rainbowStops[index + 1], lerpT);
    }
}
