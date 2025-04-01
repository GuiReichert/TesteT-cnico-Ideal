﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TesteTécnicoIdeal.WPF.DTO_s;
using TesteTécnicoIdeal.WPF.Models;
using TesteTécnicoIdeal.WPF.Service;

namespace TesteTécnicoIdeal.WPF.Views
{
    public partial class AddUserWindow : Window
    {
        private readonly ApiService _apiService;
        public AddUserWindow(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            var newUser = new UserDTO {
                Name = txtNome.Text,
                Surname = txtSobrenome.Text,
                PhoneNumber = txtTelefone.Text,
                };
            try
            {
                await _apiService.CreateUser(newUser);
                MessageBox.Show("Cadastro realizado com sucesso.");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
