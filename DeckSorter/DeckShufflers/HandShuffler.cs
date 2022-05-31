using DeckSorter.DeckCards;
using DeckSorter.CardValue;

namespace DeckSorter.DeckShufflers
{
    public class HandShuffler : IDecksShuffler
    {
        void IDecksShuffler.ShuffleDeck(Deck deck)
        {
            int deckLength = deck.basedDeck.Length;
            Random randomValue = new();
            int leftBorder = deckLength / 4;
            int rightBorder = deckLength - leftBorder;
            int shufflersCount = 30;
            while(shufflersCount > 0)
            {
                int indexSeparation = randomValue.Next(leftBorder, rightBorder);
                Deck tempArrayLeft = new(indexSeparation);
                for (int i = 0; i < indexSeparation; ++i)
                    tempArrayLeft.basedDeck[i] = deck.basedDeck[i];
                int countTempIndex = 0;
                int randomIndexInsert = randomValue.Next(0, deckLength - indexSeparation);
                for (int i = 0; i < deckLength; ++i)
                {
                    if (i < indexSeparation)
                        deck.basedDeck[i] = deck.basedDeck[randomIndexInsert + i];
                    else if(i >= randomIndexInsert && i < randomIndexInsert + indexSeparation)
                        deck.basedDeck[i] = tempArrayLeft.basedDeck[countTempIndex++];
                    else
                        continue;
                }
                shufflersCount--;
            }
            
        }
    }
}