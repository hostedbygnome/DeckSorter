using DeckSorter.CardValue;

namespace DeckSorter.DeckCards
{
    public class Deck
    {
        static private string[] suits_ = { "♣", "♦", "♥", "♠" };
        static private string[] values36_ = { "A", "6", "7", "8", "9", "10", "J", "Q", "K" };
        static private string[] values52_ = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K" };

        private Card[] basedDeck_;
        public Card[] basedDeck => basedDeck_;
        public Deck(int deckLength)
        {
            basedDeck_ = new Card[deckLength];
            for (int i = 0; i < deckLength; ++i)
                basedDeck_[i] = new Card(i);
        }

        public string ToString()
        {     
            string printDeck = "";
            for (int i = 0; i < basedDeck_.Length; ++i)
            {
                if (i % 4 != 0 && i > 0)
                {
                    if (i == basedDeck_.Length - 1)
                        printDeck += CurrentCard(basedDeck_[i].сardValue, basedDeck_.Length) + ";";
                    else
                        printDeck += CurrentCard(basedDeck_[i].сardValue, basedDeck_.Length) + ",\t";
                }
                else
                    printDeck += "\n" + CurrentCard(basedDeck_[i].сardValue, basedDeck_.Length) + ",\t";
            }    
            return printDeck;
        }

        static private string CurrentCard(int cardValue, int deckLength)
        {
            if (deckLength <= 36)
                return values36_[cardValue / suits_.Length] + " " + suits_[cardValue % suits_.Length];
            else
                return values52_[cardValue / suits_.Length] + " " + suits_[cardValue % suits_.Length];
        }
    }
}
