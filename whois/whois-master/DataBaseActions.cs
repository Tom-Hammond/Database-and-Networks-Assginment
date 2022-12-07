using Microsoft.VisualBasic;
using System.Net.NetworkInformation;
using System.Net;
using System.Net.Sockets;
using whois;
using MySql.Data;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;
using static System.Net.WebRequestMethods;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using System.Xml;
using System;

namespace whois
{
    public class DataBaseActions
    {
        public Boolean debug;
       public  Dictionary<String, User> DataBase = new Dictionary<String, User> { };

       

        /// Functions to process database requests
        public void Delete(String ID)
        {
            if (debug)
            {
                Console.WriteLine($"Delete record '{ID}' from DataBase");
            }
            DataBase.Remove(ID);
        }

       

        /// Functions to process database requests
        public void Dump(String ID)
        {

            if (debug)
            {
                Console.WriteLine(" output all fields");
            }
            Console.WriteLine($"UserID={DataBase[ID].UserID}");
            Console.WriteLine($"Surname={DataBase[ID].Surname}");
            Console.WriteLine($"Fornames={DataBase[ID].Fornames}");
            Console.WriteLine($"Title={DataBase[ID].Title}");
            Console.WriteLine($"Position={DataBase[ID].Position}");
            Console.WriteLine($"Phone={DataBase[ID].Phone}");
            Console.WriteLine($"Email={DataBase[ID].Email}");
            Console.WriteLine($"location={DataBase[ID].Location}");

        }

        public string Lookup(String ID, String field)
        {
            string output = null;
            if (debug)
            {
                Console.WriteLine($" lookup field '{field}'");
            }
            String result = null;
            switch (field)
            {
                case "location": result = DataBase[ID].Location; break;
                case "UserID": result = DataBase[ID].UserID; break;
                case "Surname": result = DataBase[ID].Surname; break;
                case "Fornames": result = DataBase[ID].Fornames; break;
                case "Title": result = DataBase[ID].Title; break;
                case "Phone": result = DataBase[ID].Phone; break;
                case "Position": result = DataBase[ID].Position; break;
                case "Email": result = DataBase[ID].Email; break;
            }
            return output = result;

        }

        public string Update(String ID, String field, String update)
        {
            string output = null;
            if (debug)
            {
                Console.WriteLine($" update field '{field}' to '{update}'");
            }
            switch (field)
            {
                case "location": DataBase[ID].Location = update; break;
                case "UserID": DataBase[ID].UserID = update; break;
                case "Surname": DataBase[ID].Surname = update; break;
                case "Fornames": DataBase[ID].Fornames = update; break;
                case "Title": DataBase[ID].Title = update; break;
                case "Phone": DataBase[ID].Phone = update; break;
                case "Position": DataBase[ID].Position = update; break;
                case "Email": DataBase[ID].Email = update; break;
            }


            UpdateDatabaseSQL(ID, update); 
            Console.WriteLine("OK");
            return output = "update field '{field}' to '{update}\n OK";
           
        }

        public void UpdateDatabaseSQL(string ID, string updateText)
        {
            string connStr = "server=localhost;user=root;database=users;port=3306;password=L3tM31n";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sqlText = @"UPDATE userdetails SET Location = @location WHERE LoginID = @id";
                // Query text incorporated into SQL command.
                MySqlCommand sqlUpdate = new MySqlCommand(sqlText, conn);

                // Bind the parameters to the query.
                sqlUpdate.Parameters.AddWithValue("@location", updateText);
                sqlUpdate.Parameters.AddWithValue("@id", ID);


                sqlUpdate.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close(); // close the connection 
        }


    }
    
}
