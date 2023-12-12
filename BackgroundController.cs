using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Sprite[] backs, middles, middlebacks, fronts;
    public SpriteRenderer[] backRenderer, middlesRenderers,
        middleBacksRenderers, frontRenderers;

    private void Start()
    {
        RandomizeBackground();
    }

    public void RandomizeBackground()
    {
        ChooseGraphicForRenderers(backRenderer, backs);
        ChooseGraphicForRenderers(middlesRenderers, middles);
        ChooseGraphicForRenderers(middleBacksRenderers, middlebacks);
        ChooseGraphicForRenderers(frontRenderers, fronts);
    }

    private void ChooseGraphicForRenderers(SpriteRenderer[] spriteRenderers, Sprite[] sprites)
    {
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
    }
}


