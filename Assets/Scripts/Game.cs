using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    static private Game singleton;

    [Header("Sprites")]
    [SerializeField]
    private Sprite[] logosCardFacePack;
    [SerializeField]
    private Sprite logoCardBack;

    [Header("Prefabs")]
    [SerializeField]
    private GameObject cardPrefab;

    [Header("Game settings")]
    [SerializeField]
    private int deckSize = 16;

    private Deck deck;

    private Card prevClickedCard;
    private Card curClickedCard;

    private bool isInputBlocked;

    private float restartTime = 3f;
    private const float mismatchedCardsShowUpTime = 2f;

    static public Game S => singleton;

    public IReadOnlyCollection<Sprite> LogosCardFacePack => Array.AsReadOnly<Sprite>(logosCardFacePack);

    public Sprite LogoCardBack => logoCardBack;

    public GameObject CardPrefab => cardPrefab;

    public bool IsInputBlocked => isInputBlocked;

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else
            Debug.Log("Tried to align another instance of class Game!");
    }

    private void Start()
    {
        deck = new Deck();
        deck.MakeCards(deckSize);
        deck.ShuffleCards();
        deck.PlaceCards();
    }

    public void CardClicked(Card card)
    {
        if (curClickedCard == null)
        {
            curClickedCard = card;
            return;
        }

        prevClickedCard = curClickedCard;
        curClickedCard = card;

        //Cards hold the same pictures
        if(curClickedCard.Label == prevClickedCard.Label)
        {
            curClickedCard = null;
            prevClickedCard = null;
            if (deck.CountCardsFaceUp() == deckSize)
                Invoke("Restart", restartTime);
        }
        else
        {
            isInputBlocked = true; //To prevent opening another cards, while animation happens
            Invoke("RotateMismatchedCards", mismatchedCardsShowUpTime);
        }
    }

    private void RotateMismatchedCards()
    {
        curClickedCard.MakeFaceDown();
        prevClickedCard.MakeFaceDown();
        curClickedCard = null;
        prevClickedCard = null;
        isInputBlocked = false;
    }

    private void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
