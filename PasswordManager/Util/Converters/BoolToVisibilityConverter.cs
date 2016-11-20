using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace PasswordManager.Util.Converters {
    public class BoolToVisibilityConverter : IValueConverter {

        public Visibility OnTrue { get; set; }
        public Visibility OnFalse { get; set; }

        public BoolToVisibilityConverter() {
            OnTrue = Visibility.Visible;
            OnFalse = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, string language) {
            var v = value as bool?;
            if (!v.HasValue) {
                return OnFalse;
            }
            return v.Value ? OnTrue : OnFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language) {
            throw new NotImplementedException();
        }
    }
}
