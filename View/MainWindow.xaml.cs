using System.Runtime.InteropServices;
using System.Windows;
using QuickShortcut.ModelView;

namespace QuickShortcut
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // 화면에 사용할 데이터 셋팅
            DataContext = new MainViewModel();

            InitializeComponent();

            // 프로그램 최대 크기 == 사용자 모니터 사이즈
            this.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            // 화면 위치 셋팅
            // 중앙 + [상/좌/우 중 택1]로 위치를 바꿀 수 있다.
            // 나중에는 설정을 통해 위치를 바꿀 수 있어야 한다. 당장 할게 아니라서 하드 코딩
            this.Top = 10;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Show_MessageBox("Loaded");
            Check_Width();
        }

        // 호출 순서는 "사이즈 변경됨 -> Window_SizeChanged 호출" 이다.
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Show_MessageBox("SizeChanged");
            Check_Width();
        }

        // 사이즈 계산
        private void Check_Width()
        {
            this.Left = (SystemParameters.PrimaryScreenWidth / 2) - (this.ActualWidth / 2);
        }

        // 디버깅용
        private void Show_MessageBox(string text)
        {
            MessageBox.Show(text);
        }
    }
}