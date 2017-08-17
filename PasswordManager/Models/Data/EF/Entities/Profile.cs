using System.ComponentModel.DataAnnotations;

namespace PasswordManager.Models.Data.EF {

    public class Profile {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Account { get; set; }

        [Required]
        public byte[] Password { get; set; }

        [Required]
        public byte[] IV { get; set; }

        [Required]
        public byte[] Salt { get; set; }
    }
}