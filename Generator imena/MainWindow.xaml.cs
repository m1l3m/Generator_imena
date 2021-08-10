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
using Microsoft.Win32;

namespace Generator_imena
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

        private void generisi_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                spisak_imena.Document.Blocks.Clear();
                generator();
                               

            }
            catch 
            {

                MessageBox.Show("Nešto si zajebo","Greška!",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            
        }

        private void generator()
        {
                string ime_prezime = "";
                string lista = "";
                string prezimena = @"C:\Users\kolfi\source\repos\Generator imena\Generator imena\Liste\prezimena.txt";
                if (muska.IsChecked == true)
                {
                    lista = @"C:\Users\kolfi\source\repos\Generator imena\Generator imena\Liste\imena_muska.txt";
                }
                if (zenska.IsChecked==true)
                {
                    lista = @"C:\Users\kolfi\source\repos\Generator imena\Generator imena\Liste\imena_zenska.txt";
                }

            int broj_redova = File.ReadAllLines(lista).Length;
            int broj_redova_prezime = File.ReadAllLines(prezimena).Length;
            Random random = new Random();
            int nasumicna = random.Next(0, broj_redova);            
            Random random_prezime = new Random();
            int nasumicno_prezime = random.Next(0, broj_redova_prezime);
            for (int i = 0; i < Convert.ToInt32(broj_imena.Text);)
            {
                nasumicna = random.Next(0, broj_redova);
                using (Stream stream = File.Open(lista, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string ime = null;
                        for (int j = 0; j < nasumicna+1; ++j)
                        {
                            ime = reader.ReadLine();
                        }
                        ime_prezime = ime; 
                    }
                    
                }
                nasumicno_prezime = random.Next(0, broj_redova_prezime);
                using (Stream stream = File.Open(prezimena, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        string prezime = null;
                        for (int j = 0; j < nasumicno_prezime+1; ++j)
                        {
                            prezime = reader.ReadLine();
                        }
                        ime_prezime = ime_prezime + " " + prezime+"\r";
                    }

                }

                if (ime_prezime!="")
                {
                    spisak_imena.AppendText(ime_prezime);
                    i++;
                }
                
            }
        }

        private void sacuvaj_Click(object sender, RoutedEventArgs e)
        {
            if (spisak_imena.Document.Blocks.Count>0)
            {
                SaveFileDialog sacuvaj = new SaveFileDialog();
                sacuvaj.Filter = "Text Files (*.txt)|*.txt";
                sacuvaj.ShowDialog();
                if (sacuvaj.FileName!="")
                {
                    TextRange range;
                    FileStream fStream;
                    range = new TextRange(spisak_imena.Document.ContentStart, spisak_imena.Document.ContentEnd);
                    fStream = new FileStream(sacuvaj.FileName, FileMode.Create);
                    range.Save(fStream, DataFormats.Text);
                    fStream.Close();
                }

            }
           
        }
    }
}
