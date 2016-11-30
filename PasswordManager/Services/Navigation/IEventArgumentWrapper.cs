using System;

namespace PasswordManager.Services.Navigation {

    public interface IEventArgumentWrapper {
        Type ArgumentType { get; set; }
        dynamic Context { get; set; }
    }
}