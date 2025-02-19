using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using QuickShortcut.Model;
using QuickShortcut.ModelView.Util;
using QuickShortcut.ModelView.Command;
using QuickShortcut.View;

namespace QuickShortcut.ModelView
{
    class MainViewModel : Notifier
    {
        private readonly int cnt = 5; // 화면에 보여줄 최대 숏컷 개수. 프로토타입때는 10개로 늘린다.
        private readonly int listCnt = ShortCut.ShortCuts.Count;

        private List<ShortCut> shortcuts = default!;
        private int page = 1;

        public List<ShortCut> ShortCuts
        {
            get
            {
                return shortcuts;
            }
            set
            {
                shortcuts = value;
                OnPropertyChanged(nameof(ShortCuts));
            }
        }
        public ICommand ShortCutCommand { get; set; }
        public ICommand LeftRightCommand { get; set; }
        public ICommand AppExitCommand { get; set; }
        public ICommand AppSettingCommand {  get; set; }

        public MainViewModel()
        {
            ShortCuts = ShortCut.ShortCuts[((page - 1) * cnt)..(page * cnt)];

            // 커맨드 하나로 통합해서 Util 폴더로 이동시키기
            // 파라미터가 아예 필요없는 경우가 있으므로 하나로 합칠때 잘 좀 하자;;
            ShortCutCommand = new ShortCutCommand(ShortCutExecuteMethod, ShortCutCanExecuteMethod);
            LeftRightCommand = new LeftRightCommand(LeftRightExecuteMethod);
            AppExitCommand = new LeftRightCommand(AppExitExecuteMethod); // 임시 커맨드
            AppSettingCommand = new LeftRightCommand(AppSettingExecuteMethod); // 임시 커맨드
        }

        /** 커맨드 함수 **/
        /* 숏컷별 클릭 이벤트(ShortCutCommand) */
        private void ShortCutExecuteMethod(object param)
        {
            var values = (object[])param;

            // 두 변수 전부 내용이 있는지와 제대로 된 내용인지를 확인해야 한다.
            char type = (char)values[0]; // 숏컷 타입
            string path = (string)values[1]; // 현재는 파일의 경로와 폴더 경로, 가상 폴더의 제목을 동시에 쓰고 있다. 이것도 분리하던가 이름을 바꿔야 한다.

            switch (type)
            {
                case '1': // 앱 실행
                case '2': // 파일 탐색기 열기
                    {
                        using (var p = new Process())
                        {
                            p.StartInfo = new ProcessStartInfo(path)
                            { UseShellExecute = true };
                            p.Start();
                        }
                    }
                    break;
                case '3': // 가상 폴더(팝업) 열기
                    {
                        // 해당 가상 폴더(팝업)에서 실행 목록을 가져오는건 나중으로 한다.
                        Folder fr = new("Popup 2");
                        fr.Show();
                    }
                    break;
                default:
                    break;
            }

            // 디버깅
            // MessageBox.Show($"해당 숏컷의 타입은 {type}이고 경로는 {path}입니다.");
        }

        private bool ShortCutCanExecuteMethod(object param)
        {
            return true;
        }

        /* 숏컷 목록 좌우 이동 이벤트(n개씩, 현재는 5개씩 고정, LeftRightCommand) */
        // 계속 메모리가 늘어나니 코드 수정할 때 ShortCut의 데이터 가져오는 부분부터 고쳐라!
        private void LeftRightExecuteMethod(object param)
        {
            var value = (string)param;

            if (value == "Left")
            {
                if (page > 1)
                {
                    page -= 1;
                    ShortCuts = ShortCut.ShortCuts[((page - 1) * cnt)..(page * cnt)];
                }
                else
                {
                    // 버튼 비활성화가 가능하다면 하기 (커맨드 방식도 ok인데, 우선 버튼이 비활성화 됫다는걸 보여줘야 한다.)
                    MessageBox.Show("왼쪽은 더이상 숏컷이 없습니다.");
                }
            }
            else if (value == "Right")
            {
                if (page < Math.Ceiling(listCnt * 0.2))
                {
                    page += 1;
                    if (page == Math.Ceiling(listCnt * 0.2))
                    {
                        // 마지막 페이지 전체 출력
                        ShortCuts = ShortCut.ShortCuts[((page - 1) * cnt)..];
                    }
                    else
                    {
                        ShortCuts = ShortCut.ShortCuts[((page - 1) * cnt)..(page * cnt)];
                    }
                }
                else
                {
                    MessageBox.Show("오른쪽은 더이상 숏컷이 없습니다.");
                }
            }
            else
            {
                MessageBox.Show("코드가지고 장난치지 마세요!");
            }
        }

        /* NotifyIcon의 종료 버튼 이벤트(AppExitCommand) */
        private void AppExitExecuteMethod(object param)
        {
            Application.Current.Shutdown();
        }

        /* 설정 팝업 띄우기(AppSettingCommand) */
        private void AppSettingExecuteMethod(object param)
        {
            Setting st = new();
            st.ShowDialog();
        }
    }
}
