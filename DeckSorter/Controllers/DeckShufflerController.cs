using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using DeckSorter.DeckCards;
using DeckSorter.DeckShufflers;


namespace DeckSorter.Controllers
{
    [ApiController]
    [Route("api")]
    public class DeckShufflerController : ControllerBase
    {
        [HttpPost]
        [Route("create/{nameDeck},{deckLength}")]
        public IActionResult CreateDeck(string nameDeck, int deckLength)
        {
            Deck deck = new(deckLength);
            Decks_[nameDeck] = deck;
            return Ok("Created deck by name = " + nameDeck + ", and length = " + deckLength + ". Create successfully!");
        }

        [HttpDelete]
        [Route("remove/{nameDeck}")]
        public IActionResult RemoveDeck(string nameDeck)
        {
            Deck removeDeck;
            if (Decks_.ContainsKey(nameDeck))
            { 
                Decks_.TryRemove(nameDeck, out removeDeck);
                return Ok("Removed deck by name: " + nameDeck + ",\ndeck: " + removeDeck.ToString());
            }
            else
                return NotFound();
        }

        [HttpPost]
        [Route("get_all")]
        public IActionResult GetAll() 
        {
            string getAllDecks = "";
            int deckNumber = 0; 
            foreach (var key in Decks_.Keys) 
            {
                getAllDecks += "Deck " + (++deckNumber).ToString() + " = " + key + "\n";
            }
            return Ok(getAllDecks);
        }

        [HttpPost]
        [Route("change_shuffle_method")] 
        public IActionResult ChangeShuffleMethod(string nameMethod)
        {
            if (nameMethod == "hand")
            {
                Shuffler_ = new HandShuffler();
            }
            else if (nameMethod == "quick")
            {
                Shuffler_ = new QuickShuffler();
            }
            else
            {
                return NotFound("You can use method name 'hand' or 'quick' only");
            }
            return Ok("Method name = " + nameMethod);
        }

        [HttpPut]
        [Route("shuffle/{nameDeck}")]
        public IActionResult Shuffle(string nameDeck)
        {
            if (Decks_.ContainsKey(nameDeck))
            {
                Shuffler_.ShuffleDeck(Decks_[nameDeck]);
                return Ok(Decks_[nameDeck].ToString());
            }
            else
                return NotFound();
        }

        [HttpGet]
        [Route("get/{nameDeck}")]
        public IActionResult GetDeck(string nameDeck)
        {
            if(Decks_.ContainsKey(nameDeck))
                return Ok(Decks_[nameDeck].ToString());
            else 
                return NotFound();
        }

        public DeckShufflerController()
        {
            if (Shuffler_ == null)
            {
                string nameMethod = "";
                try
                {
                    StreamReader file = new StreamReader("config/.deckconf");
                    nameMethod = file.ReadLine();
                    file.Close();
                }catch(Exception e)
                {
                    Console.WriteLine("Exception" + e.Message);
                }
                if (nameMethod is "hand" or "quick")
                    ChangeShuffleMethod(nameMethod);
                else
                    throw new ArgumentException("Method name is incorrect"); 
            }
        } 
        private static IDecksShuffler Shuffler_;
        private static ConcurrentDictionary<string, Deck> Decks_ = new();
    }
}