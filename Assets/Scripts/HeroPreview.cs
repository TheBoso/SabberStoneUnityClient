using SabberStoneCore.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroPreview : MonoBehaviour
{
    

    [SerializeField]
    private Text id;



    [SerializeField]
    
    
    private TextMeshProUGUI health;
    [SerializeField]
    private Transform art;






    public void UpdateHero()
    {
        string id = this.id.text.ToString();

        health.text = Cards.FromId(id).Health.ToString();
        if (TexturesUtil.GetArtFromResource(id, out Texture2D artTexture))
        {
            art.GetComponent<Image>().sprite = Sprite.Create(artTexture, new Rect(0, 0, artTexture.width, artTexture.height), new Vector2(0, 0));
        }
        else
        {
            StartCoroutine(TexturesUtil.GetTexture(id, art));
        }




    }
    
}
