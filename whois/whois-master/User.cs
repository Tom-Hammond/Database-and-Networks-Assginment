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


namespace whois
{

    public class User
    {
        public User() { }
        public String UserID { get; set; }
        public String Surname { get; set; }
        public String Fornames { get; set; }
        public String Title { get; set; }
        public String Position { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Location { get; set; }
        public User(
             String userID,
             String surname,
             String fornames,
             String title,
             String position,
             String phone,
             String email,
             String location)
        {
            UserID = userID;
            Surname = surname;
            Fornames = fornames;
            Title = title;
            Position = position;
            Phone = phone;
            Email = email;
            Location = location;

        }
    };





}

