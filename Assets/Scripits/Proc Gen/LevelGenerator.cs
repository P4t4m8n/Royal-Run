using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject[] chunkPrefabs;
    [SerializeField] private GameObject checkpointChunkPrefab;
    [SerializeField] private Transform chunkParent;
    [SerializeField] private ScoreManager scoreManager;


    [Header("Level Settings")]
    [SerializeField] private int startingChunksAmount = 12;
    [Tooltip("Length of a single chunk along the Z axis")]
    [SerializeField]
    private float chunkLength = 10f;
    [SerializeField] int checkpointChunkInterval = 8;
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float minMoveSpeed = 2f;
    [SerializeField] private float maxMoveSpeed = 20f;
    [SerializeField] private float minGravityZ = -22f;
    [SerializeField] private float maxGravityZ = -2f;

    private readonly List<GameObject> chunks = new();
    private int chunksSpawned = 0;

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
        float newMoveSpeed = moveSpeed + speedAmount;
        float clampedNewMoveSpeed = Mathf.Clamp(newMoveSpeed, minMoveSpeed, maxMoveSpeed);

        if (moveSpeed == clampedNewMoveSpeed) return;

        moveSpeed = clampedNewMoveSpeed;

        float newGravityZ = Physics.gravity.z + speedAmount;
        float clampedNewGravityZ = Mathf.Clamp(newGravityZ, minGravityZ, maxGravityZ);
        Physics.gravity = new(Physics.gravity.x, Physics.gravity.y, clampedNewGravityZ);

        cameraController.ChangeCameraFOV(speedAmount);
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

        GameObject chunkToSpawn = ChooseChunkToSpawn();

        GameObject newChunkGO = Instantiate(chunkToSpawn, position, Quaternion.identity, chunkParent);
        chunks.Add(newChunkGO);

        Chunk newChunk = newChunkGO.GetComponent<Chunk>();
        newChunk.Init(this, scoreManager);

        chunksSpawned++;
    }

    private GameObject ChooseChunkToSpawn()
    {
        return (chunksSpawned % checkpointChunkInterval == 0 && chunksSpawned != 0) ?
            checkpointChunkPrefab :
            chunkPrefabs[Random.Range(0, chunkPrefabs.Length)];
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
