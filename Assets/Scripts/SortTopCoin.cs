using UnityEngine;

public class SortTopCoin : MonoBehaviour
{
    private int sortingOrder = 0;
    private SpriteRenderer sprite;

    private void Awake()
    {

    }

    public void SortCoinToTop(SpriteRenderer spriteDragCoin)
    {
        sprite = spriteDragCoin;

        if (sprite)
        {
            sprite.sortingOrder = sortingOrder;
        }
    }
}
