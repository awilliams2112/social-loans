namespace DAL.Models
{
    public class DisclosureTemplate
    {
        public string Id { get; set; }

        public string Body { get; set; }

        public string Version { get; set; }

        public int DisclosureTemplateTypeId { get; set; }
        public DisclosureTemplateType DisclosureTemplateType { get; set; }
        public bool IsCurrent { get; internal set; }
    }
}
