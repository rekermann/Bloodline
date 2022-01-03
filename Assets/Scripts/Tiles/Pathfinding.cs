using System;
using System.Collections.Generic;
using EnemyAi;

namespace Tiles
{
    public static class Pathfinding
    {
        private static MapTile _startNode;
        private static MapTile _endNode;
        private static float _distanceCost;
        private static float _currentCost;
    
    
        public static float DistanceBetweenTiles(MapTile s, MapTile e)
        {
            float xDist = (float) Math.Pow((e.transform.position.x - s.transform.position.x), 2); 
            float yDist = (float) Math.Pow((e.transform.position.y - s.transform.position.y), 2);
            float distance = (float) Math.Sqrt(xDist + yDist);
            return distance;
        }

        public static List<MapTile> StupidFind(MapTile startNode, MapTile endNode)
        {
            _startNode = startNode;
            _endNode = endNode;
            _distanceCost  = DistanceBetweenTiles(_startNode, _endNode);
            _currentCost = _distanceCost;
        
            List<MapTile> returnList = new List<MapTile>();
            MapTile currentNode = _startNode;
            MapTile previousNode;
            while (currentNode != _endNode)
            {
                previousNode = currentNode;
                currentNode = GetClosetNeighbour(currentNode);
                if (previousNode == currentNode)
                {
                    returnList = LessStupidFind();
                    break;
                }
                returnList.Add(currentNode);
            }


            return returnList;
        }

        private static List<MapTile> LessStupidFind()
        {
            List<MapTile> searchList = new List<MapTile>();
            List<MapTile> doneSearchedList = new List<MapTile>();

            searchList.Add(_startNode);

            while (searchList.Count > 0)
            {
                MapTile searchTile = GetLowestDistanceFromEnd(searchList);
                foreach (var tile in searchTile.neighbours)
                {
                    if(tile == null || doneSearchedList.Contains(tile) || searchTile.unitOnTile != null) continue;
                    tile.distanceFromTarget = DistanceBetweenTiles(tile, _endNode);
                    tile.pathCount = searchTile.pathCount + 1;
                    searchList.Add(tile);
                }

                if(!doneSearchedList.Contains(searchTile)) doneSearchedList.Add(searchTile);
                searchList.Remove(searchTile);
                if(searchTile == _endNode) break;
            }
    
            return GetShortestPath(doneSearchedList);
        }

        public static List<MapTile> GetShortestPath(List<MapTile> pathList)
        {
            List<MapTile> tilesToRemove = new List<MapTile>();
            for (int i = 0; i < pathList.Count; i++)
            {
                int check = pathList[i].pathCount;
                for (int j = i+1; j < pathList.Count; j++)
                {
                    if(check == pathList[j].pathCount) tilesToRemove.Add(pathList[i]);
                }
            }
        
            foreach (var tile in tilesToRemove)
            {
                pathList.Remove(tile);
            }
            return pathList;
        }
    
        public static MapTile GetLowestDistanceFromEnd(List<MapTile> search)
        {
            float closets = 99999f;
            MapTile returnTile = null;
            foreach (var tile in search)
            {
                if (tile.distanceFromTarget < closets)
                {
                    closets = tile.distanceFromTarget;
                    returnTile = tile;
                }
            
            }
            return returnTile;
        }

        public static MapTile GetClosetNeighbour(MapTile tile)
        {
            if (tile == null) return null;
            MapTile closet = tile;
            float tempCost = 0f;
        
            foreach (var neighbour in tile.neighbours)
            {
                if (neighbour == _endNode) { return _endNode;}
                if(neighbour == null) continue;
                if(neighbour.unitOnTile != null) continue;
                float tmp = DistanceBetweenTiles(neighbour, _endNode);
                if (tmp < _currentCost && tmp > tempCost)
                {
                
                    closet = neighbour;
                    _currentCost = tmp;
                }
            }
        

            return closet;
        }
    
        public static bool CheckEnemiesInRange(int range, MapTile tile)
        {
            return GetEnemiesInRange(range, tile).Count > 0;
        }

        public static List<MapTile> HighlightTilesInRange(int range, MapTile tile)
        {
            List<MapTile> check = new List<MapTile>();
            List<MapTile> tmpCheck = new List<MapTile>();
            List<MapTile> alreadyChecked = new List<MapTile>();
            List<MapTile> highlightedTiles = new List<MapTile>();
            check.Add(tile);
            highlightedTiles.Add(tile);
            if (tile.isWalkable)
            {
                tile.HighlightTile();
                if(!alreadyChecked.Contains(tile)) alreadyChecked.Add(tile);
            }
        
            if (range == 0)
            {
           
                return highlightedTiles;
            }
        
            while (range > 0)
            {
                foreach (var uncheckedNode in check)
                {
                    foreach (var nTile in uncheckedNode.neighbours)
                    {
                        if(alreadyChecked.Contains(nTile)) {continue;}
                        if (nTile != null)
                        {
                        
                            if (nTile.isWalkable && nTile.unitOnTile == null)
                            {
                                nTile.HighlightTile();
                                if(!highlightedTiles.Contains(nTile)) highlightedTiles.Add(nTile);
                                if(!alreadyChecked.Contains(nTile)) {tmpCheck.Add(nTile);}
                            }
                        
                        }
                    }
                    alreadyChecked.Add(uncheckedNode);
                }

                foreach (var tmpTile in tmpCheck)
                {
                    check.Add(tmpTile);
                }
                range--;
            
            }
            return highlightedTiles;
        }

        public static List<MapTile> GetEnemiesInRange(int range, MapTile tile)
        {
            List<MapTile> check = new List<MapTile>();
            List<MapTile> tmpCheck = new List<MapTile>();
            List<MapTile> alreadyChecked = new List<MapTile>();
            List<MapTile> tilesWithEnemy = new List<MapTile>();
            check.Add(tile);
            if (tile.isWalkable)
            {
                if(!alreadyChecked.Contains(tile)) alreadyChecked.Add(tile);
            }
            if(range == 0) return tilesWithEnemy;
        
            while (range > 0)
            {
                foreach (var uncheckedNode in check)
                {
                    foreach (var nTile in uncheckedNode.neighbours)
                    {
                        if(alreadyChecked.Contains(nTile)) {continue;}
                        if (nTile != null)
                        {
                            if(nTile.unitOnTile != null)
                            {
                                if (nTile.unitOnTile.GetComponent<Enemy>() != null)
                                {
                                    if(!tilesWithEnemy.Contains(nTile))
                                    {
                                        tilesWithEnemy.Add(nTile);

                                    }
                                }

                            }       
                            if(!alreadyChecked.Contains(nTile)) {tmpCheck.Add(nTile);}
                    
                    
                        }
                
                    }
                    alreadyChecked.Add(uncheckedNode);
                }

                foreach (var tmpTile in tmpCheck)
                {
                    check.Add(tmpTile);
                }
                range--;
            
            }

            return tilesWithEnemy;
        }
    
    
        public static bool CheckPlayerInRange(int range, MapTile tile)
        {
            return GetPlayerInRange(range, tile).Count > 0;
        }

        public static List<MapTile> GetPlayerInRange(int range, MapTile tile)
        {
            List<MapTile> check = new List<MapTile>();
            List<MapTile> tmpCheck = new List<MapTile>();
            List<MapTile> alreadyChecked = new List<MapTile>();
            List<MapTile> tilesWithEnemy = new List<MapTile>();
            check.Add(tile);
            if (tile.isWalkable)
            {
                if(!alreadyChecked.Contains(tile)) alreadyChecked.Add(tile);
            }
            if(range == 0) return tilesWithEnemy;
        
            while (range > 0)
            {
                foreach (var uncheckedNode in check)
                {
                    foreach (var nTile in uncheckedNode.neighbours)
                    {
                        if(alreadyChecked.Contains(nTile)) {continue;}
                        if (nTile != null)
                        {
                            if(nTile.unitOnTile != null)
                            {
                                if (nTile.unitOnTile.GetComponent<PlayerController>() != null)
                                {
                                    if(!tilesWithEnemy.Contains(nTile))
                                    {
                                        tilesWithEnemy.Add(nTile);

                                    }
                                }

                            }       
                            if(!alreadyChecked.Contains(nTile)) {tmpCheck.Add(nTile);}
                    
                    
                        }
                
                    }
                    alreadyChecked.Add(uncheckedNode);
                }

                foreach (var tmpTile in tmpCheck)
                {
                    check.Add(tmpTile);
                }
                range--;
            
            }

            return tilesWithEnemy;
        }

        public static List<MapTile> GetSlashTargets(MapTile target, int range)
        {
            List<MapTile> returnList = new List<MapTile>();
            returnList.Add(target);
            foreach (var neighbour in target.neighbours)
            {
                if (neighbour == null) continue;
                if (neighbour.NextToPlayer())
                {
                    returnList.Add(neighbour);
                }
            }
        
            return returnList;
        }
    }
}
