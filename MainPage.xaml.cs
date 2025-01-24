namespace memory
{
    public partial class MainPage : ContentPage
    {
        List<string> aversy = new List<string>()
        {
            "jeden.png", "dwa.png", "dwa.png", "trzy.png", "jeden.png", "trzy.png"
        };
        List<ImageButton> buttons = new List<ImageButton>();
        List<string> odgadniete = new List<string>();
        Random random = new Random();
        public MainPage()
        {
            InitializeComponent();

            // Tworzenie Grid
            var grid = new Grid
            {
                RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = 300 }, // Auto
				new RowDefinition { Height = 300 }, // Więcej niż 1 gwiazdka
				new RowDefinition { Height = 300 } // Konkretna wartość
			},
                ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star },
                new ColumnDefinition { Width = GridLength.Star }
            },

                // Pozostałe właściwości
                BackgroundColor = Colors.LightGray,
                Padding = 10,
                RowSpacing = 5,
                ColumnSpacing = 5
            };

            // Dodawanie przycisków do Grid
            for (int row = 0; row < 2; row++) // 3 wiersze
            {
                for (int col = 0; col < 3; col++) // 3 kolumny
                {
                    int randomValueIndex = random.Next(0, aversy.Count - 1);
                    var button = new ImageButton
                    {
                        Source = "question.png",
                        ClassId = $"{aversy[randomValueIndex]}"
                    };
                    aversy.Remove(aversy[randomValueIndex]);
                    // Obsługa kliknięcia przycisku
                    button.Clicked += ButtonClicked;

                    // Dodawanie przycisku do Grid
                    grid.Add(button, col, row); // Można statycznie Add(button, 0, 0)
                }
            }

            // Dodawanie Gridu do StackLayout
            container.Children.Add(grid);
        }

        // Funkcja taka sama jak w button clicked w XAML
        private async void ButtonClicked(object sender, EventArgs e)
        {
            if(sender is ImageButton button)
            {
                button.Source = button.ClassId;

                buttons.Add(button);

                if(buttons.Count == 2)
                {
                    if (buttons[0].ClassId != buttons[1].ClassId)
                    {

                        await Task.Delay(500);
                        foreach (var przycisk in buttons)
                        {

                            przycisk.Source = "question.png";

                        }
                    }
                    else
                    {
                        odgadniete.Add(button.ClassId);
                    }
                    buttons.Clear();
                    
                }
            }
            if (odgadniete.Count == 3)
            {
                DisplayAlert("GRATY", "Wygrałeś giere bratku", "OK");
            }
        }
    }
}
