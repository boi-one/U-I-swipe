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
    public int[] paramaterValues = new int[4] { 5, 5, 5, 5 };
    public SpriteRenderer[] impact = new SpriteRenderer[4];
    public TMP_Text[] detailedImpact = new TMP_Text[4]; //TODO: ALS het positief is maak het + als de waarde negatief is maak het -, als je niks doet maak het ' '. voeg ook een swipe sound to als je swiped!
    public Bar[] parameterValueBar = new Bar[4];
    public TMP_Text Stelling;
    public SpriteRenderer profile;
    public TMP_Text Name;
}

public class CardManager : MonoBehaviour
{
    public static CardManager instance;
    public CardObject card = new CardObject();
    public Deck cardDeck = null;
    public Card currentCard = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;

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
        Debug.Log("cards available " + cardDeck.cards.Length);
        int index = UnityEngine.Random.Range(0, cardDeck.cards.Length);
        if (cardDeck.cards.Length > 0) currentCard = cardDeck.cards[index];
        else Debug.Log("no cards available in deck");
        SetCardElements(currentCard);
        List<Card> temp = cardDeck.cards.ToList();
        temp.RemoveAt(index);
        cardDeck.cards = temp.ToArray();
    }
}
