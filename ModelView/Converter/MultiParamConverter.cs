using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace QuickShortcut.ModelView.Converter
{
    // https://devbong.tistory.com/41
    // ex. 바인딩 될 값이 1이면 A, 2면 B를 View단에 보여주고 값 자체는 그대로 1, 2를 사용하는 방식
    class MultiParamConverter : IMultiValueConverter
    {
        // ViewModel -> View
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length <= 0)
            {
                // 바인딩 시, 값이 잘못된 값일 때 반환값 설정이 되어있으면 설정된 값으로 반환시키는 역할
                return DependencyProperty.UnsetValue;
            }

            // 얕은 복사를 의미
            // 실제로 Clone 함수 설명을 보면 Shallow copy라는 단어가 보인다.
            return values.Clone();
        }

        // View -> ViewModel
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
