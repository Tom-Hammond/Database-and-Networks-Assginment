// See https://aka.ms/new-console-template for more information
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




DataBaseActions actions = new DataBaseActions();
actions.debug = true;
LoadDatabase();
if (args.Length == 0)
{
    
    Console.WriteLine("Starting Server");
    RunServer(actions.debug);
}
else
{
    for (int i = 0; i < args.Length; i++)
    {
        Console.WriteLine(args[i]);
    }
}

for (int i = 0; i < args.Length; i++)
{
    ProcessCommand(args[i]);
}



void LoadDatabase()
{
    
    string connStr = "server=localhost;user=root;database=users;port=3306;password=L3tM31n";

    using var connection = new MySqlConnection(connStr);
    {
       
        connection.Open();

        using var command = new MySqlCommand("SELECT * FROM userdetails;", connection);
        using var reader = command.ExecuteReader();
        while (reader.Read())
        {

            string loginID = (string)reader.GetValue(0);

            User recoredOfUser = new User();
            recoredOfUser = new User
            {
              //  LoginID = (string)reader.GetValue(0),
                UserID = (string)reader.GetValue(1),
                Surname = (string)reader.GetValue(2),
                Fornames = (string)reader.GetValue(3),
                Title = (string)reader.GetValue(4),
                Position = (string)reader.GetValue(5),
                Phone = (string)reader.GetValue(6),
                Email = (string)reader.GetValue(7),
                Location = (string)reader.GetValue(8)
            };

            actions.DataBase.Add(loginID, recoredOfUser);
        }

        connection.Close();


    }
}

void RunServer(bool debug)
{
    TcpListener listener;
    Socket connection;
    NetworkStream socketStream;
    try
    {
        listener = new TcpListener(IPAddress.Any, 43);
        listener.Start();
        while (true)
        {
            if (debug)
            {
                Console.WriteLine("Server Waiting connection...");
            }
            connection = listener.AcceptSocket();
            //connection.SendTimeout = 5000;
            //connection.ReceiveTimeout = 5000;
            socketStream = new NetworkStream(connection);
            doRequest(socketStream, debug);
            socketStream.Close();
            connection.Close();
            

        }
    }
    catch (Exception e)
    {
        Console.WriteLine("Exception: " + e.ToString()); 
    }
    if (debug)
    {
        Console.WriteLine("Terminating Server");
    }

}

void doRequest(NetworkStream socketStream, bool debug)
{

    if (debug)
    {
        Console.WriteLine("Waiting for input from client...");
    }
    StreamWriter sw = new StreamWriter(socketStream);
    StreamReader sr = new StreamReader(socketStream);
   
    String line = sr.ReadLine();
    String[] slices = line.Split(" ");
    String ID = slices[1].Substring(7);
    String result = null;

    string outputString = null;

   


    try
    {

        sw.WriteLine($"Received Network Command: '{line}'");

       sw.WriteLine(line);   // Need to remove this line after testing
        sw.Flush();           // Need to remove this line after testing   
        if (line == null)
        {
            if (debug) Console.WriteLine("Ignoring null command");
            return;
        }
        else if (line.StartsWith("POST /") && line.EndsWith(" HTTP/1.1"))
        {

            // The we have an update
            if (debug)
            {
                Console.WriteLine("Received an update request");
            }

            
            sw.WriteLine("Received an update request");


            outputString = actions.Update(ID, result, line);
            sw.WriteLine(outputString);
            sw.Flush();

        }
        else if (line.StartsWith("GET /?name=") && line.EndsWith(" HTTP/1.1"))
        {
            // then we have a lookup
            if (debug) Console.WriteLine("Received a lookup request");

            sw.WriteLine("Received a lookup request");
            outputString = actions.Lookup(ID, "location");
            sw.WriteLine(outputString);
            sw.Flush();


        }
        else
        {
            sw.WriteLine("HTTP/1.1 400 Bad Request");
            sw.WriteLine("Content-Type: text/plain");
            sw.WriteLine();
            sw.Flush();

        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Fault in Command Processing: {e.ToString()}");


    }
    finally
    {
        sw.Close();
        sr.Close();

    }

    

    if (slices.Length < 2 || slices[0] != "name=" || slices[1] != "location=")
    {
        // This is an invalid request
        sw.WriteLine("HTTP/1.1 400 Bad Request");
        sw.WriteLine("Content-Type: text/plain");
        sw.WriteLine();
        sw.Flush();
        Console.WriteLine($"Unrecognised command: '{line}'");
        return;
    }



    if (line.Contains(ID))
    {
        result = line.Contains(ID).ToString();
        sw.WriteLine("HTTP/1.1 200 OK");
        sw.WriteLine("Content-Type: text/plain");
        sw.WriteLine();
        sw.WriteLine(result);
        sw.Flush();
        Console.WriteLine($"Performed Lookup on '{ID}' returning '{result}'");
    }

    else
    {
        sw.WriteLine("HTTP/1.1 404 Not Found");
        sw.WriteLine("Content-Type: text/plain");
        sw.WriteLine();
        sw.Flush();
        Console.WriteLine($"Performed Lookup on '{ID}' returning '404 Not Found'");
    }

    if (slices.Length == 2)
    {
        string operation = slices[1];
        // Could be empty if no field specified
        if (operation == "")
        {
            // Is a record delete command
            actions.Delete(ID);
            return;
        }


    }
}

/// Process the next database command request
void ProcessCommand(string command)
{
    string output = null;
    if (actions.debug) Console.WriteLine($"\nCommand: {command}");
    try
    {
        String[] slice = command.Split(new char[] { '?' }, 2);
        String ID = slice[0];
        String operation = null;
        String update = null;
        String field = null;
        if (slice.Length == 2)
        {
            operation = slice[1];
            String[] pieces = operation.Split(new char[] { '=' }, 2);
            field = pieces[0];
            if (pieces.Length == 2) update = pieces[1];
        }
        if (actions.debug) Console.Write($"Operation on ID '{ID}'");
        if (operation == null) actions.Dump(ID);
        else if (update == null) actions.Lookup(ID, field);
        else actions.Update(ID, field, update);

    }
    catch (Exception e)
    {
        Console.WriteLine($"Fault in Command Processing: {e.ToString()}");
    }
    
}



