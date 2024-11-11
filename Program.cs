using System;
using System.Numerics;
string[] files = {"nature","geografy"};
Dictionary<string, List<int>> questionCategories = new Dictionary<string, List<int>>();
Methods.Login();
Methods.ButtonChoiceMainMenu(files);

public static class Methods
{

    static bool isEscapePressed = false;
    static Dictionary<string, int> playerScores = new Dictionary<string, int>();
    static string? LogInplayerName = string.Empty;

    public static void StartGame(string[] files)
    {
        Console.WriteLine($"Logged in player: {LogInplayerName}");
        PrintQuestionsCategories(files);

        int cumulativeScore = 0; 

        bool playAnotherCategory;
        do
        {
            var chosenCategory = ChosenCategory(files);
            int categoryScore = MainGameEngine(files, chosenCategory);

            if (categoryScore != -1)
            {
                cumulativeScore += categoryScore;
            }

            Console.WriteLine("Do you want to play another category? (y/n)");
            playAnotherCategory = Console.ReadKey(true).Key == ConsoleKey.Y;
        }
        while (playAnotherCategory);

        AddScore(cumulativeScore);
        Console.WriteLine($"Your total score across all categories: {cumulativeScore}");
        Console.WriteLine("\nPress any key to return to the main menu...");
        Console.ReadKey();
    }

    public static int MainGameEngine(string[] files, string chosenCategory)
    {
        int index = 0;
        int totalScore = 0;
        var categoryDic = GetQuestionByCategories(files);
        var answersDic = GetAnswersByCategory(files);
        bool isEscapePressed = false;
        List<(int questionNumber, bool isCorrect)> questionResults = new List<(int, bool)>();

        int questionCount = GetQuestionCount(categoryDic, chosenCategory, index);
        Console.WriteLine("Press any key to start the game or 'Esc' to quit...");
        var startKey = Console.ReadKey(true);
        if (startKey.Key == ConsoleKey.Escape)
        {
            Console.WriteLine("Exiting the game...");
            return -1;
        }

        while (index < questionCount)
        {
            Console.Clear();
            Console.WriteLine($"Category: {chosenCategory.ToUpper()}");

            PrintQuestion(categoryDic, chosenCategory, index);
            AnswersPrint(answersDic, chosenCategory, index);
            Console.WriteLine("Press the answer number:");

            var correctAnswerIndex = GetCorrectAnswerFromData(files, chosenCategory, index);
            var selectedAnswer = ChosenAnswer();
            Console.WriteLine($"You selected: {selectedAnswer}");

            if (selectedAnswer == correctAnswerIndex)
            {
                Console.WriteLine("Correct!");
                int points = GetCorrectAnswerPoints(files, chosenCategory, index);
                totalScore += points;
                questionResults.Add((index + 1, true));
            }
            else
            {
                Console.WriteLine("Incorrect.");
                questionResults.Add((index + 1, false));
            }

            Console.WriteLine($"Your current score: {totalScore}");
            index++;

            if (index < questionCount)
            {
                Console.WriteLine("Press any key to continue to the next question or 'Esc' to quit...");
                var questionKey = Console.ReadKey(true);
                if (questionKey.Key == ConsoleKey.Escape)
                {
                    Console.WriteLine("Exiting the game...");
                    isEscapePressed = true;
                    break;
                }
            }
            else
            {
                Console.WriteLine("You've completed all questions in this category!");
            }
        }
        Console.Clear();
        if (!isEscapePressed)
        {
            Console.WriteLine("Congratulations! You've completed the game.");
        }
        else
        {
            Console.WriteLine("Game exited early.");
        }
        Console.WriteLine($"\nTotal Score for this category: {totalScore}");
        Console.WriteLine("Question Results:");
        foreach (var result in questionResults)
        {
            string outcome = result.isCorrect ? "Correct" : "Incorrect";
            Console.WriteLine($"Question {result.questionNumber}: {outcome}");
        }
        playerScores[LogInplayerName] = totalScore;
        var sortedScores = playerScores.OrderByDescending(score => score.Value).ToList();
        int position = sortedScores.FindIndex(player => player.Key == LogInplayerName) + 1;

        Console.WriteLine($"\nIn {chosenCategory}{LogInplayerName}, you finished in position: {position}");
        Console.WriteLine("Thanks for playing!");
        Console.WriteLine("\nPress any key to return to the main menu...");
        Console.ReadKey();
        return totalScore;
    }
    //public static void StartGame(string[]files)
    //{
    //    Console.WriteLine($"Logged in player: {LogInplayerName}");
    //    PrintQuestionsCategories(files);
    //    var chosenCategory = ChosenCategory(files);
    //    var totalPoints= MainGameEngine(files,chosenCategory);
    //    AddScore(totalPoints);
    //}

    //public static int MainGameEngine(string[] files, string chosenCategory)
    //{
    //    int index = 0;
    //    int totalScore = 0;
    //    var categoryDic = GetQuestionByCategories(files);
    //    var answersDic = GetAnswersByCategory(files);
    //    bool isEscapePressed = false;
    //    List<(int questionNumber, bool isCorrect)> questionResults = new List<(int, bool)>();

    //    int questionCount = GetQuestionCount(categoryDic, chosenCategory, index);
    //    Console.WriteLine("Press any key to start the game or 'Esc' to quit...");
    //    var startKey = Console.ReadKey(true);
    //    if (startKey.Key == ConsoleKey.Escape)
    //    {
    //        Console.WriteLine("Exiting the game...");
    //        return -1;
    //    }

    //    while (index < questionCount)
    //    {
    //        Console.Clear();
    //        Console.WriteLine($"Category: {chosenCategory.ToUpper()}");

    //        PrintQuestion(categoryDic, chosenCategory, index);
    //        AnswersPrint(answersDic, chosenCategory, index);
    //        Console.WriteLine("Press the answer number:");

    //        var correctAnswerIndex = GetCorrectAnswerFromData(files, chosenCategory, index);
    //        var selectedAnswer = ChosenAnswer();
    //        Console.WriteLine($"You selected: {selectedAnswer}");

    //        if (selectedAnswer == correctAnswerIndex)
    //        {
    //            Console.WriteLine("Correct!");
    //            int points = GetCorrectAnswerPoints(files, chosenCategory, index);
    //            totalScore += points;
    //            questionResults.Add((index + 1, true));
    //        }
    //        else
    //        {
    //            Console.WriteLine("Incorrect.");
    //            questionResults.Add((index + 1, false));
    //        }

    //        Console.WriteLine($"Your current score: {totalScore}");
    //        index++;

    //        if (index < questionCount)
    //        {
    //            Console.WriteLine("Press any key to continue to the next question or 'Esc' to quit...");
    //            var questionKey = Console.ReadKey(true);
    //            if (questionKey.Key == ConsoleKey.Escape)
    //            {
    //                Console.WriteLine("Exiting the game...");
    //                isEscapePressed = true;
    //                break;
    //            }
    //        }
    //        else
    //        {
    //            Console.WriteLine("You've completed all questions in this category!");
    //        }
    //    }
    //    Console.Clear();
    //    if (!isEscapePressed)
    //    {
    //        Console.WriteLine("Congratulations! You've completed the game.");
    //    }
    //    else
    //    {
    //        Console.WriteLine("Game exited early.");
    //    }
    //    Console.WriteLine($"\nTotal Score: {totalScore}");
    //    Console.WriteLine("Question Results:");
    //    foreach (var result in questionResults)
    //    {
    //        string outcome = result.isCorrect ? "Correct" : "Incorrect";
    //        Console.WriteLine($"Question {result.questionNumber}: {outcome}");
    //    }
    //    playerScores[LogInplayerName] = totalScore;
    //    var sortedScores = playerScores.OrderByDescending(score => score.Value).ToList();
    //    int position = sortedScores.FindIndex(player => player.Key == LogInplayerName) + 1;

    //    Console.WriteLine($"\n{LogInplayerName}, you finished in position: {position}");
    //    Console.WriteLine("Thanks for playing!");
    //    Console.WriteLine("\nPress any key to return to the main menu...");
    //    Console.ReadKey();
    //    return totalScore;
    //}
    public static void AddScore(int scoresToAdd)
    {
        if (!string.IsNullOrEmpty(LogInplayerName))
            playerScores[LogInplayerName] += scoresToAdd;
        Console.WriteLine(playerScores[LogInplayerName]);
    }

    public static string ChosenCategory(string[] files)
    {
        var choosenCategory = string.Empty;
        {
            while (true)
            {
                var button = Console.ReadKey();
                Console.Clear();
                switch (button.Key)
                {
                    case ConsoleKey.NumPad1:
                        choosenCategory = files[0];
                        Console.WriteLine($"Your choosen category is {choosenCategory.ToUpper()}");
                        break;
                    case ConsoleKey.NumPad2:
                        choosenCategory = files[1];
                        Console.WriteLine($"Your choosen category is {choosenCategory.ToUpper()}");
                        break;
                    case ConsoleKey.NumPad3:
                        choosenCategory = files[2];
                        Console.WriteLine($"Your choosen category is {choosenCategory.ToUpper()}");
                        break;
                    case ConsoleKey.NumPad4:
                        choosenCategory = files[3];;
                        Console.WriteLine($"Your choosen category is {choosenCategory.ToUpper()}");
                        break;
                    default:
                        Console.WriteLine("unknow command");
                        continue;
                }
                break;
            }

        }
        return choosenCategory;
    }
    public static void ButtonChoiceMainMenu(string [] files)
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
                    Console.Clear();
                    StartGame(files);
                    continue;
                case ConsoleKey.Escape: // EXIT
                    Console.WriteLine("Quit Game");
                    isEscapePressed = true;
                    break;
                default:  // WRONG INPUT
                    Console.WriteLine("unknow command");
                    continue;
            }
            break;
        }

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
                        Console.WriteLine("Exit game");
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
    public static int ChosenAnswer()
    {

        var answersNumbers = 0;
        {
            while (true)
            {
                var button = Console.ReadKey();
                Console.Clear();
                switch (button.Key)
                {
                    case ConsoleKey.NumPad1:
                        answersNumbers = 1;
                        break;
                    case ConsoleKey.NumPad2:
                        answersNumbers = 2;
                        break;
                    case ConsoleKey.NumPad3:
                        answersNumbers = 3;
                        break;
                    case ConsoleKey.NumPad4:
                        answersNumbers = 4;
                        break;
                    case ConsoleKey.Escape:
                        Console.WriteLine("Exit game");
                        isEscapePressed = true;
                        break;
                    default:
                        Console.WriteLine("Unknown command. Please press 1, 2, 3, or 4");
                        continue;
                }
                break;
            }

        }
        return answersNumbers;
    }


    public static void PrintQuestionsCategories(string[] dataInfo)
    {
        var categoryDic = GetQuestionByCategories(dataInfo);
        int i = 1;
            foreach (var category in categoryDic)
            {

                Console.WriteLine($"Category: {i}. {category.Key}");
                i++;
            }
        
    }
    public static int GetQuestionCount(Dictionary<string, List<string[]>> categoryDic, string chosenCategory, int index)
    {
        return categoryDic.TryGetValue(chosenCategory, out var questions) ? questions.Count : 0;
    }



    public static void PrintQuestion(Dictionary<string, List<string[]>> categoryDic, string chosenCategory, int index)
    {
        if (categoryDic.TryGetValue(chosenCategory, out var questions) && index < questions.Count)
        {
            Console.WriteLine($"{index + 1}/{questions.Count}: {questions[index][0]}");
        }

    }
    public static Dictionary<string, List<string[]>> GetAnswersByCategory(string[] dataInfo)
    {
        var answersDic = new Dictionary<string, List<string[]>>();
        var categoryDic = GetQuestionByCategories(dataInfo);

        foreach (var category in categoryDic)
        {
            var answers = new List<string[]>();
            foreach (var data in category.Value)
            {
                var aData = new string[] { data[1], data[2], data[3], data[4] };
                answers.Add(aData);
            }
            answersDic[category.Key] = answers;
        }
        return answersDic;
    }
    public static void AnswersPrint(Dictionary<string, List<string[]>> answersDic, string chosenCategory, int index)
    {
        if (answersDic.TryGetValue(chosenCategory, out var answers) && index < answers.Count)
        {
            int answerNumber = 1;
            foreach (var answer in answers[index])
            {
                Console.WriteLine($"{answerNumber}. {answer}");
                answerNumber++;
            }
        }
    }

    public static int GetCorrectAnswerFromData(string[] dataInfo, string chosencategory, int index)
    {
        var categoryDic = GetQuestionByCategories(dataInfo);
        var correctAnswerList = new List<int>();
        foreach (var category in categoryDic)
        {
            if (category.Key.Equals(chosencategory))
            {
                foreach (var data in category.Value)
                {
                    var correctAnswer = int.TryParse(data[5], out int i);
                   
                    correctAnswerList.Add(i);

                }
            }
        }
        return correctAnswerList[index];
    }
    public static int GetCorrectAnswerPoints(string[] dataInfo, string chosencategory, int index)
    {
        var categoryDic = GetQuestionByCategories(dataInfo);
        var correctAnswerList = new List<int>();
        foreach (var category in categoryDic)
        {
            if (category.Key.Equals(chosencategory))
            {
                foreach (var data in category.Value)
                {
                    var correctAnswer = int.TryParse(data[6], out int i);

                    correctAnswerList.Add(i);

                }
            }
        }
        return correctAnswerList[index];
    }

    public static Dictionary<string, List<string[]>> GetQuestionByCategories(string[] files) 
    {
        var Categories = new Dictionary<string, List<string[]>>();
        foreach (var category in files)
        {
            var failoTurinys = File.ReadAllLines(category + ".txt"); 
            var categoryQuestions = FileInfoData(failoTurinys); 
            Categories.Add(category, categoryQuestions);
        }
        return Categories;
    }

    public static List<string[]> FileInfoData(string[] questionsAnswersScores)
    {
        var result = new List<string[]>();
        for (int i = 0; i < questionsAnswersScores.Length; i++)
        {
            var susplitintaEilute = questionsAnswersScores[i].Split(';');

            if (susplitintaEilute.Length != 7)
            {
                Console.WriteLine("Error:\tWrong element number");
                continue;
            }

            result.Add(susplitintaEilute);
        }
        return result;
    }

public static void Login()
    {
        Console.WriteLine("Hello enter your name or surname");
        while (true)
        {
            LogInplayerName = Console.ReadLine().Trim();
            if (string.IsNullOrEmpty(LogInplayerName))
            {
                Console.WriteLine("Enter name and surnamie");
                continue;
            }
            else if (playerScores.ContainsKey(LogInplayerName))
            {
                Console.WriteLine("Allready in use user");
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

    public static void PrintMainMenu()
    {
        Console.WriteLine($"Hello,logged in {LogInplayerName}");
        Console.WriteLine("[P] Log in");
        Console.WriteLine("[A] Log out");
        Console.WriteLine("[T] Rules");
        Console.WriteLine("[R] Results and users");
        Console.WriteLine("[S] Start game");
        Console.WriteLine("[Esc]. Exit game ");
        Console.WriteLine("Press button which cattegory you want to choose");
    }

    public static void PrintQuestions()
    {
        Console.Clear();
        Console.WriteLine("Users in game");
        foreach (var playerKeyValuePair in playerScores)
        {
            Console.WriteLine(playerKeyValuePair.Key);
        }
        Console.WriteLine("If you want go back press B, to exit game - escape");
    }
    public static void PrintPlayers()
    {
        Console.Clear();
        Console.WriteLine("Users in game");
        foreach (var playerKeyValuePair in playerScores)
        {
            Console.WriteLine(playerKeyValuePair.Key);
        }
        Console.WriteLine("If you want go back press B, to exit game - escape");
    }

    public static void PrintPlayersResults()
    {
        Console.Clear();
        Console.WriteLine("Users in game");
        var sortedPlayers = playerScores.OrderByDescending(player => player.Value).ToList();
        for (int i = 0; i < sortedPlayers.Count; i++)
        {
            var player = sortedPlayers[i];
            string stars = i switch
            {
                0 => "***",   
                1 => "**",    
                2 => "*",     
                _ => ""       
            };
            if (i<10)
            {
               Console.WriteLine($"TOP{i+1} Player {player.Key}{stars} Score: {player.Value}");
            }
            else 
            {
              Console.WriteLine($"Player {player.Key} Score: {player.Value}");
            }
        }
        Console.WriteLine("If you want go back press B, to exit game - escape");
    }
    public static void PrintResultMenu()
    {
        Console.WriteLine($"Logged in {LogInplayerName}");
        Console.WriteLine("[D] User");
        Console.WriteLine("[T] User results");
        Console.WriteLine("If you want go back press B, to exit game - escape");

    }

    public static void GamesRules()
    {
        Console.WriteLine($"Logged in {LogInplayerName}");
        Console.WriteLine("Hello, you joind X quiz game.");
        Console.WriteLine("This quiz lets you choose 10 question from 4 cattegories");
        Console.WriteLine("When you choose cattegory, you get 10 question with 4 answers. Chose the right answer");
        Console.WriteLine("If you want go back press B, to exit game - escape");
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
                        Console.WriteLine("Exit game");
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


}





