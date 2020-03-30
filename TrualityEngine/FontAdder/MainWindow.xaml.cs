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
using System.IO;

namespace FontAdder
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> AllFont { get; } = new List<string>();
        private List<Button> AllButtons { get; } = new List<Button>();
        private string _Select;
        public string Select
        {
            get => _Select;
            set
            {
                _Select = value;
                SelectedLabel.Content = $"Select:{_Select}";
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            foreach(var font in System.Drawing.FontFamily.Families.Where(item=>item.Name.Contains("Bahnschrift ")&&item.Name.Contains("HoloLens")==false))
            {
                if(font.Name!=String.Empty)
                {
                    Button button = new Button();
                    button.Click += Button_Click;
                    button.Content = font.Name;
                  
                    button.Background = new SolidColorBrush(Colors.Transparent);
                    button.BorderBrush = new SolidColorBrush(Colors.Black);
                    StackAllFont.Children.Add(button);
                    AllButtons.Add(button);
                }
                
            }



        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Select = (sender as Button).Content.ToString();
          
        }

        
        private bool AddFont(string font,string contentFolder)
        {
            string name = font.Replace(' ', '_');
            string newPath = System.IO.Path.Combine(contentFolder, $"{name}.spritefont");

            if (File.Exists(newPath))
                return false;
            string xml = File.ReadAllText("Template.txt");
            string contentPath = System.IO.Path.Combine(contentFolder, $"Content.mgcb");
            xml =xml.Replace("FONTHERE", font);         
            File.WriteAllText(newPath, xml);
            File.WriteAllText(contentPath, File.ReadAllText(contentPath) + $"\n#begin {name}.spritefont\n" +
@"/" + "importer:FontDescriptionImporter\n" +
@"/" + "processor:FontDescriptionProcessor\n" +
@"/" + "processorParam:PremultiplyAlpha=True\n" +
@"/" + "processorParam:TextureFormat=Compressed\n" +
@"/" + $"build:{name}.spritefont\n");
            return true;
            
        }

      

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (AllFont.Find(item => item == Select) == null && Select != String.Empty)
            {
                Label label = new Label
                {
                    Margin = new Thickness(0, -5, 0, 0),
                    Content = Select
                };
                StackLeft.Children.Add(label);
                AllFont.Add(Select);
                StackAllFont.Children.Remove( AllButtons.Find(item => (string)item.Content == Select));
                Select = String.Empty;
              
            }
          
          
          
        }

        private void ButtonFinish_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Show the content folder","Warning",MessageBoxButton.OK,MessageBoxImage.Information);
            StringBuilder noAddedFonts = new StringBuilder();
            using (System.Windows.Forms.FolderBrowserDialog openFileDialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {

                    string path = openFileDialog.SelectedPath;
                    foreach (string font in AllFont)
                        if (AddFont(font, path) == false)
                            noAddedFonts.Append($"{font},");

                }
            }

            if (noAddedFonts.ToString() != String.Empty)
                MessageBox.Show($"{ noAddedFonts.ToString().Remove(noAddedFonts.Length - 1)},can not be added, beacuse was already added", "No added few fonts", MessageBoxButton.OK, MessageBoxImage.Information);




        this.Close();

        }
    }
}
