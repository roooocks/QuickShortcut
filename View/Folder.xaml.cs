using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;

namespace QuickShortcut
{
    /// <summary>
    /// Folder.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Folder : Window
    {
        public Folder()
        {
            InitializeComponent();
        }

        public Folder(string test)
        {
            InitializeComponent();
            print_data.Text = test;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var p = new Process();
            if (print_data.Text == "Popup 1")
            {
                p.StartInfo = new ProcessStartInfo(@"D:\\Memo\\기획서\\게임\\미정 (생존겜)\\공통\\공통.docx")
                { UseShellExecute = true };
            }
            else
            {
                p.StartInfo = new ProcessStartInfo(@"D:\\text123.txt")
                { UseShellExecute = true };
            }

            p.Start();
        }
    }
}
