using System;
using EnemyAi;
using UnityEngine;
using Utilities;

namespace Tiles
{
    public class MapTile : MonoBehaviour, IClickable
    {
        [NonSerialized] public MapTile[] neighbours = new MapTile[6];
        [NonSerialized] public bool highlighted;
        [NonSerialized] public GameObject unitOnTile = null;
        [NonSerialized] public float distanceFromTarget;
        [NonSerialized] public int pathCount;

        private SpriteRenderer tileSprite;


        private Vector2[] directions = {new Vector2(1,0), new Vector2(1,1.5f), new Vector2(-1,1.5f), 
            new Vector2(-1, 0), new Vector2(-1,-1.5f), new Vector2(1,-1.5f),};
    
        public bool spawnTile;
        public bool isWalkable = true;

        public GameObject enemySpawn;

        private void Start()
        {
            GetNeighbours();
            tileSprite = gameObject.GetComponent<SpriteRenderer>();
            TileManager.Instance.AddMapTile(this);
        
        }


        public void HighlightTile()
        {
            if(highlighted) return;
            tileSprite.color = Color.green;
            highlighted = true;
        }
        public void UnhighlightTile()
        {
            if(!highlighted) return;
            tileSprite.color = Color.white;
            highlighted = false;
        }
    
        private void GetNeighbours()
        {
            RaycastHit2D hits = new RaycastHit2D();
            int i = 0;
        
            foreach (var direction in directions)
            {
                hits = Physics2D.Raycast(transform.position, direction, 1f);
            
                if(hits.collider == null) continue;
                MapTile hitTile = hits.collider.GetComponent<MapTile>();
                if (hitTile != null && hitTile.isWalkable)
                {
                    neighbours[i]= hitTile;
                }
                i++;
            }
               
        
        }
        public void Clicked()
        {
            TileManager.Instance.selectedTile = this;
        }

        public bool NextToPlayer()
        {
            foreach (var nTile in neighbours)
            {
                if (nTile == null) continue;
                if (nTile.unitOnTile)
                {
                    if (nTile.unitOnTile.GetComponent<PlayerController>() != null)
                    {
                        return true;
                    }
                }
            
            }

            return false;
        }

        public bool NextToEnemy()
        {
            foreach (var nTile in neighbours)
            {
                if (nTile == null) continue;
                if (nTile.unitOnTile != null)
                {
                    if (nTile.unitOnTile.GetComponent<Enemy>() != null)
                    {
                        return true;
                    }
                }
            
            }
            return false;
        }
    }
}
