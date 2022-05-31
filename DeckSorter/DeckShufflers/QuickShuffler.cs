using DeckSorter.DeckCards;

namespace DeckSorter.DeckShufflers
{
    public class QuickShuffler : IDecksShuffler
    {
        void IDecksShuffler.ShuffleDeck(Deck deck)
        {
            int deckLength = deck.basedDeck.Length;
            Random randomValue = new();
            for (int i = 0; i < deckLength; ++i)
            {
                int randomIndex = randomValue.Next(0, deckLength - 1);
                (deck.basedDeck[i], deck.basedDeck[randomIndex]) = (deck.basedDeck[randomIndex], deck.basedDeck[i]);
            }
        }
    }
}
