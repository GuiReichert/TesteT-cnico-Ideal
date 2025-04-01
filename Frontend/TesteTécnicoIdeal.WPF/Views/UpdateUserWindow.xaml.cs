using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TesteTécnicoIdeal.WPF.Models;
using TesteTécnicoIdeal.WPF.Service;

namespace TesteTécnicoIdeal.WPF.Views
{
    public partial class UpdateUserWindow : Window
    {
        private ApiService _apiService;
        private User_Model _oldUser;

        public UpdateUserWindow(ApiService apiService, User_Model oldUser)
        {
            InitializeComponent();
            _apiService = apiService;
            _oldUser = oldUser;

            txtId.Text = oldUser.Id.ToString();
            txtNome.Text = oldUser.Name;
            txtSobrenome.Text = oldUser.Surname;
            txtTelefone.Text = oldUser.PhoneNumber;
        }

        



        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text) || string.IsNullOrWhiteSpace(txtSobrenome.Text) || string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("Você deve preencher todos os campos.");
            }
            else if (!Regex.IsMatch(txtTelefone.Text, @"^\+?[0-9\s\-\(\)]{8,15}$"))
            {
                MessageBox.Show("Número de telefone inválido.");
            }
            else
            {
                var newUser = new User_Model
                {
                    Id = _oldUser.Id,
                    Name = txtNome.Text,
                    Surname = txtSobrenome.Text,
                    PhoneNumber = txtTelefone.Text
                };
                try
                {
                    await _apiService.UpdateUser(newUser);
                    MessageBox.Show("Atualização de cadastro realizada com sucesso.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
