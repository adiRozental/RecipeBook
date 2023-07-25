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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace myBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RecipeService _recipeService;

        public MainWindow()
        {
            InitializeComponent();
            _recipeService = new RecipeService();

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                var recipes = await _recipeService.GetAllRecipes();
                // Use the 'recipes' list to display data in your WPF application
                recipeDataGrid.ItemsSource = recipes;
                MessageBox.Show($"Error: ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);



            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during API communication
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void recipeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void DataGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            var recipes = await _recipeService.GetAllRecipes();
            // Use the 'recipes' list to display data in your WPF application
            recipeDataGrid.ItemsSource = recipes;



        }

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            var recipes = await _recipeService.GetAllRecipes();
            // Use the 'recipes' list to display data in your WPF application
            recipeDataGrid.ItemsSource = recipes;

        }

        private async void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           if (textBox.Text == null || _recipeService == null ) return;
           if (textBox.Text.Length == 0 )
            {
                button_Click(sender, e);
                return;

            }
            var recipes = await _recipeService.GetRecipesByKeyword(textBox.Text);
            // Use the 'recipes' list to display data in your WPF application
            recipeDataGrid.ItemsSource = recipes;
           
        }

        private void textBox_MouseLeave(object sender, MouseEventArgs e)
        {

        }
    }
}

