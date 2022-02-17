using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinApp.Models
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    
    }
}
