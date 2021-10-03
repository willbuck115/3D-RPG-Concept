using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{

    // This script needs to be run during the loading transition.
    [SerializeField] private GameObject grassTile, waterTile;
    [SerializeField] private GameObject treePrefab, appleTree;
    [SerializeField] private GameObject tileMap;
    [SerializeField] private Transform terrain, treeParent;
    [SerializeField] private Sprite main, treeS, water, path, edge;
    [SerializeField] private Mesh fullMesh;

    [SerializeField] private TileBase _treeTile;
    private Tilemap map;

    bool debug = false;
    Vector3 centerOfOverlapBox;

    public void Generate()
    {
        /*
        map = tileMap.GetComponent<Tilemap>();

        AddTrees();

        foreach (var pos in map.cellBounds.allPositionsWithin)
        {

            Vector3Int localPosition = new Vector3Int(pos.x, pos.y, pos.z);
            Vector3 position = new Vector3(map.CellToWorld(localPosition).x + 2, 0, map.CellToWorld(localPosition).z + 2);//2
            if (map.HasTile(localPosition))
            {
                GenerateTile(localPosition, position);
            }
        }
        */
    }

    private void AddTrees()
    {
        foreach(var pos in map.cellBounds.allPositionsWithin)
        {
            Vector3Int local = new Vector3Int(pos.x, pos.y, pos.z);

            if (map.GetSprite(local) == main)
            {
                int rand = Random.Range(0, 11);
                if (rand == 2)
                {
                    map.SetTile(local, _treeTile);
                }
            }
        }
    }

    private void GenerateTile(Vector3Int localPosition, Vector3 position)
    {
        Sprite tile = map.GetSprite(localPosition);

        if (tile == path || tile == main)
        {
            GameObject obj = Instantiate(grassTile, position, Quaternion.identity);
            obj.transform.parent = terrain;
        }
        else if(tile == edge)
        {
            GameObject obj = Instantiate(grassTile, position, Quaternion.identity);
            obj.transform.parent = terrain.transform;
            obj.GetComponent<MeshFilter>().mesh = fullMesh;
        }
        else if (tile == treeS)
        {
            GameObject obj = Instantiate(grassTile, position, Quaternion.identity);
            obj.transform.parent = terrain;
            float yValue = Random.Range(0, 360);
            int rand = Random.Range(0, 10);
            if (rand == 1)
            {
                GameObject newTree = Instantiate(appleTree, position, Quaternion.Euler(0, yValue, 0));
                newTree.transform.parent = treeParent;
            }
            else
            {
                GameObject newTree = Instantiate(treePrefab, position, Quaternion.Euler(0, yValue, 0));
                newTree.transform.parent = treeParent;
            }
        }
        else if (tile == water)
        {
            GameObject obj = Instantiate(waterTile, position, Quaternion.identity);
            obj.transform.parent = terrain;
        }
    }
    /*
    public bool CanWalkOnTile(Vector3 t)
    {
        bool canWalk = false;
        Vector3 target = new Vector3(t.x, 0, t.z);
        Collider[] collider = Physics.OverlapSphere(target, 1f);
        if (collider.Length > 0)
        {
            for (int i = 0; i < collider.Length; i++)
            {
                if (collider[i].GetComponent<Tile>())
                {
                    Tile targetTile = collider[i].gameObject.GetComponent<Tile>();
                    if (targetTile.type == "Walkable")
                    {
                        canWalk = true;
                    }
                }
            }
        }
        else
        {
            canWalk = false;
        }

        return canWalk;
    }
    */
    /*
    public void MovementCall(string direction, Vector3 newPosition)
    {
        if(direction == "backward")
        {
            // Create the new tiles
            for (int z = (int)(newPosition.z / 4) - (renderDistance - 1); z <= (int)(newPosition.z / 4) + (renderDistance - 1); z++)
            {
                Vector3Int pointOnTileMap = new Vector3Int((int)(newPosition.x / 4) + renderDistance, z, 0);
                Vector3 positionToSpawnTile = new Vector3(map.CellToWorld(pointOnTileMap).x + 2, 0, map.CellToWorld(pointOnTileMap).z + 2); //2
                GenerateTile(pointOnTileMap, positionToSpawnTile);
            }
            // Delete the old ones

            for (int z = (int)(newPosition.z / 4) - (renderDistance - 1); z <= (int)(newPosition.z / 4) + (renderDistance - 1); z++)
            {
                Vector3Int mapPoint = new Vector3Int((int)(newPosition.x / 4) - (renderDistance - 1), z, 0);
                if (map.HasTile(mapPoint))
                {
                    centerOfOverlapBox = new Vector3((int)newPosition.x - ((renderDistance * 4) - 2), 1, z * 4);
                    Collider[] colliders = Physics.OverlapBox(centerOfOverlapBox, new Vector3(1, 3f, 1));
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        Destroy(colliders[i].gameObject);
                    }
                }
            }
            direction = null;
        }
        if (direction == "forward")
        {
            // Create the new tiles
            for (int z = (int)(newPosition.z / 4) - (renderDistance - 1); z <= (int)(newPosition.z / 4) + (renderDistance - 1); z++)
            {
                Vector3Int pointOnTileMap = new Vector3Int((int)(newPosition.x / 4) - renderDistance, z, 0);
                Vector3 positionToSpawnTile = new Vector3(map.CellToWorld(pointOnTileMap).x + 2, 0, map.CellToWorld(pointOnTileMap).z + 2); //2
                GenerateTile(pointOnTileMap, positionToSpawnTile);
            }
            // Delete the old ones
            for (int z = (int)(newPosition.z / 4) - (renderDistance - 1); z <= (int)(newPosition.z / 4) + (renderDistance - 1); z++)
            {
                Vector3Int mapPoint = new Vector3Int((int)(newPosition.x / 4) + renderDistance, z, 0);
                if (map.HasTile(mapPoint))
                {
                    centerOfOverlapBox = new Vector3((int)newPosition.x + ((renderDistance * 4) - 2), 1, z * 4);
                    Collider[] colliders = Physics.OverlapBox(centerOfOverlapBox, new Vector3(1 , 3, 1));
                    for(int i = 0; i < colliders.Length; i++)
                    {
                        Destroy(colliders[i].gameObject);
                        
                    }
                }
            }
            direction = null;
        }
        if (direction == "right")
        {
            // Create the new tiles
            for (int x = (int)(newPosition.x / 4) - (renderDistance - 1); x <= (int)(newPosition.x / 4) + (renderDistance - 1); x++)
            {
                Vector3Int pointOnTileMap = new Vector3Int(x, (int)(newPosition.z / 4) + renderDistance, 0);
                Vector3 positionToSpawnTile = new Vector3(map.CellToWorld(pointOnTileMap).x + 2, 0, map.CellToWorld(pointOnTileMap).z + 2); //2
                GenerateTile(pointOnTileMap, positionToSpawnTile);
            }
            // Delete the old ones
            for (int x = (int)(newPosition.x / 4) - (renderDistance - 1); x <= (int)(newPosition.x / 4) + (renderDistance - 1); x++)
            {
                Vector3Int mapPoint = new Vector3Int(x, (int)(newPosition.z / 4) - (renderDistance - 1), 0);
                if (map.HasTile(mapPoint))
                {
                    centerOfOverlapBox = new Vector3(x * 4, 1, (int)newPosition.z - ((renderDistance * 4) - 2));
                    //centerOfOverlapBox = new Vector3((int)newPosition.x + 26, 1, z * 4);
                    Collider[] colliders = Physics.OverlapBox(centerOfOverlapBox, new Vector3(1, 3, 1));
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        Destroy(colliders[i].gameObject);

                    }
                }
            }
            direction = null;
        }
        if (direction == "left")
        {
            // Create the new tiles
            for (int x = (int)(newPosition.x / 4) - (renderDistance - 1); x <= (int)(newPosition.x / 4) + (renderDistance - 1); x++)
            {
                Vector3Int pointOnTileMap = new Vector3Int(x, (int)(newPosition.z / 4) - renderDistance, 0);
                Vector3 positionToSpawnTile = new Vector3(map.CellToWorld(pointOnTileMap).x + 2, 0, map.CellToWorld(pointOnTileMap).z + 2); //2
                GenerateTile(pointOnTileMap, positionToSpawnTile);
            }
            // Delete the old ones
            for (int x = (int)(newPosition.x / 4) - (renderDistance - 1); x <= (int)(newPosition.x / 4) + (renderDistance - 1); x++)
            {
                Vector3Int mapPoint = new Vector3Int(x, (int)(newPosition.z / 4) + (renderDistance - 1), 0);
                if (map.HasTile(mapPoint))
                {
                    centerOfOverlapBox = new Vector3(x * 4, 1, (int)newPosition.z + ((renderDistance * 4) - 2));
                    Collider[] colliders = Physics.OverlapBox(centerOfOverlapBox, new Vector3(1, 3, 1));
                    for (int i = 0; i < colliders.Length; i++)
                    {
                        Destroy(colliders[i].gameObject);

                    }
                }
            }
            direction = null;
        }
    }
    */
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if(debug)
        {
            Gizmos.DrawWireCube(centerOfOverlapBox, new Vector3(2f, 6f, 2f));
        }
    }
}
