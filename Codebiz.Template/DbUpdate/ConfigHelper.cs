using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace DbUpdate
{
    public class ConfigHelper
    {
        private readonly string configFile;
        private readonly DbConfig dbConfig;

        public string ConnectionString => $"Data Source={dbConfig.DataSource};Initial Catalog={dbConfig.Database};User Id={dbConfig.UserId};Password={dbConfig.Password};MultipleActiveResultSets=True";

        public ConfigHelper(string configFile = "Config.ini")
        {
            this.configFile = configFile;
            dbConfig = new DbConfig();
        }

        public void Start()
        {
            // Load Config
            this.Load();

            var connected = false;
            do
            {
                // Prompt User
                var configConfirmed = false;
                do
                {
                    Console.WriteLine("\nPlease provide the following information to connect to PSSLAI-AML Database.");

                    do
                    {
                        Console.Write("Data Server: ");
                        if (!String.IsNullOrWhiteSpace(dbConfig.DataSource))
                        {
                            Console.Write($"{dbConfig.DataSource} (Enter to accept) ");
                        }
                        var dataSource = Console.ReadLine();
                        if (dataSource != "")
                        {
                            dbConfig.DataSource = dataSource;
                        }

                        if (String.IsNullOrWhiteSpace(dbConfig.DataSource))
                        {
                            Console.WriteLine("Please provide value for Data Server.");
                        }
                    } while (String.IsNullOrWhiteSpace(dbConfig.DataSource));

                    do
                    {
                        Console.Write("Database: ");
                        if (!String.IsNullOrWhiteSpace(dbConfig.Database))
                        {
                            Console.Write($"{dbConfig.Database} (Enter to accept) ");
                        }
                        var database = Console.ReadLine();
                        if (database != "")
                        {
                            dbConfig.Database = database;
                        }

                        if (String.IsNullOrWhiteSpace(dbConfig.Database))
                        {
                            Console.WriteLine("Please provide value for Database.");
                        }
                    } while (String.IsNullOrWhiteSpace(dbConfig.Database));

                    do
                    {
                        Console.Write("User Id: ");
                        if (!String.IsNullOrWhiteSpace(dbConfig.UserId))
                        {
                            Console.Write($"{dbConfig.UserId} (Enter to accept) ");
                        }
                        var userId = Console.ReadLine();
                        if (userId != "")
                        {
                            dbConfig.UserId = userId;
                        }

                        if (String.IsNullOrWhiteSpace(dbConfig.UserId))
                        {
                            Console.WriteLine("Please provide value for User Id.");
                        }
                    } while (String.IsNullOrWhiteSpace(dbConfig.UserId));

                    do
                    {
                        Console.Write("Password: ");
                        if (!String.IsNullOrWhiteSpace(dbConfig.Password))
                        {
                            for (int i = 0; i < dbConfig.Password.Length; i++)
                            {
                                Console.Write("*");
                            }
                            Console.Write(" (Enter to accept) ");
                        }

                        var password = "";
                        ConsoleKeyInfo key;
                        do
                        {
                            key = Console.ReadKey(true);

                            if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                            {
                                password += key.KeyChar;
                                Console.Write("*");
                            }
                            else
                            {
                                if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                                {
                                    password = password.Substring(0, (password.Length - 1));
                                    Console.Write("\b \b");
                                }
                            }
                        }
                        while (key.Key != ConsoleKey.Enter);

                        if (password != "")
                        {
                            dbConfig.Password = password;
                        }

                        if (String.IsNullOrWhiteSpace(dbConfig.Password))
                        {
                            Console.WriteLine("Please provide value for Password.");
                        }
                    } while (String.IsNullOrWhiteSpace(dbConfig.Password));

                    Console.Write("\n\nAre you sure this is correct? [Y/N] ");

                    ConsoleKey response;
                    do
                    {
                        response = Console.ReadKey(false).Key;
                    } while (response != ConsoleKey.Y && response != ConsoleKey.N);

                    if (response == ConsoleKey.Y)
                    {
                        configConfirmed = true;
                    }
                    else if (response == ConsoleKey.N)
                    {
                        Console.Clear();
                    }
                } while (!configConfirmed);

                // Test Connection
                connected = this.TestConnection();
                if (connected)
                {
                    // Save Config            
                    this.Save();
                }
            }
            while (!connected);
        }

        private bool Load()
        {
            try
            {
                if (File.Exists(configFile))
                {
                    using (XmlReader reader = XmlReader.Create(configFile))
                    {
                        XElement root = XElement.Load(reader);

                        var dbConnection = root.Descendants("dbConnection").FirstOrDefault();
                        if (dbConnection != null)
                        {
                            dbConfig.DataSource = dbConnection.Element("dataSource").Value;
                            dbConfig.UserId = dbConnection.Element("userId").Value;
                            dbConfig.Password = StringCipher.Decrypt(dbConnection.Element("password").Value, "DbUpdate");
                        }

                        reader.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        private bool Save()
        {
            try
            {
                using (XmlTextWriter writer = new XmlTextWriter(configFile, System.Text.Encoding.UTF8))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 4;

                    writer.WriteStartDocument();
                    writer.WriteStartElement("configuration");

                    writer.WriteStartElement("dbConnection");

                    writer.WriteStartElement("dataSource");
                    writer.WriteString(dbConfig.DataSource);
                    writer.WriteEndElement();

                    writer.WriteStartElement("userId");
                    writer.WriteString(dbConfig.UserId);
                    writer.WriteEndElement();

                    writer.WriteStartElement("password");
                    writer.WriteString(StringCipher.Encrypt(dbConfig.Password, "DbUpdate"));
                    writer.WriteEndElement();

                    writer.WriteEndElement();

                    writer.WriteEndElement();
                    writer.WriteEndDocument();

                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        private bool TestConnection()
        {
            Console.Write("\n\nTesting connection...");
            var connStr = $"Data Source={dbConfig.DataSource};User Id={dbConfig.UserId};Password={dbConfig.Password};MultipleActiveResultSets=True";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                }
                catch
                {
                    Console.WriteLine("\nConnection Failed!");
                    return false;
                }

                conn.Close();
                Console.WriteLine("\nConnection Successful!");
                return true;
            }
        }
    }

    public class DbConfig
    {
        public string DataSource { get; set; }

        public string Database { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }
    }
}
