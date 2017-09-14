using PasswordManager.Services.Frames;

namespace PasswordManager.ViewModels {
    public class SettingsViewModel : IViewModel {
        private FrameService _frameService;

        public SettingsViewModel(FrameService frameService) {
            _frameService = frameService;
        }

    }
}
