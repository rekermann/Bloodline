
using Tiles;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    public MapTile tileStandingOn;

    public delegate void MovementEvent();
    public event MovementEvent OnMove = () => {};


    public void HighlightMove(int move)
    {
        TileManager.Instance.HighlightTilesInRange(tileStandingOn, move);
        UiManager.Instance.mouseController.OnClickedObject += Move;
        UiManager.Instance.DisableAction();
    }

    public void Move(GameObject obj)
    {
        MapTile tile = obj.GetComponent<MapTile>();
        if(tile ==  null) return;
        if(!tile.highlighted) return;
        if (tileStandingOn != null) tileStandingOn.unitOnTile = null;
        tile.unitOnTile = gameObject;
        tileStandingOn = tile;
        transform.position = tile.gameObject.transform.position;
        TileManager.Instance.UnhighlightTiles();
        UiManager.Instance.mouseController.OnClickedObject -= Move;
        UiManager.Instance.EnableAction();
        OnMove();
    }
    
    
}
