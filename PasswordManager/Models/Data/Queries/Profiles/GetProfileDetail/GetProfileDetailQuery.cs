namespace PasswordManager.Models.Data.Queries {

    public class GetProfileDetailQuery : ISeparatedQuery<GetProfileDetailResult> {
        public string PublicKey { get; set; }
        public int ProfileId { get; set; }
    }
}