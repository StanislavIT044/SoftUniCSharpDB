namespace CarDealer.DataTransferObjects
{
    using System.Xml.Serialization;
    
    [XmlType("Supplier")]
    public class ImportSupliersDto
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("isImporter")]
        public bool IsImporter { get; set; }
    }
}
