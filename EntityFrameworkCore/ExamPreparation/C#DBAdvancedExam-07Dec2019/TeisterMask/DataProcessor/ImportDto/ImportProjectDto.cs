namespace TeisterMask.DataProcessor.ImportDto
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    [XmlType("Project")]
    public class ImportProjectDto
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("OpenDate")]
        public string OpenDate { get; set; }

        [XmlElement("DueDate")]
        public string DueDate { get; set; }

        [XmlElement("Tasks")]
        public List<Task> Tasks { get; set; }
    }
}
