﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace PasswordManager.Logic {
    public interface IViewModel<T> where T : IViewModel<T> {
    }
}
