using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace smile
{
	public partial class MainPage : ContentPage
	{

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != this.MinimumWidthRequest || height != this.MinimumHeightRequest)
            {
                this.MinimumWidthRequest = width;
                this.MinimumHeightRequest = height;
                if (width > height)
                {
                    MainStack.Orientation = StackOrientation.Horizontal;
                }
                else
                {
                    MainStack.Orientation = StackOrientation.Vertical;
                }
            }
        }

        public MainPage()
        {
            InitializeComponent();

            Label labelTapped = new Label
            {
                Text = "This smile has only eyes. Tap any yeelow boxes to draw a Red mouth. Or tap  again for Yellow.",
                FontSize = 20
            };

            void OnButtonClicked(object sender, System.EventArgs e)
            {
                Button button = (Button)sender;
                if (button.BackgroundColor == Color.White || button.BackgroundColor == Color.Black) return;
                button.BackgroundColor = (button.BackgroundColor == Color.Red) ? Color.Yellow : Color.Red;
            }

            Grid grid = new Grid();
            grid.RowSpacing = 1;
            grid.ColumnSpacing = 1;
            int cols = 17;
            int rows = 17;

            for (int i = 0; i < rows; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            }
            for (int i = 0; i < cols; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
            }

            Color boxViewColor = Color.White;
            int initYellow = 5;

            int YellowStart = 6;
            int YellowMinInRow = initYellow;//init value
            int YellowRows = 0;//init value

            int iRows = 0;
            int yellowstring = initYellow;


            while (iRows < rows)
            {
                for (int j = 0; j < cols; j++)
                {
                    yellowstring = YellowMinInRow + YellowStart;

                    if (j >= YellowStart && j < yellowstring)
                    {
                        boxViewColor = Color.Yellow;
                    }
                    else
                    {
                        boxViewColor = Color.White;
                    }
                    //eyes
                    if (iRows == 7 && j == 5 || iRows == 7 && j == 11)
                    {
                        boxViewColor = Color.Black;
                    }
                    Button box = new Button();
                    box.BackgroundColor = boxViewColor;
                    box.Clicked += OnButtonClicked;
                    grid.Children.Add(box, j, iRows);
                }
                //5 rows with yeelow strings/no white
                if (yellowstring == cols)
                {
                    YellowRows++;
                }
                //rows in the top / before 5 yeelow strings
                if (YellowRows == 0)
                {
                    YellowMinInRow += 2;
                    YellowStart--;
                }
                //rows in bottom / after 5 yyellow strings
                if (YellowRows >= initYellow)
                {
                    YellowMinInRow -= 2;
                    YellowStart++;
                }
                iRows++;
            }
            MainStack.Children.Add(grid);
            MainStack.Children.Add(labelTapped);
        }

    }
}
