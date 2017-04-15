using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models.Data.EF.Entities {

    public class Setting {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Key { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Value { get; set; }
    }
}