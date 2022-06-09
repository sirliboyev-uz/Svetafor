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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Svetafor
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer; // vaqt
        bool startStop = true; // boshlash va to'xtash uchun o'zgaruvchi
        private int avtomobilSvetaforVaqti = 40; // avtomobillar uchun svetafor
        private int piyodaSvetaforningRangiAlmashishi; // piyoda svetaforining rangini almashtirish
        private int piyodalarUchunSvetaforVaqti = 20; // piyodalar uchun svetafor vaqti
        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(vaqtniHisoblash);
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1); // new TimeSpan(0, 0, 0, 0, 0);

            avtomobilQizilChiroqVaqtiTxt.Visibility = Visibility.Hidden;
            avtomobilSariqChiroqVaqtiTxt.Visibility = Visibility.Hidden;
            avtomobilYashilChiroqVaqtiTxt.Visibility = Visibility.Hidden;

            piyodaQizilChiroqVaqtiTxt.Visibility = Visibility.Hidden;
            piyodaYashilChiroqVaqtiTxt.Visibility = Visibility.Hidden;
        }
        private void vaqtniHisoblash(object sender, EventArgs e)
        {
            avtomobilQizilChiroqVaqtiTxt.Content = String.Format("{0:00}", avtomobilSvetaforVaqti);
            avtomobilQizilChiroqVaqtiTxt.Visibility = Visibility.Visible;
            avtomobilSvetaforVaqti--;
            piyodalarUchunSvetaforVaqti--;

            piyodaYashilChiroqVaqtiTxt.Visibility = Visibility.Visible;
            piyodaYashilChiroqVaqtiTxt.Content = String.Format("{0:00}", piyodalarUchunSvetaforVaqti);
            piyodaSvetaforningRangiAlmashishi = avtomobilSvetaforVaqti;

            Automobile_Red.Fill = Brushes.Red;
            Pedestrian_Green.Fill = Brushes.Green;

            if (avtomobilSvetaforVaqti < 35)
            {
                story_Automobile_Red.Begin();
                if (avtomobilSvetaforVaqti < 30)
                {
                    Automobile_Red.Fill = Brushes.White;
                    Automobile_Orange.Fill = Brushes.Yellow;

                    avtomobilQizilChiroqVaqtiTxt.Visibility = Visibility.Hidden;
                    avtomobilSariqChiroqVaqtiTxt.Visibility = Visibility.Visible;
                    avtomobilSariqChiroqVaqtiTxt.Content = avtomobilQizilChiroqVaqtiTxt.Content;
                    if (avtomobilSvetaforVaqti < 25)
                    {
                        story_Automobile_Orange.Begin();
                        story_Pedestrian_Green.Begin();
                        if (piyodaSvetaforningRangiAlmashishi < 22)
                        {
                            piyodaQizilChiroqVaqtiTxt.Content = String.Format("{0:00}", piyodaSvetaforningRangiAlmashishi);

                            Pedestrian_Green.Fill = Brushes.White;
                            Pedestrian_Red.Fill = Brushes.Red;
                            piyodaYashilChiroqVaqtiTxt.Visibility = Visibility.Hidden;
                            piyodaQizilChiroqVaqtiTxt.Visibility = Visibility.Visible;
                            if (avtomobilSvetaforVaqti < 20)
                            {
                                Automobile_Orange.Fill = Brushes.White;
                                Automobile_Green.Fill = Brushes.Green;

                                avtomobilSariqChiroqVaqtiTxt.Visibility = Visibility.Hidden;
                                avtomobilYashilChiroqVaqtiTxt.Visibility = Visibility.Visible;
                                avtomobilYashilChiroqVaqtiTxt.Content = avtomobilSariqChiroqVaqtiTxt.Content;
                                if (avtomobilSvetaforVaqti < 5)
                                {
                                    story_Automobile_Green.Begin();
                                    story_Pedestrian_Red.Begin();
                                    if (avtomobilSvetaforVaqti < 0)
                                    {
                                        Automobile_Green.Fill = Brushes.White;
                                        Automobile_Red.Fill = Brushes.Red;
                                        avtomobilSvetaforVaqti = 40;
                                        avtomobilQizilChiroqVaqtiTxt.Visibility = Visibility.Visible;
                                        avtomobilYashilChiroqVaqtiTxt.Visibility = Visibility.Hidden;
                                        if (piyodaSvetaforningRangiAlmashishi > -2)
                                        {
                                            Pedestrian_Red.Fill = Brushes.White;
                                            Pedestrian_Green.Fill = Brushes.Green;
                                            piyodaQizilChiroqVaqtiTxt.Visibility = Visibility.Hidden;
                                            piyodalarUchunSvetaforVaqti = 20;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        private void btn_Start_Stop_Click(object sender, RoutedEventArgs e)
        {
            Automobile_Red.Fill = Brushes.White;
            Automobile_Orange.Fill = Brushes.White;
            Automobile_Green.Fill = Brushes.White;

            Pedestrian_Red.Fill = Brushes.White;
            Pedestrian_Green.Fill = Brushes.White;

            if (startStop == true)
            {
                dispatcherTimer.Start();

                startStop = false;
                btn_Start_Stop.Content = "STOP";
                btn_Start_Stop.Background = System.Windows.Media.Brushes.Red;
            }
            else if (startStop == false)
            {
                dispatcherTimer.Stop();

                startStop = true;
                btn_Start_Stop.Content = "START";
                btn_Start_Stop.Background = System.Windows.Media.Brushes.GreenYellow;
            }
        }

    }
}
