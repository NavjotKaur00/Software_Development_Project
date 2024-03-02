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



namespace NavjotKaur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnAth_Click(object sender, RoutedEventArgs e)
        {
            using (var a = new BooksDBEntities())
            {
                var auth = a.Authors.Select
                    (ath => new {
                        ath.AuthorID,
                        ath.FirstName,
                        ath.LastName,
                        ath.City,
                        ath.State
                    }).ToList();

                grdBk1.ItemsSource = auth;

                grdBk1.Columns[0].Header = "Author ID";
                grdBk1.Columns[1].Header = "First Name";
                grdBk1.Columns[2].Header = "Last Name";

            }
        }

        private void btnPble_Click(object sender, RoutedEventArgs e)
        {
            using (var publ = new BooksDBEntities())
            {
                var p = publ.Publishers.Select
                    (pub => new { pub.PublisherID, pub.Name, pub.City, pub.State }).ToList();

                grdBk1.ItemsSource = p;

                grdBk1.Columns[0].Header = "Publisher ID";

            }
        }

        private void btnTt_Click(object sender, RoutedEventArgs e)
        {
            using (var titl = new BooksDBEntities())

            {
                var t = titl.Titles.Select
                    (title => new { title.TitleID, title.Title1, titlePublisher.Name, title.Price }).ToList();

                grdBk1.ItemsSource = t;

                grdBk1.Columns[0].Header = "Title ID";
                grdBk1.Columns[1].Header = "Title";
                grdBk1.Columns[2].Header = "Publisher";

            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var wl = new BooksDBEntities())
            {
                var publr = wl.Publishers.ToList();
                cmbForPuls.ItemsSource = publr;
                cmbForPuls.DisplayMemberPath = "Name";
                cmbForPuls.SelectedValuePath = "PublisherID";


                var auth = wl.Authors.ToList();
                cmbForAuh.ItemsSource = auth;
                cmbForAuh.DisplayMemberPath = "LastName";
                cmbForAuh.SelectedValuePath = "AuthorID";

            }
        }

        private void cmbForAuh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbForAuh.SelectedValue != null)
            {
                int AuthoID = (int)cmbForAuh.SelectedValue;
                using (var titl = new BooksDBEntities())
                {
                    var title = (from t in titl.Titles
                                where t.Authors. == AuthoID
                                select new
                                {//TitleID
                                    TitleID = t.TitleID,
                                    TitleName = t.Title1,
                                    Price = t.Price
                                }).ToList();
                    grdBk1.ItemsSource = title;
                    grdBk1.Columns[0].Header = "Title ID";

                }
            }
        }

        private void cmbForPuls_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbForPuls.SelectedValue != null)
            {
                int PublId = (int)cmbForPuls.SelectedValue;
                using (var pub = new BooksDBEntities())
                {
                    var p = (from pu in pub.Titles
                              where pu.PublisherID == PublId
                              select new
                              {
                                  pu.TitleID,
                                  pu.Title1,
                                  pu.Price
                              }).ToList();
                    grdBk1.ItemsSource = p;
                    grdBk1.Columns[0].Header = "Title ID";
                }
            }
        }

        private void btnCr_Click(object sender, RoutedEventArgs e)
        {
            cmbForAuh.SelectedIndex = -1;
            grdBk1.ItemsSource = null;
            cmbForPuls.SelectedIndex = -1;
            txtSeNa.Text = "";
        }

        private void btnSeByNa_Click(object sender, RoutedEventArgs e)
        {
            string name = txtSeNa.Text;
            using (var s = new BooksDBEntities())
            {
                var n = s.Titles.Where(a => a.Title1.Contains(name)).ToList();

                grdBk1.ItemsSource = n;

                grdBk1.Columns[0].Header = "Title ID";

            }
        }
    }
}