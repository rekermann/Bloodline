using System.Collections.Generic;
using Tiles;
using UnityEngine;

namespace EnemyAi
{
    public static class EnemyMovementController
    {
    
        public static MapTile MoveTowardsPlayer(MapTile tileStandingOn, int moveValue)
        {
            MapTile playerTile = UiManager.Instance.playerRef.GetComponent<PlayerMovementController>().tileStandingOn;
            if (tileStandingOn == playerTile) return tileStandingOn;
        
            List<MapTile> returnTiles = Pathfinding.StupidFind(tileStandingOn, playerTile);

            if(returnTiles.Count > moveValue)
                return  returnTiles[moveValue - 1];
            
            
            if (returnTiles.Count == 1) return tileStandingOn;
            if (returnTiles.Count == moveValue) return returnTiles[returnTiles.Count - moveValue];
            
            //FIX
            return returnTiles[returnTiles.Count-2];

        }

        public static PlayerController CheckIfPlayerIsInRange(MapTile tileStandingOn, int range)
        {
            if (!Pathfinding.CheckPlayerInRange(range, tileStandingOn)) return null;
            return Pathfinding.GetPlayerInRange(range, tileStandingOn)[0].unitOnTile.GetComponent<PlayerController>();
        }
    }
}
