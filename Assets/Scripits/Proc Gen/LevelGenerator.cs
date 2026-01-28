using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GameObject chunkPrefabs;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private int startingChunksAmount = 12;
    [SerializeField] private float chunkLength = 10f;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float minMoveSpeed = 2f;

    private List<GameObject> chunks = new();

    private void Start()
    {
        SpawnChunks();
    }

    private void Update()
    {
        MoveChunks();
    }

    public void ChangeChunkMoveSpeed(float speedAmount)
    {
        moveSpeed += speedAmount;
        if (moveSpeed < minMoveSpeed) moveSpeed = minMoveSpeed;

        Physics.gravity = new(Physics.gravity.x, Physics.gravity.y, Physics.gravity.z - speedAmount);

    }


    private void SpawnChunks()
    {
        for (int i = 0; i < startingChunksAmount; i++)
        {
            SpawnChunkSingle();
        }
    }

    private void SpawnChunkSingle()
    {
        Vector3 position = CalculateChunkPosition();
        chunks.Add(Instantiate(chunkPrefabs, position, Quaternion.identity, chunkParent));
    }

    private Vector3 CalculateChunkPosition()
    {
        float zPosition = chunks.Count == 0 ?
                          transform.position.z :
                          chunks[^1].transform.position.z + chunkLength;

        return new Vector3(transform.position.x, transform.position.y, zPosition);
    }

    private void MoveChunks()
    {
        Vector3 translation = moveSpeed * Time.deltaTime * -transform.forward;
        for (int i = 0; i < chunks.Count; i++)
        {
            GameObject chunk = chunks[i];
            chunk.transform.Translate(translation);

            bool isChunkBehindPlayer = chunk.transform.position.z <= Camera.main.transform.position.z
             - chunkLength;

            if (!isChunkBehindPlayer) continue;

            chunks.RemoveAt(i);
            Destroy(chunk);

            SpawnChunkSingle();
        }

    }
}
