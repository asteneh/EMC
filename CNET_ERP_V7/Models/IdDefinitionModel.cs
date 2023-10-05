using CNET_V7_Domain.Misc;

namespace CNET_ERP_V7.Models
{
    public class IdDefinitionModel
    {
        public string iden_description { get; set; }
        public string iden_prefix { get; set; }
        public string iden_prefixseparator { get; set; }
        public string iden_length { get; set; }
        public string iden_suffix { get; set; }
        public string iden_suffixseparator { get; set; }
        public string iden_remark { get; set; }
        public int iden_code { get; set; }
        public int iden_component { get; set; }
        public string iden_type { get; set; }
        public List<IdDefinations> dTO2s { get; set; }
    }
    public class AssignedTo
    {
        public string code { get; set; }
        public string description { get; set; }
    }
    public class separator
    {
        public string code { get; set; }
        public string description { get; set; }
    }

       public class IddefinitionDTO2
    {
        public int Id { get; set; }

        public int Code { get; set; }
        public string reference { get; set; }

        public int? Pointer { get; set; }

        public string? Description { get; set; }

        public string? Prefix { get; set; }

        public string? PrefixSeparator { get; set; }

        public int? Length { get; set; }

        public string? SuffixSeparator { get; set; }

        public string? Suffix { get; set; }

        public string? Remark { get; set; }
    }
}

