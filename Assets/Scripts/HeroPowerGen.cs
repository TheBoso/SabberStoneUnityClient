using SabberStoneCore.Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeroPowerGen : AnimationGen
{
    private TextMeshProUGUI mana;
    [SerializeField] private GameObject _playable;

    public override void UpdateEntity(EntityExt entity)
    {
        base.UpdateEntity(entity);

        var front = transform.Find("Front");
        var frame = front.Find("Frame");
        mana = frame.Find("Mana").GetComponent<TextMeshProUGUI>();
        mana.text = entity.Tags[GameTag.COST].ToString();

        var exhausted = front.Find("Exhausted");
        bool isExhausted = entity.Tags.ContainsKey(GameTag.EXHAUSTED) && entity.Tags[GameTag.EXHAUSTED] == 1;
        exhausted.gameObject.SetActive(isExhausted);
        frame.gameObject.SetActive(isExhausted == false);
        if (isExhausted == true)
        {
            AudioSource.PlayClipAtPoint(GameSettings.Instance.HeroPowerFlipOff, Vector3.zero);
        }
        else
        {
            AudioSource.PlayClipAtPoint(GameSettings.Instance.HeroPowerFlipOn, Vector3.zero);

        }
      
    }

    public override void Update()
    {
        base.Update();
        var heroPower = PowerInterpreter.instance.Game.Player1.Hero.HeroPower;
        if (heroPower.Id == this._entityExt.Id)
        {
            _playable.SetActive(heroPower.IsPlayable);
        }
    }


    internal void Generate(EntityExt entity)
    {
        var front = transform.Find("Front");
        var artMask = front.Find("ArtMask");
        artMask.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/weapon_mask");

        var art = artMask.Find("Art");

        if (TexturesUtil.GetArtFromResource(entity.CardId, out Texture2D artTexture))
        {
            art.GetComponent<Image>().sprite = Sprite.Create(artTexture, new Rect(0, 0, artTexture.width, artTexture.height), new Vector2(0, 0));
        }
        else
        {
            StartCoroutine(TexturesUtil.GetTexture(entity.CardId, art));
        }

        UpdateEntity(entity);
    }
}
