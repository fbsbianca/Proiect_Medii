using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinApp.Models;

namespace XamarinApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //Get All Persons  
            var userList = await App.SQLiteDb.GetUsersAsync();
            var inventoryList = await App.SQLiteDb.GeInventoriesAsync();

            if (userList != null)
            {
                lstUser.ItemsSource = userList;
                lstInventory.ItemsSource = inventoryList;

            }
        }


        

         
        private async void BtnAdd_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtName.Text))
            {
                User user = new User()
                {
                    FirstName = txtName.Text,
                    LastName= txtLastName.Text
                };

                //Add New Person  
                await App.SQLiteDb.SaveUserAsync(user);
                

                Inventory inventory = new Inventory()
                {
                    CoinName = txtNameCoin.Text,
                    Quantity = txtQuantity.Text,
                    UserID = user.UserID
                };
                await App.SQLiteDb.SaveInventoryAsync(inventory);

                txtName.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtNameCoin.Text = string.Empty;
                txtQuantity.Text = string.Empty;

                await DisplayAlert("Success", "Comanda cripto a User-ului a fost adaugata cu Success", "OK");
                //Get All Persons  
                var userList = await App.SQLiteDb.GetUsersAsync();
                var inventoryList = await App.SQLiteDb.GeInventoriesAsync();
                if (userList != null && inventoryList!= null)
                {
                    lstUser.ItemsSource = userList;
                    lstInventory.ItemsSource = inventoryList;

                }
            }
            else
            {
                await DisplayAlert("Required", "Va rog introduceti un nume!", "OK");
            }
        }
        private async void BtnRead_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserId.Text))
            {
                //Get Person  
                var user = await App.SQLiteDb.GetUserAsync(Convert.ToInt32(txtUserId.Text));
                var inventory = await App.SQLiteDb.GetInventoryAsync(Convert.ToInt32(txtUserId.Text));

                if (user != null)
                {
                    txtName.Text = user.FirstName;
                    txtLastName.Text = user.LastName;
                    txtNameCoin.Text = inventory.CoinName;
                    txtQuantity.Text = inventory.Quantity;

                    await DisplayAlert("Success",
                                        "-------------------" + "\n"
                                        + "Nume: " + user.FirstName + "\n"
                                        + "------------------" + "\n"
                                        + "Prenume: " + user.LastName + "\n"
                                        + "-------------------" + "\n"
                                        + "Cod comanda: " + inventory.UserID + "\n"
                                        + "-------------------" + "\n"
                                        + "Nume crypto moneda: " + inventory.CoinName + "\n"
                                        + "-------------------" + "\n"
                                        + "Cantitate: " + inventory.Quantity + "\n"
                                        + "-------------------", "OK"); 
                                     
                }
            }
            else
            {
                await DisplayAlert("Required", "Please Enter UserID", "OK");
            }
        }
        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserId.Text))
            {
                User User = new User()
                {
                    UserID = Convert.ToInt32(txtUserId.Text),
                    FirstName = txtName.Text,
                    LastName= txtLastName.Text
                };
 
                Inventory Inventory = new Inventory()
                {
                    InventoryID = Convert.ToInt32(txtUserId.Text),
                    CoinName = txtNameCoin.Text,
                    Quantity = txtQuantity.Text
                };

                //Update User  
                await App.SQLiteDb.SaveUserAsync(User);
                await App.SQLiteDb.SaveInventoryAsync(Inventory);
               txtUserId.Text = string.Empty;
                txtName.Text = string.Empty;
                txtLastName.Text = string.Empty;
                txtQuantity.Text = string.Empty;    
                txtNameCoin.Text = string.Empty;    
                await DisplayAlert("Success", "Datele user-ului au fost actualizate cu success!", "OK");
                //Get All Users  
                var UserList = await App.SQLiteDb.GetUsersAsync();
                var InventoryList = await App.SQLiteDb.GeInventoriesAsync();
                if (UserList != null)
                {
                    lstUser.ItemsSource = UserList;
                    lstInventory.ItemsSource = InventoryList;   
                }

            }
            else
            {
                await DisplayAlert("Required", "Introduceti cod-ul user-ului!", "OK");
            }
        }
        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUserId.Text)) { 
            
                //Get Person  
                var user = await App.SQLiteDb.GetUserAsync(Convert.ToInt32(txtUserId.Text));
                var inventory= await App.SQLiteDb.GetInventoryAsync(user.UserID);

                if (user != null && inventory!=null)
                {
                    //Delete Person  
                    await App.SQLiteDb.DeleteInventoryAsync(inventory);

                    await App.SQLiteDb.DeleteUserAsync(user);
                    txtName.Text = string.Empty;
                    txtLastName.Text = string.Empty;
                    txtNameCoin.Text = string.Empty;
                    txtQuantity.Text = string.Empty;
                    await DisplayAlert("Success", "User Sters...", "OK");

                    //Get All Persons  
                    var userList = await App.SQLiteDb.GetUsersAsync();
                    var inventoryList = await App.SQLiteDb.GeInventoriesAsync();

                    if (userList != null)
                    {
                        lstUser.ItemsSource = userList;
                        lstInventory.ItemsSource = inventoryList;
                    }
                }
            }else
            {
                await DisplayAlert("Required", "Please Enter UserID", "OK");
            }
        }

    }
}
