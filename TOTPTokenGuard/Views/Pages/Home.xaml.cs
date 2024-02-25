﻿using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using TOTPTokenGuard.Core;
using TOTPTokenGuard.Core.Models;
using TOTPTokenGuard.Core.Security;
using TOTPTokenGuard.Views.UIComponents;

namespace TOTPTokenGuard.Views.Pages
{
    /// <summary>
    /// Interaktionslogik für Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        public Home()
        {
            InitializeComponent();

            Loaded += async (sender, e) => await LoadTokens();
        }

        private async Task LoadTokens()
        {
            try
            {
                List<TOTPTokenHelper>? tokenHelpers =
                    await TokenManager.GetAllTokens()
                    ?? throw new Exception("Error loading tokens (tokenHelpers is null)");
                foreach (var token in tokenHelpers)
                {
                    TokenCard card = new(token);
                    TOTPTokenContainer.Children.Add(card);
                }

                if (tokenHelpers.Count == 0)
                {
                    LoadingText.Visibility = Visibility.Collapsed;
                    LoadingProgressBar.Visibility = Visibility.Collapsed;
                    NoTokensText.Visibility = Visibility.Visible;
                    return;
                }

                LoadingInfo.Visibility = Visibility.Collapsed;
                TOTPTokenContainer.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading data: {ex.Message}");
            }
        }
    }
}
