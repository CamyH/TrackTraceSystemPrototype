using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TrackTrace.Business;

namespace TrackTrace.Data
{
    /// <summary>
    /// Author: Cameron Hunt
    /// Date last modified: 03/12/2020
    /// This class is for serializing lists of objects. It has one method for Serialization and one method for Deserialization.
    /// </summary>
    [Serializable]
    class Serialize
    {
        private static string directory = @"D:\C# Coursework\TrackTrace\";
        private static string serializationFile = Path.Combine(directory + "data.xml");
        /* EXPERIMENTAL
        public void ExportToFile(AllData allData)
        {
            // Serialization
            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, allData);
            }
        }*/

        public void ExportToFile(List<User> list)
        {
            // Serialization
            using (Stream stream = File.Open(serializationFile, FileMode.Create))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                binaryFormatter.Serialize(stream, list);
            }
        }
        public List<User> ImportFromFile()
        {
            // Deserialization
            List<User> importedUsers = new List<User>();
            using (Stream stream = File.Open(serializationFile, FileMode.Open))
            {
                var binaryFormatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                importedUsers = (List<User>)binaryFormatter.Deserialize(stream);
            }
            return importedUsers;
        }
    }
}
