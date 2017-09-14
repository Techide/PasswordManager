using System;
using Windows.UI.Xaml.Controls;

namespace PasswordManager.ViewModels {
    public class MenuItem {
        public Symbol Icon { get; set; }
        public string Name { get; set; }
        public int Index { get; set; }
        public Type ViewModelType { get; set; }
    }
}
