namespace PasswordManager.Models.Data.EF {

    public class Setting {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Value { get; set; }
    }
}