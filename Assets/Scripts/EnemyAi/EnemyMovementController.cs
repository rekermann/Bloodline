using System.Collections.Generic;
using Tiles;

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

            if (returnTiles.Count == moveValue) return returnTiles[returnTiles.Count - 2];
            if (returnTiles.Count == 1) return tileStandingOn;
        
            return returnTiles[returnTiles.Count-1];

        }

        public static PlayerController CheckIfPlayerIsInRange(MapTile tileStandingOn, int range)
        {
            if (!Pathfinding.CheckPlayerInRange(range, tileStandingOn)) return null;
            return Pathfinding.GetPlayerInRange(range, tileStandingOn)[0].unitOnTile.GetComponent<PlayerController>();
        }
    }
}
