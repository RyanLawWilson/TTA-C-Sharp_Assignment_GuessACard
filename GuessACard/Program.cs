using System;

namespace GuessACard
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] deck = initializeDeck();

            // Introduction
            awesomeWrite("Welcome to Guess a card!\n");
            System.Threading.Thread.Sleep(800);
            awesomeWrite("The objective is to guess the correct card on the top of the deck\n");
            System.Threading.Thread.Sleep(800);
            awesomeWrite("It's multiple choice.  You have 5 guesses!  type ");
            awesomeWrite("exit", 100);
            System.Threading.Thread.Sleep(500);
            awesomeWrite(" to stop playing\n");
            System.Threading.Thread.Sleep(800);
            awesomeWrite("Note that your answer is case sensitive", 50);
            System.Threading.Thread.Sleep(500);

            short life = 5;         // The number of times the user can guess before they lose.
            bool playGame = true;   // Start the game.  Activates game loop.

            while(playGame)
            {
                string topCard = getTopCard(deck);
                string[] choices = getCardChoices(deck, topCard);
                awesomeWrite("Answer: " + topCard + "\n");

                bool keepPlaying = true;
                do
                {
                    Console.WriteLine();    // spacing

                    // Display your choices
                    for (int i = 1; i < choices.Length + 1; i++)
                    {
                        // Make a new line after every 4 cards
                        if (i % 4 == 0)
                        {
                            awesomeWrite("" + choices[i - 1] + "\n", 5);
                        }
                        else
                        {
                            awesomeWrite(choices[i - 1] + " | ", 5);
                        }
                        
                    }

                    awesomeWrite("\nYour choice: ", 50);
                    string choice = Console.ReadLine();

                    if (choice == topCard)
                    {
                        life--;
                        awesomeWrite("Your answer is", 100);
                        displayDotDotDot();
                        awesomeWrite("C O R R E C T!  ");
                        System.Threading.Thread.Sleep(1000);
                        keepPlaying = false;
                    }
                    else if (choice == "exit")
                    {
                        Console.WriteLine("\nThanks for playing!");
                        System.Threading.Thread.Sleep(1000);
                        System.Environment.Exit(0);
                    }
                    else
                    {
                        life--;
                        awesomeWrite("Your answer is", 100);
                        displayDotDotDot();
                        Console.Write("INCORRECT!");
                        // Say different things based on the amount of life the player has
                        switch (life)
                        {
                            case 0:     // Change lives to life if you have 1 life.
                                awesomeWrite(" You have " + life + " lives\n");
                                break;
                            case 1:     // Don't say 'try again' if the player has 0 lives
                                awesomeWrite(" Try Again.  You have " + life + " life\n");
                                break;
                            default:
                                awesomeWrite(" Try Again.  You have " + life + " lives\n");
                                break;
                        }
                    }
                }
                while (life > 0 && keepPlaying);

                // Does the player want to play again?
                awesomeWrite("\nDo you want to play again? (y or n): ");
                string playAgain = Console.ReadLine();

                switch (playAgain)
                {
                    case "y":
                        playGame = true;
                        awesomeWrite("Shuffling deck...", 100);
                        System.Threading.Thread.Sleep(500);
                        break;
                    case "n":
                        playGame = false;
                        break;
                    default:
                        playGame = false;
                        break;
                }  
            }

            Console.WriteLine("\nThanks for playing!");
            Console.Read();
        }

        // Initializes the deck for the game
        private static string[] initializeDeck()
        {
            string[] deckCards = {"Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King"};
            string[] deckSuits = { "Spades", "Clubs", "Hearts", "Diamonds" };

            // The size of the deck is the product of the sizes of the cards and suits.
            string[] deck = new string[deckCards.Length * deckSuits.Length];

            // Add each card variation to the deck.
            int deckPosition = 0;
            foreach (string card in deckCards)
            {
                foreach (string suit in deckSuits)
                {
                    deck[deckPosition] = card + " of " + suit;
                    deckPosition++;
                    // Console.WriteLine(deckPosition);
                }
            }

            return deck;
        }

        // Retrieves a random car from the deck, not the top card.  But the user doesn't need to know that...
        private static string getTopCard(string[] deck)
        {
            Random rand = new Random();
            int randomIndex = rand.Next(0, deck.Length);
            return deck[randomIndex];
        }

        private static string[] getCardChoices(string[] deck, string topCard)
        {
            int numberOfChoices = 10;       // The number of choices you want the player to see.

            Random rand = new Random();
            int topCardIndex = rand.Next(0, 10);    // The location of the top card in the choices array.
            int randomIndex;

            string[] choices = new string[numberOfChoices];

            // If there are more choices than cards.  Set chocies to 10.  Can be removed probably.
            if (numberOfChoices > deck.Length)
            {
                numberOfChoices = 10;
            }

            // Get the choices.
            int cardsCollected = 0;         // The number of cards that have been found
            while (cardsCollected < numberOfChoices)
            {
                randomIndex = rand.Next(0, deck.Length);

                // Add the top card to the choices and skip the rest of the loop.
                if (topCardIndex == cardsCollected)
                {
                    choices[cardsCollected] = topCard;
                    cardsCollected++;
                    continue;
                }

                // If the random card is not the top card, add it.  If it is the top card, skip.
                if (deck[randomIndex] != topCard)
                {
                    choices[cardsCollected] = deck[randomIndex];
                    cardsCollected++;
                }
            }
            
            return choices;
        }

        static void displayDotDotDot()
        {
            Random rand = new Random();

            int numberOfDots = rand.Next(5, 11);

            short baseDelay = 400;
            short maxDelay = 200;

            for (int i = 0; i < numberOfDots; i++)
            {
                Console.Write(".");
                System.Threading.Thread.Sleep(baseDelay + rand.Next(0, maxDelay + 1));
            }
        }

        // A method that types your string letter by letter.
        static void awesomeWrite(string text)
        {
            char[] letters = text.ToCharArray();
            foreach (char letter in letters)
            {
                System.Threading.Thread.Sleep(15);
                Console.Write(letter);
            }
        }

        // Overloaded method.  Allows you to change the speed that characters appear.  Bigger number = slower. 5 < speed < 100
        static void awesomeWrite(string text, int speed)
        {
            if (speed < 5 || speed > 100)
            {
                speed = 15;
            }
            char[] letters = text.ToCharArray();
            foreach (char letter in letters)
            {
                System.Threading.Thread.Sleep(speed);
                Console.Write(letter);
            }
        }
    }
}
