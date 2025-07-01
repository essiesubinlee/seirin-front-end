using System;
using System.Collections.Generic;
using Microsoft.Maui.Controls;
using seirin1.Services;  // Adjust to your namespace where FirebaseService is

namespace seirin1.Pages
{
    public partial class DataTable : ContentPage
    {
        private readonly FirebaseService _firebaseService = new FirebaseService();

        public DataTable()
        {
            InitializeComponent();
            LoadData();
        }

        private async void LoadData()
        {
            try
            {
                var dataList = await _firebaseService.FetchWithCacheAsync();



                if (dataList.Count == 0)
                {
                    await DisplayAlert("No Data", "No solar data found.", "OK");
                }

                DataListView.ItemsSource = dataList;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load data: {ex.Message}", "OK");
            }
        }
    }
}