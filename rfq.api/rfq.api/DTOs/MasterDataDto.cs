namespace rfq.api.DTOs
{    public class RfqPortalMasterDataDto
    {
        public int MasterDataId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class CreateRfqPortalMasterDataDto
    {
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }

    public class UpdateRfqPortalMasterDataDto
    {
        public int MasterDataId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
    }
}
