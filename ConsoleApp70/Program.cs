using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp70
{
    class Game
    {
        public string Title { get; set; }
        public string Studio { get; set; }
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        public int SoldUnits { get; set; }
    }

    class GameDatabase
    {
        private List<Game> games;

        public GameDatabase()
        {
            games = new List<Game>();
        }

        public bool AddGame(Game newGame)
        {
            if (games.Any(game => game.Title.ToLower() == newGame.Title.ToLower() && game.Studio.ToLower() == newGame.Studio.ToLower()))
            {
                Console.WriteLine("Така гра вже існує в базі.");
                return false;
            }

            games.Add(newGame);
            Console.WriteLine("Гру додано до бази.");
            return true;
        }

        public bool UpdateGame(string title, string studio, Game updatedGame)
        {
            var gameToUpdate = games.FirstOrDefault(game => game.Title.ToLower() == title.ToLower() && game.Studio.ToLower() == studio.ToLower());
            if (gameToUpdate != null)
            {
                gameToUpdate.Title = updatedGame.Title;
                gameToUpdate.Studio = updatedGame.Studio;
                gameToUpdate.Genre = updatedGame.Genre;
                gameToUpdate.ReleaseYear = updatedGame.ReleaseYear;
                gameToUpdate.SoldUnits = updatedGame.SoldUnits;
                Console.WriteLine("Інформацію про гру оновлено.");
                return true;
            }
            Console.WriteLine("Гру не знайдено.");
            return false;
        }

        public bool DeleteGame(string title, string studio)
        {
            var gameToDelete = games.FirstOrDefault(game => game.Title.ToLower() == title.ToLower() && game.Studio.ToLower() == studio.ToLower());
            if (gameToDelete != null)
            {
                Console.WriteLine($"Ви впевнені, що хочете видалити гру {gameToDelete.Title} від {gameToDelete.Studio}? (Так/Ні)");
                string response = Console.ReadLine();
                if (response.ToLower() == "так")
                {
                    games.Remove(gameToDelete);
                    Console.WriteLine("Гру видалено.");
                    return true;
                }
                else
                {
                    Console.WriteLine("Видалення гри скасовано.");
                    return false;
                }
            }
            Console.WriteLine("Гру не знайдено.");
            return false;
        }
        public List<Game> SearchByGameTitle(string title)
        {
            return games.FindAll(game => game.Title.ToLower().Contains(title.ToLower()));
        }

        public List<Game> SearchByStudioName(string studioName)
        {
            return games.FindAll(game => game.Studio.ToLower() == studioName.ToLower());
        }

        public List<Game> SearchByStudioAndGame(string studioName, string gameTitle)
        {
            return games.FindAll(game => game.Studio.ToLower() == studioName.ToLower() && game.Title.ToLower().Contains(gameTitle.ToLower()));
        }

        public List<Game> SearchByGenre(string genre)
        {
            return games.FindAll(game => game.Genre.ToLower() == genre.ToLower());
        }

        public List<Game> SearchByReleaseYear(int year)
        {
            return games.FindAll(game => game.ReleaseYear == year);
        }

        public List<Game> GetSinglePlayerGames()
        {
            return games.FindAll(game => game.Genre.ToLower().Contains("singleplayer"));
        }

        public List<Game> GetMultiplayerGames()
        {
            return games.FindAll(game => game.Genre.ToLower().Contains("multiplayer"));
        }

        public Game GetGameWithMostUnitsSold()
        {
            return games.OrderByDescending(game => game.SoldUnits).FirstOrDefault();
        }

        public Game GetGameWithLeastUnitsSold()
        {
            return games.OrderBy(game => game.SoldUnits).FirstOrDefault();
        }

        public List<Game> GetTop3MostSoldGames()
        {
            return games.OrderByDescending(game => game.SoldUnits).Take(3).ToList();
        }

        public List<Game> GetTop3LeastSoldGames()
        {
            return games.OrderBy(game => game.SoldUnits).Take(3).ToList();
        }
        internal class Program
        {
            static void Main(string[] args)
            {
                GameDatabase database = new GameDatabase();

                while (true)
                {
                    Console.WriteLine("Оберіть опцію:");
                    Console.WriteLine("1. Пошук за назвою гри");
                    Console.WriteLine("2. Додати нову гру");
                    Console.WriteLine("3. Оновити існуючу гру");
                    Console.WriteLine("4. Видалити гру");
                    Console.WriteLine("5. Показати всі однокористувацькі ігри");
                    Console.WriteLine("6. Показати всі багатокористувацькі ігри");
                    Console.WriteLine("7. Показати гру з максимальною кількістю проданих ігор");
                    Console.WriteLine("8. Вийти");

                    int option = Convert.ToInt32(Console.ReadLine());

                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("Введіть назву гри для пошуку:");
                            string gameTitle = Console.ReadLine();
                            var gamesByTitle = database.SearchByGameTitle(gameTitle);
                            // Виведення результатів
                            foreach (var game in gamesByTitle)
                            {
                                Console.WriteLine($"Гра: {game.Title}, Студія: {game.Studio}, Рік: {game.ReleaseYear}");
                            }
                            break;

                        case 2:
                            Console.WriteLine("Введіть назву нової гри:");
                            string newGameTitle = Console.ReadLine();
                            Console.WriteLine("Введіть студію нової гри:");
                            string newGameStudio = Console.ReadLine();
                            // Інші дані для нової гри (жанр, рік, кількість проданих тощо)
                            Game newGame = new Game { Title = newGameTitle, Studio = newGameStudio, Genre = "Новий жанр", ReleaseYear = 2023, SoldUnits = 0 };
                            database.AddGame(newGame);
                            break;

                        case 3:
                            Console.WriteLine("Введіть назву гри, яку потрібно оновити:");
                            string gameToUpdateTitle = Console.ReadLine();
                            Console.WriteLine("Введіть студію гри:");
                            string gameToUpdateStudio = Console.ReadLine();
                            // Інші дані для оновлення
                            Game updatedGame = new Game { Title = "Оновлена гра", Studio = "Студія", Genre = "Жанр", ReleaseYear = 2022, SoldUnits = 150 };
                            database.UpdateGame(gameToUpdateTitle, gameToUpdateStudio, updatedGame);
                            break;

                        case 4:
                            Console.WriteLine("Введіть назву гри для видалення:");
                            string gameToDeleteTitle = Console.ReadLine();
                            Console.WriteLine("Введіть студію гри:");
                            string gameToDeleteStudio = Console.ReadLine();
                            database.DeleteGame(gameToDeleteTitle, gameToDeleteStudio);
                            break;

                        case 5:
                            var singlePlayerGames = database.GetSinglePlayerGames();
                            foreach (var game in singlePlayerGames)
                            {
                                Console.WriteLine($"Однокористувацька гра: {game.Title}, Продано одиниць: {game.SoldUnits}");
                            }
                            break;

                        case 6:
                            var multiplayerGames = database.GetMultiplayerGames();
                            foreach (var game in multiplayerGames)
                            {
                                Console.WriteLine($"Багатокористувацька гра: {game.Title}, Продано одиниць: {game.SoldUnits}");
                            }
                            break;

                        case 7:
                            var gameWithMostSales = database.GetGameWithMostUnitsSold();
                            Console.WriteLine($"Гра з максимальною кількістю проданих ігор: {gameWithMostSales.Title}, Продано одиниць: {gameWithMostSales.SoldUnits}");
                            break;

                        case 8:
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                            break;
                    }
                }
            }
        }
    }
}