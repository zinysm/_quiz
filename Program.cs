//Methods.Login();
Methods.ButtonChoiceMainMenu();
public static class Methods
{
    static bool isEscapePressed = false;
    static Dictionary<string, int> playerScores = new Dictionary<string, int>();
    static string? LogInplayerName = string.Empty;
    public static void Login()
    {
        Console.WriteLine("Sveiki, įveskite savo vardą ir pavardę");
        while (true)
        {
            LogInplayerName = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(LogInplayerName))
            {
                Console.WriteLine("Įveskite vardą ir pavardę");
                continue;
            }
            else if (playerScores.ContainsKey(LogInplayerName))
            {
                Console.WriteLine("Jau egzistuoja toks vartotojas, įveskite kitą vardą ir pavardę");
                continue;
            }
            break;
        }

        if (!playerScores.ContainsKey(LogInplayerName))
        {
            playerScores.Add(LogInplayerName, 0);
        }
    }

    public static void Logout()
    {
        Console.WriteLine($"Atsijunge useris {LogInplayerName}");
        LogInplayerName = string.Empty;
        Login();
    }

    public static void AddScore(int scoresToAdd)
    {
        if (!string.IsNullOrEmpty(LogInplayerName))
            playerScores[LogInplayerName] += scoresToAdd;
    }

    public static void PrintMainMenu()
    {
        Console.WriteLine($"Sveikas prisijungęs {LogInplayerName}");
        Console.WriteLine("[P] Prisijungimas");
        Console.WriteLine("[A] Atsijungimas");
        Console.WriteLine("[T] Žaidimų taisyklių atvaizdavimas");
        Console.WriteLine("[R] Žaidimo rezultatų ir dalyvių peržiūra");
        Console.WriteLine("[S] Dalyvavimas (Start game)");
        Console.WriteLine("[Esc]. Išėjimas iš žaidimo ");
        Console.WriteLine("Norėdami pasirinkti menu paspauskite atitinkamą mygtuką");
    }


public static void PrintPlayers()
    {
        Console.Clear();
        Console.WriteLine("Žaidime dalyvautis žaidėjai");
        foreach (var playerKeyValuePair in playerScores)
        {
            Console.WriteLine(playerKeyValuePair.Key);
        }
        Console.WriteLine("Norint grįžti į Menu paspauskite B, norint išeiti paspasukite escape");
    }

    public static void PrintPlayersResults()
    {
        Console.Clear();
        Console.WriteLine("Žaidime dalyvautis žaidėjai");
        foreach (var playerKeyValuePair in playerScores)
        {
            Console.WriteLine(playerKeyValuePair); 
        }
        Console.WriteLine("Norint grįžti į Menu paspauskite B, norint išeiti paspasukite escape");
    }
    public static void PrintResultMenu()
    {
        Console.WriteLine($"Prisijungęs {LogInplayerName}");
        Console.WriteLine("[D] Dalyviai");
        Console.WriteLine("[T] Taškai");
        Console.WriteLine("Norint pasirinkti menu paspauskite atitinkamą mygtuką,\ngrįžti atgal spauskite B,\nišeiti iš žaidimo Esc");

    }

    public static void GamesRules()
    {
        Console.WriteLine($"Prisijungęs {LogInplayerName}");
        Console.WriteLine("Sveikiname prisijungus prie X protmūšio programos.");
        Console.WriteLine("Šis protmūšis jums leidžia pasirinkti iš X klausimų kategorijų.");
        Console.WriteLine("Pasirinkus kategoriją pradėsite žaidimą ir turėsite pasirinkti iš 4 galimų variantų,\nkuris yra jūsų klausimui teisingas atsakymas.");
        Console.WriteLine("Norint grįžti atgal paspauskite B, norint išeiti paspasukite escape");
    }
    public static void ButtonsChoiceSecondaryMenu()
    {
        {
            while (true)
            {
                var button = Console.ReadKey();
                switch (button.Key)
                {
                    case ConsoleKey.B:
                        Console.Clear();
                        break;
                    case ConsoleKey.Escape:
                        Console.WriteLine("Išėjimas iš žaidimo");
                        isEscapePressed = true;
                        break;
                    case ConsoleKey.D:
                        PrintPlayers();
                        break;
                    case ConsoleKey.T:
                        PrintPlayersResults();
                        break;
                    default:
                        Console.WriteLine("unknow command");
                        continue;
                }
                break;
            }

        }
    }
    public static void BackOrExit()
    {
        {
            while (true)
            {
                var button = Console.ReadKey();
                switch (button.Key)
                {
                    case ConsoleKey.B:
                        Console.Clear();
                        break;
                    case ConsoleKey.Escape:
                        Console.WriteLine("Išėjimas iš žaidimo");
                        isEscapePressed = true;
                        break;
                    default:
                        Console.WriteLine("unknow command");
                        continue;
                }
                break;
            }

        }
    }


    public static void ButtonChoiceMainMenu()
    {
        
        while (!isEscapePressed)
        {
            Console.Clear();
            PrintMainMenu();
            var button = Console.ReadKey();
            switch (button.Key)
            {
                case ConsoleKey.P: // LOGIN
                    Console.Clear();
                    Login();
                    continue;
                case ConsoleKey.A: // LOGOUT
                    Console.Clear();
                    Logout();
                    continue;
                case ConsoleKey.R: // RESULTS
                    Console.Clear();
                    PrintResultMenu();
                    ButtonsChoiceSecondaryMenu();
                    BackOrExit();
                    continue;
                case ConsoleKey.T: // RULES
                    Console.Clear();
                    GamesRules();
                    BackOrExit();
                    continue;
                case ConsoleKey.S: // START GAME
                    Console.WriteLine("Žaidimo pradžia");
                    continue;
                case ConsoleKey.Escape: // EXIT
                    Console.WriteLine("Išėjimas iš žaidimo");
                    isEscapePressed = true;
                    break;
                default:  // WRONG INPUT
                    Console.WriteLine("unknow command");
                    continue;
            }
            break;
        }

    }
}





