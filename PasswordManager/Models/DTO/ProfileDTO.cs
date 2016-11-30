using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Models.DTO {

    public class ProfileDTO {
        public int ID { get; set; }
        public string Profile { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}