using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace PasswordManager.ViewModels {
    public class MenuItem {
        public Symbol Icon { get; set; }
        public string Name { get; set; }

        public Type ViewModelType { get; set; }
    }
}
