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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TesteTécnicoIdeal.WPF.Models;
using TesteTécnicoIdeal.WPF.Service;
using TesteTécnicoIdeal.WPF.Views;

namespace TesteTécnicoIdeal.WPF
{
    public partial class MainWindow : Window
    {
        private readonly ApiService _apiService;
        List<User_Model> usersList = new List<User_Model>();
        User_Model selectedUser;

        public MainWindow(ApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
            try
            {
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao se conectar com a API.");
            }

        }

        private async Task RefreshList()
        {
            usersList = await _apiService.GetAllUsers();
            gridCadastro.ItemsSource = usersList;
        }
        private void gridCadastro_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedUser = (User_Model) gridCadastro.SelectedItem;
        }

        private async void btnBuscar_Click(object sender, RoutedEventArgs e)
        {
            var selectedIdText = txtUserId.Text;

            if (string.IsNullOrWhiteSpace(selectedIdText) || !int.TryParse(selectedIdText, out int selectedId))
            {
                MessageBox.Show("Id inserido inválido.");
            }
            else
            {
                try
                {
                    var user = await _apiService.GetUserById(selectedId);
                    usersList = new List<User_Model> { user };
                    gridCadastro.ItemsSource = usersList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }
        private async void btnAtualizar_Click(object sender, RoutedEventArgs e)
        {
            await RefreshList();
            txtUserId.Text = "Buscar por Id";
        }

        private async void btnAdicionar_Click(object sender, RoutedEventArgs e)
        {
            AddUserWindow addUserWindow = new AddUserWindow(_apiService);
            addUserWindow.ShowDialog();
            await RefreshList();
        }

        private async void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("Você deve selecionar um cadastro para atualizar.");
            }
            else
            {
                UpdateUserWindow updateUserWindow = new UpdateUserWindow(_apiService, selectedUser);
                updateUserWindow.ShowDialog();
                await RefreshList();
            }

        }

        private async void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            if (selectedUser == null)
            {
                MessageBox.Show("Você deve selecionar um cadastro para deletar.");
            }
            else
            {
                DeleteUserWindow deleteUserWindow = new DeleteUserWindow(_apiService, selectedUser.Id);
                deleteUserWindow.ShowDialog();
                await RefreshList();
            }
        }

        private void txtUserId_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        private void txtUserId_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUserId.Text = string.Empty;
        }

        private void txtUserId_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserId.Text))
            {
                txtUserId.Text = "Buscar por Id";
            }
        }
    }
}