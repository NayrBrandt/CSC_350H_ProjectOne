using System;
using System.Collections.Generic;

namespace csc350_homework2
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            deck.Shuffle();

            List<Card> cardStack = new List<Card>();

            // Draw 9 Cards
            while (cardStack.Count < 9)
                cardStack.Add(deck.TakeTopCard());

            while (ListHasPairAddsToEleven(cardStack) || ListHasKQJ(cardStack)) {
                List<Card> selectedCards = new List<Card>();


                do
                {
                    Console.Clear();
                    Console.Write("Select two cards that add up to 11." +
                                  "\nYou can select a card by pressing the number on the left.\n\n");

                    // Print Cards
                    PrintCards(cardStack);

                    if (selectedCards.Count > 0)
                    {
                        Console.Write("\n\nCurrent Selected Cards:\n");
                        PrintCards(selectedCards);
                    }

                    ConsoleKeyInfo input = Console.ReadKey();
                    ClearLine(); // Clear the user input

                    if (input.KeyChar < 48 || input.KeyChar > 57 - 10 + cardStack.Count)
                    {
                        Console.Write("Invalid Key! Press any key to continue...");
                        Console.ReadKey();
                    }
                    else
                    {
                        int selectedCardIndex = (int)input.KeyChar - 48;

                        selectedCards.Add(cardStack[selectedCardIndex]);
                        cardStack.RemoveAt(selectedCardIndex);
                    }

                    if (selectedCards.Count == 2) {
                        if (ListHasPairAddsToEleven(selectedCards))
                            break;

                        if (!listHasRoyalty(selectedCards))
                            break;
                    }

                } while (selectedCards.Count != 3);

                // Print the second selected card in our list
                Console.WriteLine("["+ (selectedCards.Count - 1) +"]   " + selectedCards[selectedCards.Count - 1].Rank + " of " + selectedCards[selectedCards.Count - 1].Suit);

                if (ListHasPairAddsToEleven(selectedCards) || ListHasKQJ(selectedCards))
                {
                    Console.WriteLine("\nGreat!");
                    AwaitKeyPress();
                }
                else
                {
                    // Didn't add up to 10. Add the cards back to the cardStack and prompt the user
                    Console.WriteLine("\nHmm, that's not a valid pair.");
                    cardStack.AddRange(selectedCards);
                    AwaitKeyPress();
                }

                // Draw cards if our cardStack doesn't have 9, but only if the deck has cards to give
                while (cardStack.Count < 9 && !deck.Empty)
                    cardStack.Add(deck.TakeTopCard());

                Console.Clear();

            };

            Console.WriteLine("Hmm, our hand does not contain any pairs that add up to 11.");

            Console.WriteLine("\nOur ending hand");
            PrintCards(cardStack);

            Console.WriteLine("\n\n" + (deck.Count()) + " cards remaining in the Deck");
            //deck.Print();
        }
        
        private static void AwaitKeyPress()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static bool SelectedCardsAddUpToEleven(List<Card> selectedCards)
        {
            int sum = 0;
            foreach(Card card in selectedCards)
            {
                sum += (int)System.Enum.Parse(typeof(Rank), card.Rank) + 1;
            }

            if (sum == 11)
                return true;
            return false;
        }

        private static void PrintCards(List<Card> cardStack)
        {
            for(int i = 0; i < cardStack.Count; i++)
                Console.WriteLine("[" + i + "]  " + cardStack[i].Rank + " of " + cardStack[i].Suit);
        }

        private static bool ListHasPairAddsToEleven(List<Card> cardStack)
        {
            // Loop through each element checking to see if any combination
            // adds up to 10 
            if(cardStack.Count == 2) {
                int firstElementVal = (int)System.Enum.Parse(typeof(Rank), cardStack[0].Rank) + 1;
                int sum = firstElementVal + (int)System.Enum.Parse(typeof(Rank), cardStack[1].Rank) + 1;

                if (sum == 11)
                    return true;
            }
            else {
                for (int i = 0; i < cardStack.Count - 1; i++) {
                    int firstElementVal = (int)System.Enum.Parse(typeof(Rank), cardStack[i].Rank) + 1;
                    for (int j = i + 1; j < cardStack.Count; j++) {
                        int sum = firstElementVal + (int)System.Enum.Parse(typeof(Rank), cardStack[j].Rank) + 1;

                        if (sum == 11)
                            return true;
                    }
                }
            }

            return false;
        }

        private static bool ListHasKQJ(List<Card> cardStack) {
            bool hasKing = false;
            bool hasQueen = false;
            bool hasJack = false;
            for (int i = 0; i < cardStack.Count; i++) {
                if (cardStack[i].Rank == Rank.King.ToString())
                    hasKing = true;
                if (cardStack[i].Rank == Rank.Queen.ToString())
                    hasQueen = true;
                if (cardStack[i].Rank == Rank.Jack.ToString())
                    hasJack = true;
            }

            if (hasKing && hasQueen && hasJack)
                return true;
            return false;
        }

        private static bool listHasRoyalty(List<Card> selectedCards) {
            for(int i = 0; i < selectedCards.Count; i++) {
                if (selectedCards[i].Rank == Rank.King.ToString())
                    return true;
                if (selectedCards[i].Rank == Rank.Queen.ToString())
                    return true;
                if (selectedCards[i].Rank == Rank.Jack.ToString())
                    return true;
            }
            return false;
        }

        private static void ClearLine()
        {
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, Console.CursorTop);
        }
    }
}
