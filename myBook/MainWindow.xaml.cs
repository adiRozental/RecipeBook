using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using receiptBook.Models;


namespace myBook
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly RecipeService _recipeService;
        private readonly ImageService _imageService;

        public MainWindow()
        {
            InitializeComponent();
            _recipeService = new RecipeService();
            _imageService= new ImageService();

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

        private async void button1_ClickAsync(object sender, RoutedEventArgs e)
        {
            //Image1.Source = new BitmapImage(new Uri("C:\\Users\\shira\\OneDrive\\Desktop\\SemesterB\\AOOP&D\\Tar9\\src\\WS1\\ws1uml.png"));
            try
            {
                if(await _imageService.GetFoodImageUrlAsync("https://drive.google.com/drive/my-drive?q=parent:0AD_gZ1mIfsrqUk9PVA%20type:image", "green"))
                {
                    receiptBook.Models.Image image = new receiptBook.Models.Image
                    {
                        Id = 1,
                        Url="",
                        RecipeId = ((receiptBook.Models.Image)recipeDataGrid.SelectedItem).RecipeId
                    };
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }



        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                InitialDirectory = "C:\\Users\\shira\\Source\\Repos\\receiptBook\\myBook\\images\\", //Environment.CurrentDirectory, // Set the initial directory to your project's root folder
                Filter = "Image Files|*.jpg;*.png;*.bmp;*.jpeg|All Files|*.*",
                Title = "Select a Photo"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedFilePath = openFileDialog.FileName;

                // Check if the selected file exists
                if (File.Exists(selectedFilePath))
                {
                    // Load the selected image and display it
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(selectedFilePath);
                    image.EndInit();

                    // Assuming you have an Image control named "photoImage" in your XAML
                    Image1.Source = image;

                    // You can also save the selected file path for further use
                    // string selectedFilePath = openFileDialog.FileName;
                }
                else
                {
                    MessageBox.Show("The selected file does not exist.");
                }
            }
        }
    }
}

