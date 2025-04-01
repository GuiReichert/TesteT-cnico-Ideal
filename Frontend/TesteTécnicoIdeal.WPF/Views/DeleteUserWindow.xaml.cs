using System;
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
using TesteTécnicoIdeal.WPF.Service;

namespace TesteTécnicoIdeal.WPF.Views
{
    public partial class DeleteUserWindow : Window
    {
        private ApiService _apiService;
        private int _userId;

        public DeleteUserWindow(ApiService apiService, int userId)
        {
            InitializeComponent();
            _apiService = apiService;
            _userId = userId;
        }

        private async void btnConfirmar_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                await _apiService.DeleteUser(_userId);
                MessageBox.Show("Cadastro deletado com sucesso.");
                this.Close();
            }
            catch (Exception ex) 
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
