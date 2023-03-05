using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck
{
    private List<Card> cards = new List<Card>();

    public void MakeCards(int deckSize)
    {
        cards.Clear();

        for (int i = 0; i < deckSize / 2; i++)
        {
            Sprite faceSprite = Game.S.LogosCardFacePack.ElementAt(i);
            for (int j = 0; j < 2; j++)
            {
                Card newCard = Object.Instantiate<GameObject>(Game.S.CardPrefab).GetComponent<Card>();
                newCard.Face.sprite = faceSprite;
                newCard.Back.sprite = Game.S.LogoCardBack;
                newCard.Label = faceSprite.name;
                newCard.transform.position = new Vector3(i * 2, 0, 0);
                cards.Add(newCard);
            }
        }
    }

    public void PlaceCards()
    {
        float startX = -2f;
        float startY = 2f;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                cards[i * 4 + j].transform.position = new Vector3(startX + 1.33f * j, startY - 1.33f * i);
            }
        }
    }

    public void ShuffleCards()
    {
        var tmpList = new List<Card>();
        while(cards.Count != 0)
        {
            int n = Random.Range(0, cards.Count);
            tmpList.Add(cards[n]);
            cards.RemoveAt(n);
        }
        cards = tmpList;
    }

    public void MakeCardsFaceUp()
    {
        foreach (Card card in cards)
            card.MakeFaceUp();
    }

    public void MakeCardsFaceDown()
    {
        foreach (Card card in cards)
            card.MakeFaceDown();
    }

    public int CountCardsFaceUp()
    {
        int count = 0;
        foreach (Card card in cards)
            if (card.IsFaceUp)
                count++;
        return count;

    }
}
