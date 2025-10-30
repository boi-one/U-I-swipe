using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;

[Serializable]
public class Parameter
{
    public string parameterTitleData = "";
    public string parameterIconData = "";
    public int leftValueMin = 0;
    public int leftValueMax = -1;
    public int rightValueMin = 0;
    public int rightValueMax = -1;
}

[Serializable]
public class Card
{
    public int id = 0;
    public Parameter[] parameters = new Parameter[4];
    public string description = "";
    public string profile = "";
    public string name = "";
}
[Serializable]
public class Deck
{
    public Card[] cards;
};

[Serializable]
public class CardObject
{
    public SpriteRenderer[] parameters = new SpriteRenderer[4];
    public TMP_Text Stelling;
    public SpriteRenderer profile;
    public TMP_Text Name;
}

public class CardManager : MonoBehaviour
{
    public static CardManager cardManager;
    public CardObject card = new CardObject();
    private Deck cardDeck = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cardManager = this;

        LoadJson();
        SetCard();
        Debug.Log(cardDeck.cards.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadJson()
    {
        string data = Application.dataPath + "/Decks/deck.json";

        cardDeck = JsonUtility.FromJson<Deck>(File.ReadAllText(data));
    }

    void SetCardElements(Card currentCard)
    {
        for (int i = 0; i < currentCard.parameters.Length; i++)
        {
            string noExtention = Path.GetFileNameWithoutExtension(currentCard.parameters[i].parameterIconData);
            card.parameters[i].sprite = Resources.Load<Sprite>($"Sprites/icons/{noExtention}");
        }

        card.Stelling.text = currentCard.description;
        card.Name.text = currentCard.name;
        card.profile.sprite = Resources.Load<Sprite>($"Sprites/profiles/{Path.GetFileNameWithoutExtension(currentCard.profile)}");
    }

    public void SetCard()
    {
        int index = UnityEngine.Random.Range(0, cardDeck.cards.Length);
        SetCardElements(cardDeck.cards[index]);
        List<Card> temp = cardDeck.cards.ToList();
        temp.RemoveAt(index);
        cardDeck.cards = temp.ToArray();
    }
}
