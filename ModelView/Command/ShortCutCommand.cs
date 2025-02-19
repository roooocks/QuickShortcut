using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

/* 나중에 Command<T> 방식으로 여러 커맨드를 하나로 합치기 */
namespace QuickShortcut.ModelView.Command
{
    internal class ShortCutCommand : ICommand
    {
        /**
         * C#의 함수 포인터 비스므리한 개념
         *  - Delegate
         *      - delegate 반환_타입 이름(파라미터 설정) 방식으로 사용한다.
         *      - 내부적으로 .NET Delegate / MulticastDelegate 클래스를 사용한다.
         *          - 이 클래스가 지원하는 속성 (예: .Method - 함수 Prototype을 보여줌)과 메서드 (예: GetInvokcationList())를 모두 사용할 수 있다
         *  - Action
         *      - 반환값이 없는 delegate
         *      - 1~16개까지의 파라미터가 존재한다. (ex. Action<string>이면 파라미터가 string 타입)
         *  - Func
         *      - 반환값이 있는 delegate
         *      - 반환값의 타입이 늘 마지막에 들어간다. 
         *          - ex. Func<string>이면 파라미터가 없으면서 string을 반환
         *          - ex. Func<object, string>이면 파라미터가 object 타입이면서 string을 반환
         *      - 1~16개까지의 파라미터가 존재한다.
         *      - .NET 3.5 부터는 LINQ 등도 지원
         *  - Predicate
         *      - 반환값이 있는 delegate
         *      - 반환값의 타입이 늘 bool 타입이다.
         *          - Predicate<string> == Func<string, bool>
         *      - Generic으로 들어가는 타입들은 늘 파라미터를 가리키며, 1개만 사용할 수 있다.
         *      - .NET 2.0 부터 Array, List 등을 지원하기 위해 도입
         */
        Action<object> _execute;
        Predicate<object> _canExecute;

        ShortCutCommand() { }

        public ShortCutCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /* ICommand 순서 => CanExecute 메소드 호출 > CanExecute의 상태가 변경 > CanExecuteChanged 이벤트가 발생 > WPF는 CanExecute를 호출 > Command에 연결된 컨트롤의 상태를 변경 */
        // 사용자 정의 명령은 키보드, 마우스 등을 통해 발생하는 CanExecute 함수가 정상적으로 작동하지 않는게 대부분이다.
        // 이때 아래의 이벤트를 CommandManager의 RequerySuggested 이벤트에 연결하면 정상적으로 작동한다.
        /*public event EventHandler CanExecuteChanged
         * {
         *   add { CommandManager.RequerySuggested += value; }
         *   remove { CommandManager.RequerySuggested -= value; } 
         * }
         */
        public event EventHandler? CanExecuteChanged;

        // 커맨드 활성화, 비활성화
        // Execute 함수를 실행할건지(true 반환) 안할건지(false 반환)을 기술
        // 만약 CanExecute함수가 작동해 상태가 변경되면 CanExecuteChanged 이벤트를 호출하게 된다.
        public bool CanExecute(object? parameter)
        {
            return _canExecute(parameter);
        }

        // 커맨드 호출 시 실제 처리해야 하는 작업을 실행
        public void Execute(object? parameter)
        {
            // _execute(parameter as string); // 만약 값이 string으로 전달되어야 한다면 이런식으로 사용한다.
            // _execute.Invoke(parameter);
            _execute(parameter);
        }
    }
}
