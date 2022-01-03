using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class MouseController : MonoBehaviour
{
    public delegate void Clicked(GameObject obj);
    public event Clicked OnClickedObject = (obj) => {};

    private IAltHoverable _hoveredObj;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && UiManager.Instance.CheckControls())
        {
            CheckClicked();
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            CheckHovered();
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            if(_hoveredObj != null)
                _hoveredObj.AltUnHovered();
        }
    }
    private void CheckClicked()
    {

        Vector2 mousePosition = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hits = Physics2D.OverlapPoint(mousePosition);
        
        if(hits == null) return;
        OnClickedObject(hits.gameObject);
        if (hits.GetComponent<IClickable>() != null)
        {
            hits.GetComponent<IClickable>().Clicked();
        }
    }

    private void CheckHovered()
    {
        Vector2 mousePosition = GameManager.Instance.mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hits = Physics2D.OverlapPoint(mousePosition);

        if (hits == null)
        {
            if(_hoveredObj != null) _hoveredObj.AltUnHovered();
        }
        else if (hits.GetComponent<IAltHoverable>() != null)
        {
            _hoveredObj = hits.GetComponent<IAltHoverable>();
            _hoveredObj.AltHovered();
            return;
        }
        
        hits = Physics2D.OverlapPoint(Input.mousePosition);
        if (hits == null)
        {
            if(_hoveredObj != null) _hoveredObj.AltUnHovered();
            return;
        }
        if (hits.GetComponent<IAltHoverable>() != null)
        {
            _hoveredObj = hits.GetComponent<IAltHoverable>();
            _hoveredObj.AltHovered();
        }
    }
}
