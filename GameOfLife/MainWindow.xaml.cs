using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Button[,] SpaceMatrix { get; set; }
        public int Size { get; set; }
        public DispatcherTimer LifeTimer { get; set; }
        public MainWindow()
        {
            LifeTimer = new DispatcherTimer();
            LifeTimer.Interval = new TimeSpan(0, 0, 1);
            LifeTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            Size = 20;
            SpaceMatrix = new Button[Size, Size];
            InitializeComponent();

            Grid myGrid = new Grid();
            myGrid.Width = 400;
            myGrid.Height = 400;
            myGrid.ShowGridLines = false;

            Button tempButton;
            for (int i = 0; i < Size; i++)
            {
                myGrid.ColumnDefinitions.Add(new ColumnDefinition());
                myGrid.RowDefinitions.Add(new RowDefinition());
                for (int j = 0; j < Size; j++)
                {
                    tempButton = new Button();
                    tempButton.Background = Brushes.LightGray;
                    Grid.SetColumnSpan(tempButton, 1);
                    Grid.SetRow(tempButton, i);
                    Grid.SetColumn(tempButton, j);

                    myGrid.Children.Add(tempButton);
                    SpaceMatrix[i, j] = tempButton;
                }
            }

            myGrid.RowDefinitions.Add(new RowDefinition());
            TextBox Instructions = new TextBox();
            Instructions.Text = "<ENTER> to start/stop, <C> to clear";
            Instructions.IsEnabled = false;
            myGrid.Children.Add(Instructions);
            Grid.SetColumnSpan(Instructions, 20);
            Grid.SetRow(Instructions, 21);

            this.Content = myGrid;
            this.Show();

            this.KeyDown += new KeyEventHandler(MainWindow_KeyDown);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            // Testing the event
            Brush[] brushes = new Brush[] {
                Brushes.AliceBlue,
                Brushes.AntiqueWhite,
                Brushes.YellowGreen,
                Brushes.Red
            };
            foreach (Button Space in SpaceMatrix)
            {
                Random rnd = new Random();
                Brush brush = brushes[rnd.Next(brushes.Length)];
                Space.Background = brush;
            }
        }

        void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key) {
                case Key.Enter:
                    if (LifeTimer.IsEnabled)
                    {
                        LifeTimer.Stop();
                    }
                    else
                    {
                        LifeTimer.Start();
                    }
                    break;
                case Key.C:
                    LifeTimer.Stop();
                    foreach (Button Space in SpaceMatrix)
                    {
                        Space.Background = Brushes.LightGray;
                    }
                    break;
            }
        }
    }
}
