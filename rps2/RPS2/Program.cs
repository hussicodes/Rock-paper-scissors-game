using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPS2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RpsGame();
        }

        static void RpsGame()
        {
            
            bool playAgain;

            do // ydre loop
            {
                int scorePlayer = 0, scoreCPU = 0;

                while (scorePlayer < 3 && scoreCPU < 3)
                {
                    string[] moves = { "STEN", "SAKS", "PAPIR" };

                    string inputPlayer = GetPlayerInput(moves);
                    string inputCPU = GetRandomMove(moves);

                    DetermineWinner(inputPlayer, inputCPU, ref scorePlayer, ref scoreCPU);

                    Console.WriteLine($"\n\nSCORES:\tPLAYER: {scorePlayer}\tCPU: {scoreCPU}");
                }

                DisplayWinner(scorePlayer, scoreCPU);

                playAgain = AskToPlayAgain();
                Console.Clear();

            } while (playAgain);
        }

        static string GetPlayerInput(string[] moves)
        {
            string inputPlayer;
            do
            {
                Console.WriteLine($"VÆLG MELLEM {string.Join(", ", moves)}:");
                inputPlayer = Console.ReadLine().ToUpper();

                if (!moves.Contains(inputPlayer))
                {
                    Console.WriteLine("Ugyldigt input. Prøv igen.");
                }

            } while (!moves.Contains(inputPlayer));

            return inputPlayer;
        }

        static string GetRandomMove(string[] moves)
        {
            Random rnd = new Random();
            return moves[rnd.Next(moves.Length)];
        }

        static void DetermineWinner(string inputPlayer, string inputCPU, ref int scorePlayer, ref int scoreCPU)
        {
            Console.WriteLine($"\nPlayer vælger {inputPlayer}");
            Console.WriteLine($"Computeren Vælger {inputCPU}");

            switch (inputCPU) // Switchen tjekker for hvad spilleren og CPU'en vælger 
            {
                case "STEN":
                    switch (inputPlayer)
                    {
                        case "STEN":
                            Console.WriteLine("UAFGJORT!!\n\n");
                            break;
                        case "SAKS":
                            Console.WriteLine("COMPUTEREN VANDT!!\n\n");
                            scoreCPU++;
                            break;
                        case "PAPIR":
                            Console.WriteLine("PLAYER VINDER!!\n\n");
                            scorePlayer++;
                            break;
                        default:
                            Console.WriteLine("Ugyldigt input");
                            break;
                    }
                    break;

                case "SAKS":
                    switch (inputPlayer)
                    {
                        case "STEN":
                            Console.WriteLine("PLAYER VINDER!!\n\n");
                            scorePlayer++;
                            break;
                        case "SAKS":
                            Console.WriteLine("UAFGJORT!!\n\n");
                            break;
                        case "PAPIR":
                            Console.WriteLine("COMPUTEREN VANDT!!\n\n");
                            scoreCPU++;
                            break;
                        default:
                            Console.WriteLine("Ugyldigt input");
                            break;
                    }
                    break;

                case "PAPIR":
                    switch (inputPlayer)
                    {
                        case "STEN":
                            Console.WriteLine("COMPUTEREN VANDT!!\n\n");
                            scoreCPU++;
                            break;
                        case "SAKS":
                            Console.WriteLine("PLAYER VINDER!!\n\n");
                            scorePlayer++;
                            break;
                        case "PAPIR":
                            Console.WriteLine("UAFGJORT!!\n\n");
                            break;
                        default:
                            Console.WriteLine("Ugyldigt input");
                            break;
                    }
                    break;

                default:
                    Console.WriteLine("UGYLDIGT INPUT");
                    break;
            }
        }


        static void DisplayWinner(int scorePlayer, int scoreCPU) // Metoden tjekker om scorePlayer eller scoreCPU har noget 3 point
        {
            if (scorePlayer == 3)
            {
                Console.WriteLine("Spilleren har vundet!!");
            }
            else if (scoreCPU == 3)
            {
                Console.WriteLine("Computeren har vundet!!");
            }
        }

        static bool AskToPlayAgain()
        {
            Console.WriteLine("Vil du gerne spille igen? (ja/nej)");

            // En løkke der køre indtil at inputttet svarer til 'ja' eller 'nej' ellers får man en fejl besked
            string loop;
            do
            {
                // Alt input bliver til uppercase
                loop = Console.ReadLine().ToLower();

                // Starter spillet igen
                if (loop == "ja")
                {
                    return true;
                }

                // Lukker programmet
                else if (loop == "nej")
                {
                    return false;
                }

                else
                {
                    Console.WriteLine("UGYLDIGT INPUT - Vil du gerne spille igen? (ja/nej)");
                }
                
            } while (loop != "ja" && loop != "nej"); // kør kun når ja og nej ikke skrives

            return true;
        }

    }
}
