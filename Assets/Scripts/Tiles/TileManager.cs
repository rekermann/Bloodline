using System.Collections.Generic;
using EnemyAi;
using Unity.Mathematics;
using UnityEngine;

namespace Tiles
{
    public class TileManager : MonoBehaviour
    {
        public static TileManager Instance;
        private List<MapTile> _mapTiles = new List<MapTile>();
        public MapTile selectedTile;
        private List<MapTile> _highlightedTiles = new List<MapTile>();

        private void Awake()
        {
            Instance = this;
        }
    
        public void UnhighlightTiles()
        {
            foreach (var tile in _highlightedTiles)
            {
                tile.UnhighlightTile();
            
            }
        }
        public void AddMapTile(MapTile tile)
        {
            _mapTiles.Add(tile);
            if (tile.spawnTile)
            {
                HighlightTile(tile);
                UiManager.Instance.mouseController.OnClickedObject -= TrySpawnPlayer;
                UiManager.Instance.mouseController.OnClickedObject += TrySpawnPlayer;
            }

            if (tile.enemySpawn != null)
            {
                SpawnEnemy(tile);
            }
        }
    
        public void SpawnPlayer(MapTile t)
        {
            var playerPrefab = UiManager.Instance.playerPrefab;
            var playerRef = Instantiate(playerPrefab, t.transform.position, quaternion.identity);
            playerRef.GetComponent<PlayerMovementController>().tileStandingOn = t;
            CombatManager.Instance.SetPlayerReference(playerRef);
            UiManager.Instance.SetPlayerRefrence(playerRef);
            t.unitOnTile = playerRef;
        
        }

        public void SpawnEnemy(MapTile tile)
        {
            var monsterRef = Instantiate(tile.enemySpawn, tile.transform.position, quaternion.identity);
            var enemy = monsterRef.GetComponent<Enemy>();
            //CombatManager.Instance.AddEnemy(enemy);
            tile.unitOnTile = monsterRef;
            enemy.tileStandingOn = tile;
        }

        public void TrySpawnPlayer(GameObject obj)
        {
            MapTile tile = obj.GetComponent<MapTile>();
            if (tile == null) return;
            if(!tile.highlighted) return;
            UiManager.Instance.mouseController.OnClickedObject -= TrySpawnPlayer;
            SpawnPlayer(tile);
            UnhighlightTiles();

        }


        public void HighlightTile(MapTile tile)
        {
            tile.HighlightTile();
            _highlightedTiles.Add(tile);
        }

        public void HighlightTilesInRange(MapTile tile, int range)
        {
            _highlightedTiles = Pathfinding.HighlightTilesInRange(range, tile);
        
        }


        public List<MapTile> GetHighlightedTiles()
        {
            return _highlightedTiles;
        }
    
    
    
    }
}
