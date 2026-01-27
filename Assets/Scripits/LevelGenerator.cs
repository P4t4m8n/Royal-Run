using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject chunkPrefabs;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private int startingChunksAmount = 12;
    [SerializeField] private float chunkLength = 10f;
    [SerializeField] private float moveSpeed = 8f;
    private GameObject[] chunks = new GameObject[12];

    private void Start()
    {
        SpawnChunks();
    }

    private void Update()
    {
        MoveChunks();
    }

    private void SpawnChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            Vector3 position = CalculateChunkPosition(i);
            chunks[i] = Instantiate(chunkPrefabs, position, Quaternion.identity, chunkParent);
        }
    }

    private Vector3 CalculateChunkPosition(int index)
    {
        float zPosition = index * chunkLength + transform.position.z;
        return new Vector3(transform.position.x, transform.position.y, zPosition);
    }

    private void MoveChunks()
    {
        Vector3 translation = moveSpeed * Time.deltaTime * -transform.forward;
        for (int i = 0; i < chunks.Length; i++)
        {
            chunks[i].transform.Translate(translation);
        }

    }
}
