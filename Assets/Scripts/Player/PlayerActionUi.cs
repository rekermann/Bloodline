using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionUi : MonoBehaviour
{
    private List<ActionContainer> hearts = new List<ActionContainer>();
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            hearts.Add(new ActionContainer(transform.GetChild(i).GetComponent<Image>(), true));
        }
    }


    public void LoseAction(int amount)
    {
        if (amount <= 0) return;
        foreach (var actionPoint in hearts)
        {
            if (actionPoint.Full)
            {
                actionPoint.Full = false;
                actionPoint.Image.color = Color.black;
                amount--;
            }

            if (amount == 0) break;
        }
    }

    public void GetAction(int amount)
    {
        if (amount <= 0) return;
        for (int i = hearts.Count - 1; i >= 0; i--)
        {
            if (!hearts[i].Full)
            {
                hearts[i].Full = true;
                hearts[i].Image.color = Color.white;
                amount--;
            }
            if (amount == 0) break;
        }

    }
    
}

public class ActionContainer
{
    public Image Image;
    public bool Full;

    public ActionContainer(Image image, bool full)
    {
        this.Image = image;
        this.Full = full;
    }
}