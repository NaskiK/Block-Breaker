using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject blockPrefab;
    public int rows = 5;
    public int columns = 7;
    public float spacing = 0.1f;
    public Vector2 startPosition = new Vector2(-3.5f, 4f);

    // Rainbow color stops: red → orange → yellow → green → blue → indigo → violet
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

                GameObject block = Instantiate(blockPrefab, position, Quaternion.identity, transform);

                // Set rainbow color based on row
                SpriteRenderer sr = block.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    float t = (float)row / (rows - 1); // Goes from 0 to 1 based on row
                    sr.color = InterpolateRainbowColor(t);
                }
            }
        }
    }

    // Smoothly interpolates between rainbow color stops
    Color InterpolateRainbowColor(float t)
    {
        int segmentCount = rainbowStops.Length - 1;
        float scaledT = t * segmentCount;
        int index = Mathf.FloorToInt(scaledT);
        float lerpT = scaledT - index;

        if (index >= segmentCount) return rainbowStops[^1]; // Edge case: return last color
        return Color.Lerp(rainbowStops[index], rainbowStops[index + 1], lerpT);
    }
}
