namespace CarDealer.DataTransferObjects.ExportDtos
{
    using System.Xml.Serialization;

    [XmlType("customer")]
    public class ExportCustomersDto
    {
        [XmlAttribute("full-name")]
        public string FullName { get; set; }
        [XmlAttribute("bought-cars")]
        public int BoughtCarsCount { get; set; }
        [XmlAttribute("spent-money")]
        public decimal SpentMoney { get; set; }
    }
}
