using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickShortcut.Model
{
    class ShortCut
    {
        public string Order { get; set; } = ""; // Grid 순서(당장은 필요 없는 방식)
        public string Icon { get; set; } = ""; // 숏컷 아이콘
        public char Type { get; set; } = '\0'; // 숏컷 타입(1: 파일 실행, 2: 파일 탐색기, 3: 가상 폴더)
        public string test { get; set; } = ""; // 1과2는 경로, 3은 가상 폴더 제목인데 나중에 바꿀 예정

        public override string ToString()
        {
            return $"Order: {Order}, Icon: {Icon}, Type: {Type}, Path: {test}";
        }

        // 테스트 후 *.txt파일에서 가져오는걸로 바꾸기
        // 앱 1, 폴더 2, 자체 폴더 3
        public static List<ShortCut> ShortCuts => new List<ShortCut>
        {
            new ShortCut{Order="0", Icon="../Image/icon/test.png", Type='1', test="C:\\Game Resource\\Tools\\HxD\\HxD.exe"},
            new ShortCut{Order="1", Icon="../Image/icon/icons8-windows-10-100.png", Type='3', test="중요 메모"},
            new ShortCut{Order="3", Icon="../Image/icon/icons8-windows-10-150.png", Type='2', test="C:\\Game Resource\\Mika Team\\Grils Frontline\\Android\\New"},
            new ShortCut{Order="2", Icon="../Image/icon/test.png", Type='2', test="C:\\Programming\\Study\\React\\pratice"},
            new ShortCut{Order="4", Icon="../Image/icon/icons8-windows-10-100.png", Type='1', test="C:\\Memo\\계정\\Github.txt"},

            new ShortCut{Order="2", Icon="../Image/icon/test.png", Type='2', test="C:\\Programming\\Study\\React\\pratice"},
            new ShortCut{Order="4", Icon="../Image/icon/icons8-windows-10-100.png", Type='1', test="C:\\Memo\\계정\\Github.txt"},
            new ShortCut{Order="0", Icon="../Image/icon/test.png", Type='1', test="C:\\Game Resource\\Tools\\HxD\\HxD.exe"},
            new ShortCut{Order="3", Icon="../Image/icon/icons8-windows-10-150.png", Type='2', test="C:\\Game Resource\\Mika Team\\Grils Frontline\\Android\\New"},
            new ShortCut{Order="1", Icon="../Image/icon/icons8-windows-10-100.png", Type='3', test="중요 메모"},

            new ShortCut{Order="0", Icon="../Image/icon/test.png", Type='1', test="C:\\Game Resource\\Tools\\HxD\\HxD.exe"},
        };

        // 숏컷 목록 가져오기
        /*var shortList = new List<string>();
        var sr = new StreamReader("Setting\\shortcut.txt");

        string line;
        while ((line = sr.ReadLine()) != null)
        {
            if (line[0] != '#')
            {
                line.Split('|');
            }
        }
        sr.Close();*/
    }
}
