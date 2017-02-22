using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace Units.Models
{
    public class XmlLoader
    {
        public string filePath { get; set; }

        public XmlLoader(string path)
        {
            filePath = path;
        }

        /// <summary>
        /// this method receives the input file and converts it to the List<Unit>
        /// </summary>
        /// <returns></returns>
        public List<Unit> GetData()
        {
            var Units = new List<Unit>();

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            foreach (XmlNode node in doc.DocumentElement)
            {
                Unit unit = new Unit();
                unit.Employees = new List<Employee>();
                unit.Title = node.Attributes.GetNamedItem("Title").Value.ToString();

                foreach (XmlNode emplnode in node.ChildNodes)
                {
                    ///if using DateTime type
                    ///
                    //string dateString = emplnode.Attributes[2].Value;
                    // char separtor = '.';
                    //string[] dateValues = dateString.Split(separtor);

                    Employee employee = new Employee();
                    employee.Name = emplnode.Attributes.GetNamedItem("Name").Value.ToString();
                    employee.Position = emplnode.Attributes.GetNamedItem("Position").Value.ToString();
                    employee.HireDate = emplnode.Attributes.GetNamedItem("HireDate").Value.ToString();
                    employee.UnitName = unit.Title;
                    unit.Employees.Add(employee);
                }
                Units.Add(unit);
            }

            return Units;
        }
    }
}