using System;
using System.ComponentModel;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Data.Sqlite;

namespace DatabaseProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowWidth = 180;
            //FillTable();
            //GetTen();
            //DeleteRecord();
            UpdateRecord();

        }

        static void UpdateRecord()
        {
            using (var connection = new SqliteConnection(@"Data Source=../../../video_games_v03.db"))
            {
                Console.WriteLine("********** UPDATE A GAME **********");
                Console.Write("Title: ");
                string inputTitle = Console.ReadLine();
                Console.Write("Year: ");
                int inputYear = Convert.ToInt32(Console.ReadLine());
                Console.Write("Console: ");
                string inputConsole = Console.ReadLine();

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @$"
        SELECT title, year, genres, publishers, review_score, sales, console
        FROM video_games
        WHERE title = '{inputTitle}' AND year = '{inputYear}' AND console = '{inputConsole}'
    ";
                var reader = command.ExecuteReader();
                PrintNames(reader);
                PrintCols(reader);

                Console.WriteLine("********** DO YOU WANT TO UPDATE THIS GAME? **********");
                string inputAnswer = Console.ReadLine().ToLower();
                var updateCommand = connection.CreateCommand();
                if (inputAnswer == "yes")
                {
                    Console.WriteLine("Which column do you want to update?");
                    var inputColumn = Console.ReadLine();
                    Console.WriteLine("What do you want to replace it with?");
                    var inputChange = Console.ReadLine();
                    updateCommand.CommandText =
                @$"
        UPDATE video_games
        SET '{inputColumn}' = '{inputChange}'
        WHERE title = '{inputTitle}' AND year = '{inputYear}' AND console = '{inputConsole}'
    ";
                    var updater = updateCommand.ExecuteNonQuery();
                }
                else if (inputAnswer == "no")
                {
                    UpdateRecord();
                }
            }
        }

        static void DeleteRecord()
        {
            using (var connection = new SqliteConnection(@"Data Source=../../../video_games_v03.db"))
            {
                Console.WriteLine("********** DELETE A GAME **********");
                Console.Write("Title: ");
                string inputTitle = Console.ReadLine();
                Console.Write("Year: ");
                int inputYear = Convert.ToInt32(Console.ReadLine());
                Console.Write("Console: ");
                string inputConsole = Console.ReadLine();

                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @$"
        SELECT title, year, genres, publishers, review_score, sales, console
        FROM video_games
        WHERE title = '{inputTitle}' AND year = '{inputYear}' AND console = '{inputConsole}'
    ";
                var reader = command.ExecuteReader();
                PrintNames(reader);
                PrintCols(reader);
                
                Console.WriteLine("********** DO YOU WANT TO DELETE THIS GAME? **********");
                string inputAnswer = Console.ReadLine().ToLower();
                var deleteCommand = connection.CreateCommand();
                if (inputAnswer == "yes")
                {
                    deleteCommand.CommandText =
                @$"
        DELETE FROM video_games
        WHERE title = '{inputTitle}' AND year = '{inputYear}' AND console = '{inputConsole}'
    ";
                    var deleter = deleteCommand.ExecuteNonQuery();
                }
                else if (inputAnswer == "no")
                {
                    DeleteRecord();
                }
            }
        }

        static void FillTable()
        {
            using (var connection = new SqliteConnection(@"Data Source=../../../video_games_v03.db"))
            {
                Console.WriteLine("********** ADD A NEW GAME **********");
                Console.Write("Title: ");
                string title = Console.ReadLine();
                Console.Write("Year: ");
                int year = Convert.ToInt32(Console.ReadLine());
                Console.Write("Genre: ");
                string genre = Console.ReadLine();
                Console.Write("Publisher: ");
                string publisher = Console.ReadLine();
                Console.Write("Review Score: ");
                int review_score = Convert.ToInt32(Console.ReadLine());
                Console.Write("Sales: ");
                decimal sales = Convert.ToDecimal(Console.ReadLine());
                Console.Write("Console: ");
                string console = Console.ReadLine();


                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @$"
        INSERT INTO video_games (TITLE,YEAR,GENRES,PUBLISHERS,REVIEW_SCORE,SALES,CONSOLE)
        VALUES ('{title}',{year},'{genre}','{publisher}',{review_score},{sales},'{console}')
    ";
               int insert = command.ExecuteNonQuery();
            }
        }

        static void GetTen()
        {
            using (var connection = new SqliteConnection(@"Data Source=../../../video_games_v03.db"))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText =
                @"
        SELECT title, year, genres, publishers, review_score, sales, console
        FROM video_games
        ORDER BY genres
    ";
                var reader = command.ExecuteReader();
                PrintNames(reader);
                PrintCols(reader);
            }

        }

        static void PrintCols(SqliteDataReader reader)
        {
            for (int i = 1; reader.Read(); i++)
            {
                var title = (reader.IsDBNull(0) ? "null".PadRight(50) : reader.GetString(0).PadRight(50));
                var year = (reader.IsDBNull(1) ? "null".PadRight(10) : reader.GetString(1).PadRight(10));
                var genre = (reader.IsDBNull(2) ? "null".PadRight(40) : reader.GetString(2).PadRight(40));
                var publisher = (reader.IsDBNull(3) ? "null".PadRight(25) : reader.GetString(3).PadRight(25));
                var review_score = (reader.IsDBNull(4) ? "null".PadRight(15) : reader.GetString(4).PadRight(15));
                var sales = (reader.IsDBNull(5) ? "null".PadRight(10) : reader.GetString(5).PadRight(10));
                var console = (reader.IsDBNull(6) ? "null".PadRight(20) : reader.GetString(6).PadRight(20));

                Console.Write($"{title}");
                Console.Write($"{year}");
                Console.Write($"{genre}");
                Console.Write($"{publisher}");
                Console.Write($"{review_score}");
                Console.Write($"{sales}");
                Console.Write($"{console} \n");

                if (i > 9 && i % 10 == 0)
                {
                    Console.ReadLine();
                }
            }
        }

        static void PrintNames(SqliteDataReader reader)
        {
            Console.Write(reader.GetName(0).PadRight(50));
            Console.Write(reader.GetName(1).PadRight(10));
            Console.Write(reader.GetName(2).PadRight(40));
            Console.Write(reader.GetName(3).PadRight(25));
            Console.Write(reader.GetName(4).PadRight(15));
            Console.Write(reader.GetName(5).PadRight(10));
            Console.Write(reader.GetName(6).PadRight(20));
            Console.Write("\n");

            Console.Write(reader.GetDataTypeName(0).PadRight(50));
            Console.Write(reader.GetDataTypeName(1).PadRight(10));
            Console.Write(reader.GetDataTypeName(2).PadRight(40));
            Console.Write(reader.GetDataTypeName(3).PadRight(25));
            Console.Write(reader.GetDataTypeName(4).PadRight(15));
            Console.Write(reader.GetDataTypeName(5).PadRight(10));
            Console.Write(reader.GetDataTypeName(6).PadRight(20));
            Console.Write("\n");
        }
    }
}


