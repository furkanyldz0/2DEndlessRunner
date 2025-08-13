using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using static UnityEngine.UI.Image;

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
        if(spawnPointX - currentTileRightEdgeX > 4) //currentTile.transform.position.x platformun en sað noktasý
        {
            CreateNewPlatform();
        }

        currentTileRightEdgeX = collider.bounds.max.x;
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
