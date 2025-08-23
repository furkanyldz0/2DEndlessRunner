using UnityEngine;

public class TileSpawner : MonoBehaviour
{
    public Transform[] tiles;
    public float spawnPointX;
    public Transform parentObject;

    private Transform currentTile;
    private float currentTileRightEdgeX;
    CompositeCollider2D collider;

    void Start()
    {
        spawnPointX = transform.position.x; //tilespawner konumu
        CreateNewPlatform();
        //Instantiate(Object original, Vector3 position, Quaternion rotation, Transform parent);
    }

    
    void Update()
    {
        if(spawnPointX - currentTileRightEdgeX > 4)
        {
            CreateNewPlatform();
        }

        currentTileRightEdgeX = collider.bounds.max.x; //þuanki collider'lý nesnenin sahnedeki en sað noktasý
    }

    private void CreateNewPlatform()
    {
        
        int randomTileIndex = Random.Range(0, tiles.Length);
        //Debug.Log(randomTileIndex);
        currentTile = Instantiate(tiles[randomTileIndex], transform.position, Quaternion.identity, parentObject);
        //Debug.Log(currentTile.transform.position.x);
        collider = currentTile.GetComponent<CompositeCollider2D>();
        currentTileRightEdgeX = collider.bounds.max.x;
        //Debug.Log(currentTileRightEdgeX);
    }
}
