using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Services.Navigation {

    public class EventArgumentWrapper : IEventArgumentWrapper {
        public Type ArgumentType { get; set; }

        public dynamic Context { get; set; }

        public EventArgumentWrapper(Type argumentType, object context) {
            ArgumentType = argumentType;
            Context = context;
        }
    }
}