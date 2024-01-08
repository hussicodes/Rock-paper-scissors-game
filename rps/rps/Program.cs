using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace rps
{
    internal class Program
    {
        static string playername;
        static void Main(string[] args)
        {            
            GameMenu();
        }

        static void GameMenu()
        {
            Console.Clear();
            Console.WriteLine("Vælg et spil du gerne vil spille\nrps af Hussi\nmastermind af Marc\nlab af muddi");
            string chooseInput = Console.ReadLine();
            chooseInput = chooseInput.ToLower();

           

            switch (chooseInput)
            {
                case "rps":
                    RpsGame();
                    break;

                case "mastermind":
                    MasterMind();
                    break;
                case "lab":
                    Lab();
                    break;
                default:
                    Console.WriteLine("Vælg et gyldigt spil");
                    break;
            }
            Console.ReadLine();
        }

        static void RpsGame() 
        {
            // input bliver registret af både CPU og Player
            string inputPlayer, inputCPU;

            // variable for random
            int randomInt;
            
            
            bool playAgain = true;

            // outer loop
            while (playAgain)
            {
                // score bliver gemt på disse variabler
                int scorePlayer = 0;
                int scoreCPU = 0;

                // inner loop der køre spillet igennem og køre kun når begge score har noget under 3 point
                while (scorePlayer < 3 && scoreCPU < 3)
                {
                    Console.WriteLine("VÆLG MELLEM STEN, SAKS OG PAPIR:");
                    inputPlayer = Console.ReadLine();
                    inputPlayer = inputPlayer.ToUpper();


                    // randomizer - vælger mellem 3 forskellige 'outcomes' (1, 2 og 3)
                    Random rnd = new Random();
                    randomInt = rnd.Next(1, 4);

                    // sammenligner det valgte input med cpu'en random generated 
                    switch (randomInt)
                    {
                        case 1:                         
                            
                                inputCPU = "STEN";
                                Console.WriteLine("\nPlayer vælger " + inputPlayer);
                                Console.WriteLine("Computeren Vælger STEN");

                                if (inputPlayer == "STEN")
                                {
                                    Console.WriteLine("UAFGJORT!!\n\n");
                                }
                                else if (inputPlayer == "PAPIR")
                                {
                                    Console.WriteLine("PLAYER VINDER!!\n\n");
                                    scorePlayer++;
                                }
                                else if (inputPlayer == "SAKS")
                                {
                                    Console.WriteLine("COMPUTEREN VANDT!!\n\n");
                                    scoreCPU++;
                                }
                                else
                                {
                                Console.WriteLine("ugyldigt input");
                                }
                            break;
                        case 2:
                            inputCPU = "PAPIR";
                            Console.WriteLine("\nPlayer vælger " + inputPlayer);
                            Console.WriteLine("Computeren Vælger PAPIR");

                            if (inputPlayer == "PAPIR")
                            {
                                Console.WriteLine("UAFGJORT!!\n\n");
                            }
                            else if (inputPlayer == "SAKS")
                            {
                                Console.WriteLine("PLAYER VANDT!!\n\n");
                                scorePlayer++;
                            }
                            else if (inputPlayer == "STEN")
                            {
                                Console.WriteLine("COMPUTEREN VANDT!!\n\n");
                                scoreCPU++;
                            }
                            else
                            {
                                Console.WriteLine("ugyldigt input");
                            }
                            break;
                        case 3:
                            inputCPU = "SAKS";
                            Console.WriteLine("\nPlayer vælger " + inputPlayer);
                            Console.WriteLine("Computeren Vælger SAKS");

                            if (inputPlayer == "SAKS")
                            {
                                Console.WriteLine("UFAGJORT!!\n\n");
                            }
                            else if (inputPlayer == "STEN")
                            {
                                Console.WriteLine("PLAYER VINDER!!\n\n");
                                scorePlayer++;
                            }
                            else if (inputPlayer == "PAPIR")
                            {
                                Console.WriteLine("Computeren VANDT!!\n\n");
                                scoreCPU++;
                            }
                            else
                            {
                                Console.WriteLine("ugyldigt input");
                            }
                            break;
                        default:
                            Console.WriteLine("UGYLDIGT INPUT");
                            break;

                    }

                    Console.WriteLine("\n\nSCORES:\tPLAYER: " + scorePlayer + "\tCPU: " + scoreCPU); // scoren opdateres her
                }

                // her finder vi vinderen der rammer 3 point først
                if (scorePlayer == 3)
                {
                    Console.WriteLine("Spilleren har vundet!!");
                }
                else if (scoreCPU == 3)
                {
                    Console.WriteLine("Computeren har vundet!!");
                }
             
                Console.WriteLine("Vil du gerne spille igen?(ja/nej)");

                // looper tilbage til loopen (playAgain) hvis man gerne vil spille igen og hvis man vælger nej sender den tilbage til menuen
                string loop;
                do
                {
                    loop = Console.ReadLine();
                    if (loop == "ja")
                    {
                        playAgain = true;
                        Console.Clear();
                        break;
                    }
                    else if (loop == "nej")
                    {
                        GameMenu();
                    }
                    else
                    {
                        Console.WriteLine("UGYLDIGT INPUT - Vil du gerne spille igen?(ja/nej)");
                    }
                }
                while (loop != "ja" || loop != "nej");
                

            }
        }

        /// <summary>
        /// Her er så hele spillet
        /// </summary>
        static void MasterMind()
        {
            Console.WriteLine("************** Let's play Master-Mind **************\n");

            string name = GetPlayerName();
            string again;
            bool igenigen = true;


            while (igenigen)
            {
                Play(name);
                Console.WriteLine("Would you like to play again (Y/N)?");
                do
                {
                    again = Console.ReadLine().ToUpper();
                    if (again == "Y")
                    {
                        igenigen = true;
                        Console.Clear();
                        break;
                    }
                    else if (again == "N")
                    {
                        GameMenu();
                    }
                    else
                    {
                        Console.WriteLine("WRONG INPUT - Would you like to play again (Y/N)? ");
                    }
                }
                while (again != "Y" || again != "N");
            }

            
                                                            
        }
        /// <summary>
        /// Her er så spille delen sat sammen
        /// </summary>
        /// <param name="name"></param>
        private static void Play(string name)
        {
            int numberCount = 4;
            int[] PCArray = GenerateRandomNumbers(numberCount);
            Console.Clear();
            Console.WriteLine("Welcome, {0}. Have fun!!\n", name);
            Console.Write("************** Let's play Master-Mind **************\n");
            Console.WriteLine();

            Console.WriteLine("A {0}-digit number has been chosen. Each possible digit may be the number 0 to 9.\n", numberCount);

            int difficulty = GetGameDifficulty();

            bool won = false;
            for (int allowedAttempts = difficulty * numberCount; allowedAttempts > 0 && !won; allowedAttempts--)
            {
                Console.WriteLine("\nEnter your guess ({0} guesses remaining)", allowedAttempts);

                int[] userArray = GetUserGuess(numberCount);

                if (CountHits(PCArray, userArray) == numberCount)
                    won = true;
            }

            if (won)
                Console.WriteLine("You win, {0}!", name);
            else
                Console.WriteLine("Oh no, {0}! You couldn't guess the right number.", name);

            Console.Write("The correct number is: ");

            for (int j = 0; j < numberCount; j++)
                Console.Write(PCArray[j] + " ");
            Console.WriteLine();
        }
        /// <summary>
        /// Her kommer brugers navn fra
        /// </summary>
        /// <returns></returns>
        private static string GetPlayerName()
        {
            Console.Write("Please enter your name: ");
            string name = Console.ReadLine();

            return name;
        }

        /// <summary>
        /// Her kommer sværheds graden fra alt efter hvad bruger vil have det på
        /// </summary>
        /// <returns></returns>
        public static int GetGameDifficulty()
        {
            int difficulty;

            Console.Write("Choose a difficulty level (1=hard, 2=medium, 3=easy): ");
            while (!int.TryParse(Console.ReadLine(), out difficulty) || difficulty < 1 || difficulty > 3)
                Console.WriteLine("Incorrect entry: Please re-enter.");

            return difficulty;
        }
        /// <summary>
        /// Her får jeg det 4 cifret random tal man skal gætte
        /// </summary>
        /// <param name="PCSize"></param>
        /// <returns></returns>
        public static int[] GenerateRandomNumbers(int PCSize)
        {
            int eachNumber;
            int[] randomNumber = new int[PCSize];
            Random rnd = new Random();


            for (int i = 0; i < PCSize; i++)
            {
                eachNumber = rnd.Next(10);
                randomNumber[i] = eachNumber;

            }
            Console.WriteLine();
            return randomNumber;
        }
        /// <summary>
        /// Her kommer brugers gæt
        /// </summary>
        /// <param name="userSize"></param>
        /// <returns></returns>
        public static int[] GetUserGuess(int userSize)
        {
            int[] userGuess = new int[4];
            string guess1;
            int guess1ToInt;
            guess1 = Console.ReadLine();
            // Her gør jeg så den kun tager imod tal
            while (int.TryParse(guess1, out guess1ToInt) == false)
            {
                Console.WriteLine("Please write your guess in numbers:");
                guess1 = Console.ReadLine();
            }
            guess1ToInt = Convert.ToInt32(guess1);
            for (int i = 0; i < guess1.Length; i++)
            {
                guess1ToInt = Convert.ToInt32(guess1[i]);
                // Her har jeg sat -48 fordi jeg ikke kunne finde ude af
                // hvorfor den startet med 1 = 49, 2 = 50 osv.
                guess1ToInt -= 48;
                userGuess[i] = guess1ToInt;
            }


            Console.WriteLine();
            Console.Write("Your guess: ");
            for (int i = 0; i < userSize; i++)
            {
                Console.Write(userGuess[i] + " ");
            }
            Console.WriteLine();
            return userGuess;
        }
        /// <summary>
        /// Her tjekker jeg så de to Arrays op mod hindanden
        /// </summary>
        /// <param name="PCArray"></param>
        /// <param name="userArray"></param>
        /// <returns></returns>
        public static int CountHits(int[] PCArray, int[] userArray)
        {
            int hits = 0;

            for (int i = 0; i < PCArray.Length; i++)
            {
                if (PCArray[i] == userArray[i])
                    hits++;
            }

            Console.WriteLine("Results: {0} Hit(s), {1} Miss(es)", hits, PCArray.Length - hits);
            return hits;
        }
        static void Lab()
        {
            /*Starter lige ud med at definere nogle stats der skal bruges senere, jeg satte dem tidligt fordi i starten havde jeg 100+ linje lange if sætninger
                 Og jeg valgte at beholde dem i starten, da det stadig ikke er en dårlig applikation af koden.*/
            int playerHealth = 10;
            int playerDamage = 1;
            int playerArmor = 0;

            //Remnants of old code, simpler times, before functions.
            /*
            int BeetleHealth = 3;
            int BeetleDamage = 1;
            */






            Console.WriteLine("Hello, Welcome to the Labyrinth");
            Console.WriteLine("Your friends have dared you to enter the Labyrinth, and in a moment of poor judgement, you agreed.");
            Console.WriteLine("Supposedly there is an awesome Sword that the builder of the Labyrinth put in there");
            Console.WriteLine("You and your friends want the sword, because of course you do");
            Console.WriteLine("Please tell us your name, so you may be remembered");
            Console.Write("< ");
            string playername = Console.ReadLine();
            if (playername == "")
                Console.WriteLine("You will remain anonymous");
            else
                Console.WriteLine("You will be remembered as " + playername);
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("Good Luck, are you ready " + playername + "?");

            //Jeg skulle bruge rigtig lang tid på at finde ud af hvor man skulle sætte funktionen for at alt giver mening
            //Det endte ud med at blive her.



            bool validInput = false;

            while (!validInput)
            {
                Console.WriteLine("It's time to answer your friend, will you be courageous or cowardly?");
                Console.WriteLine("Please write 'courage' for Courageous or 'coward' for Cowardly");
                string firstchoice = Console.ReadLine();

                if (firstchoice.ToLower() == "courage" || firstchoice == "coward")
                {
                    validInput = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("That isn't an option. Please enter 'courage' or 'coward'.");
                }


                if (firstchoice == "courage")
                {
                    playerDamage++;
                    Console.Clear();
                    Console.WriteLine("You let them know that you are feeling confident, and stride into the Labyrinth ready to conquer it");
                    Console.WriteLine("Inside the Labyrinth you have to make your first choice, left or right?");
                    Console.Write("< ");
                }

                else if (firstchoice == "coward")

                {
                    {
                        playerArmor++;
                        Console.Clear();
                        Console.WriteLine("You let them know that you are feeling confident, and stride into the Labyrinth ready to conquer it");
                        Console.WriteLine("Inside the Labyrinth you have to make your first choice, left or right?");
                        Console.Write("< ");
                    }


                    string secondchoice = Console.ReadLine();
                    if (secondchoice.ToLower() == "left")
                    {
                        {
                            Console.Clear();
                            Console.WriteLine("You have decided to go left");
                            Console.WriteLine("The walls stretch far, gray and dull, you hear a breeze on the wind");
                            Console.WriteLine("You continue down the path, to find a shield");
                            Console.WriteLine("You equip the shield, hoping that you won't need it");
                            Console.WriteLine("Your Armor has Increased by +1!");
                            playerArmor++;

                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("You continue to the left, the path long and weary, how many turns have you taken now?");
                            Console.WriteLine("It does no good to wonder about things like that, but as you march on.. You hear noises up ahead");
                            Console.WriteLine("As you approach, you see a GIANT BEETLE");
                            Console.WriteLine("The GIANT BEETLE also sees you! And it wants to battle!");
                            Console.ReadKey();
                            CombatBeetle(ref playerHealth, ref playerArmor, ref playerDamage, "Giant Beetle");
                            
                                Console.WriteLine("You Continue the path Left");
                                Console.ReadKey();

                                Console.WriteLine("The Journey continues, and you eventually find yourself faced with a wall");
                                Console.WriteLine("And as you touch the wall, you realise you can walk through it, there you come upon an Altar.");
                                Console.WriteLine("And Upon the Altar is the writing \"you who face the blade, take it if you are prepared for oblivion\" ");
                                Console.WriteLine("As you look closer, you notice this ancient looking sword with a blue glow and hum to it");
                                Console.WriteLine("Do you pick up the sword?");
                                Console.WriteLine("Yes or No");
                                Console.Write("< ");
                            

                            string sword = Console.ReadLine();
                            if (sword.ToLower() == "yes")
                            {
                                Console.WriteLine("You pick up the sword and you feel ancient power rush into you");
                                Console.WriteLine("Man does it feel good");
                                playerDamage = playerDamage + 2;
                                Console.WriteLine("Your damage has increased to " + playerDamage);
                                Console.ReadKey();
                                Console.Clear();
                                Console.WriteLine("You hear quaking in the Labyrinth, the walls are trembling, the ground is shaking");
                                Console.WriteLine("Soon enough you see it, a beast most foul, with horns on its head and foam by the mouth");
                                Console.WriteLine("A Devil born of a Bovine, it must be, but regardless of whatever it is..");
                                Console.WriteLine("You know one thing, it wants your blood");
                                Console.WriteLine("The MINOTAUR wants to KILL you");
                                Console.ReadKey();
                                CombatMinotaur(ref playerHealth, ref playerArmor, ref playerDamage);
                            }
                            else if (sword.ToLower() == "no")
                            {
                                Console.Clear();
                                Console.WriteLine("You decide that this cannot be worth it");
                                Console.WriteLine("And you might be right, you decide to leave the Labyrinth");
                                Console.WriteLine("Your friends will not believe what you tell them, and you live out the rest of your life wondering what could have been");
                                Console.WriteLine("But atleast you still live.");
                                Console.WriteLine("THE END");
                            }
                                

                            




                        }
                    }




                    else if (secondchoice.ToLower() == "right")
                    {
                        {
                            Console.Clear();
                            Console.WriteLine("You have decided to go right");
                            Console.WriteLine("The Walls stretch far, adorned with green vines and thorns. You hear a grunt in the distance");
                            Console.WriteLine("As you apporoach, you see a GIANT BEETLE");
                            Console.WriteLine("The GIANT BEETLE also sees you! And it wants to battle!");
                            Console.ReadKey();
                            CombatBeetle(ref playerHealth, ref playerArmor, ref playerDamage, "Giant Beetle");

                            Console.ReadKey();
                            Console.Clear();
                            Console.WriteLine("The path continues, and before long you manage to find a stick");
                            Console.WriteLine("You pick up the stick, it fits your hands perfectly");
                            playerDamage = playerDamage + 1;
                            Console.WriteLine("Your damage has increased by +1!");
                            Console.ReadKey();
                            Console.Clear();

                            Console.WriteLine("You hear stomping, you feel the ground quaking");
                            Console.WriteLine("You feel uneasy, you feel like something is coming");
                            Console.WriteLine("And you feel that if you will not live to see the end of it");
                            Console.WriteLine("a Brown furred Fiend stands before you, you are in the pressence of the Minotaur");
                            Console.WriteLine("The Minotaur knew you were here, it's here for your blood");
                            Console.WriteLine("The Minotaur Attacks with deadly intent!");
                            Console.ReadKey();
                            Console.Clear();
                            CombatMinotaur(ref playerHealth, ref playerArmor, ref playerDamage);
                            {
                                Console.Clear();
                                Console.WriteLine("You decide that this cannot be worth it");
                                Console.WriteLine("And you might be right, you decide to leave the Labyrinth");
                                Console.WriteLine("Your friends will not believe what you tell them, and you live out the rest of your life wondering what could have been");
                                Console.WriteLine("But atleast you still live.");
                                Console.WriteLine("THE END");
                                
                            }
                            
                            //continue writing about the left path Muddi


                            //Continue writing Coward's path, include Left and Right.


                        }
                    }
                }
            }
            Console.ReadLine();
            GameMenu();
        }

        //Minotaur Fight Function
        static void CombatMinotaur(ref int playerHealth, ref int playerArmor, ref int playerDamage)
        {

            int enemyHealth = 10;
            int enemyDamage = 3;


            bool combatEnd = false;

            while (!combatEnd) //Et loop, combat varer indtil at nogen er død eller nogen flygter
            {
                Console.Clear();
                Console.WriteLine($"The Minotaur, is faster than you and acts first!");
                Console.WriteLine($"The Minotaur attacks!");


                int playerDamageTaken = enemyDamage - playerArmor;
                playerHealth = playerHealth - playerDamageTaken;

                Console.WriteLine($"The Minotaur deals {playerDamageTaken} damage to you.");


                if (playerHealth <= 0)
                {
                    Console.WriteLine($"{playername} has been defeated by the foul Minotaur!");
                    combatEnd = true;
                    break;
                }

                Console.WriteLine($"Minotaur Health: {enemyHealth}");
                Console.WriteLine($"Health: {playerHealth}");
                Console.WriteLine($"Your Armor: {playerArmor} Your Damage: {playerDamage}");

                Console.WriteLine("What will you do now? Type 'attack' or 'flee'");
                Console.Write("< ");
                string playerAction = Console.ReadLine();

                if (playerAction == "attack")
                {

                    int enemyDamageTaken = playerDamage;
                    enemyHealth -= enemyDamageTaken;

                    Console.WriteLine($"You strike out at the Minotaur and deal {enemyDamageTaken} damage.");

                    if (enemyHealth <= 0)
                    {
                        Console.WriteLine($"You have defeated the Minotaur!");
                        combatEnd = true;
                    }
                }
                else if (playerAction == "flee")
                {
                    Console.WriteLine($"You managed to escape from the Minotaur!");
                    combatEnd = false;
                    Console.WriteLine("You failed to Escape the Minotaur");
                }
            }
        }

        //CombatBeetle Funktion, som skal være lige under eller over Minotaur for at de begge virker
        //At opskilde dem for meget ville gøre det mere uoverskueligt.

        static void CombatBeetle(ref int playerHealth, ref int playerArmor, ref int playerDamage, string enemyName)
        {

            int enemyHealth = 3;
            int enemyDamage = 1;

            bool combatEnd = false;

            while (!combatEnd)
            {
                Console.Clear();
                Console.WriteLine($"The {enemyName}, is faster than you and acts first!");
                Console.WriteLine($"The {enemyName} attacks!");


                int playerDamageTaken = enemyDamage - playerArmor;
                playerHealth = playerHealth - playerDamageTaken;

                Console.WriteLine($"{enemyName} deals {playerDamageTaken} damage to you.");


                if (playerHealth <= 0)
                {
                    Console.WriteLine($"{playername} has been defeated by the {enemyName}!");
                    combatEnd = true;
                    break;
                }

                Console.WriteLine($"{enemyName} Health: {enemyHealth}");
                Console.WriteLine($"Health: {playerHealth}");
                Console.WriteLine($"Your Armor: {playerArmor} Your Damage: {playerDamage}");

                Console.WriteLine("What will you do now? Type 'attack' or 'flee'");
                Console.Write("< ");
                string playerAction = Console.ReadLine();

                if (playerAction == "attack")
                {
                    // Calculate the damage to the enemy
                    int enemyDamageTaken = playerDamage;
                    enemyHealth -= enemyDamageTaken;

                    Console.WriteLine($"You strike out at the {enemyName} and deal {enemyDamageTaken} damage.");

                    if (enemyHealth <= 0)
                    {
                        Console.WriteLine($"You have defeated the {enemyName}!");
                        combatEnd = true;
                    }
                }
                else if (playerAction == "flee")
                {
                    Console.WriteLine($"You managed to escape from the {enemyName}!");
                    combatEnd = true;
                }

            }
        }
    }
}


    

