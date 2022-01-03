using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    private List<HealthContainer> hearts = new List<HealthContainer>();
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            hearts.Add(new HealthContainer(transform.GetChild(i).GetComponent<Image>(), true));
        }
    }


    public void TakeDamage(int damage)
    {
        
        if (damage <= 0) return;
        foreach (var heart in hearts)
        {
            if (heart.Full)
            {
                heart.Full = false;
                heart.Image.color = Color.black;
                damage--;
            }

            if (damage == 0) break;
        }
    }

    public void HealDamage(int heal)
    {
        if (heal <= 0) return;
        for (int i = hearts.Count - 1; i >= 0; i--)
        {
            if (!hearts[i].Full)
            {
                hearts[i].Full = true;
                hearts[i].Image.color = Color.white;
                heal--;
            }
            if (heal == 0) break;
        }

    }
    
}

public class HealthContainer
{
    public Image Image;
    public bool Full;

    public HealthContainer(Image image, bool full)
    {
        this.Image = image;
        this.Full = full;
    }
}
