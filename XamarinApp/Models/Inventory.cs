using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinApp.Models
{
    public class Inventory
    {
        [PrimaryKey, AutoIncrement]
        public int InventoryID { get; set; }
        public string CoinName { get; set; }
        public string Quantity { get; set; }

        [ForeignKey(typeof(User))]
        public int UserID { get; set; }
    }


}

