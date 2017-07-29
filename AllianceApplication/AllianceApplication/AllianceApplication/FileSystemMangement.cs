using AllianceApplication;
using AllianceApplication.SubClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
namespace AllianceApplication
{
    public class FileSystemMangement<T>
        where T: FileSystemMangement<T>
    {
        //How an ID is set up Type|GUID
        public string Id { get; set; }

        /// <summary>
        /// Author: Amin Sharif
        /// Modified Date: 7/29/2017
        /// Gets List from file. Adds this to List. Saves List back to File. Each type has their own file.
        /// </summary>
        public void Save()
        {
                
            string type = this.GetType().Name;

            // According to the test a FileSystemMangement subclass will only get an ID when saved.
            Id = this.GetType().Name + "|" + Guid.NewGuid();

            string filePath = GetFilePath(type);
            List<T> listOfAllObjectsFromFile = GetAllObjectsFromFile(filePath);
            bool doNotOverwrite = true;

            if (listOfAllObjectsFromFile == null)
            {
                listOfAllObjectsFromFile = new List<T>();
                listOfAllObjectsFromFile.Add((T)this);
            }
            else
            {
                doNotOverwrite = false;
                listOfAllObjectsFromFile.Add((T)this);
            }

            string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(listOfAllObjectsFromFile);
            SaveToFile(jsonString, doNotOverwrite, type);
        }

        /// <summary>
        /// Author: Amin Sharif
        /// Modified Date: 7/29/2017
        /// Gets List from file. Searches List for Id. Returns object with that Id if found. Otherwise, null.
        /// </summary>
        public static T  Find(string idIn) 
        {
            string type = idIn.Split('|')[0];
            string filePath = GetFilePath(type);
            List<T> listOfObjects = GetAllObjectsFromFile(filePath);

            if (listOfObjects == null || !listOfObjects.Any(x => x.Id == idIn))
                return null;

            var listOfFoundObjects = listOfObjects.Where(x => x.Id == idIn);
            return (T)listOfFoundObjects.ElementAt(listOfFoundObjects.Count() - 1);
        }

        /// <summary>
        /// Author: Amin Sharif
        /// Modified Date: 7/29/2017
        /// Grabs List from file. Searches List for Id. 
        /// If found, removes from List and saves the List back into the file. Sets ID to null
        /// Otherwise, just set ID to null. 
        /// </summary>
        public void Delete()
        {
            string type = this.GetType().Name;
            string filePath = GetFilePath(type);
            string modifiedString= null;
            List<T> listOfObjects = GetAllObjectsFromFile(filePath);

            if (listOfObjects != null && listOfObjects.Any(x => x.Id == this.Id))
            {
                var updatedList = listOfObjects.Where(x => x.Id != this.Id);
                modifiedString = Newtonsoft.Json.JsonConvert.SerializeObject(updatedList);
            }

            if(modifiedString != null)
                SaveToFile(modifiedString, false, type);

            this.Id = null;
        }

        /// <summary>
        /// Author: Amin Sharif
        /// Modified Date: 7/29/2017
        /// Saves JSON string to file that is specfied by type.
        /// </summary>
        /// <param name="jsonString"> The JSON string you want to save to file.</param>
        /// <param name="doNotOverwrite"> Whether or not, you wish to overwrite the contents of the file.</param>
        /// <param name="type">The type of the objects in the jsonstring.</param>
        private static void SaveToFile(string jsonString, bool doNotOverwrite, string type)
        {
            string filePath = GetFilePath(type);

            using (var sw = new StreamWriter(filePath, doNotOverwrite))
            {
                sw.Write(jsonString);
                sw.Close();
            }
        }


        /// <summary>
        /// Author: Amin Sharif
        /// Modified Date: 7/29/2017
        /// Creates a valid path to save files for each different type.
        /// </summary>
        /// <param name="type"> The type of the object you're working with</param>
        /// <returns>A file Path </returns>
        private static string GetFilePath(string type)
        {
            string excutingPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            char seperator = Path.DirectorySeparatorChar;
            string folderPath = excutingPath + seperator + "Storage";
            Directory.CreateDirectory(folderPath);
            return folderPath + seperator + type + "Storage.txt";
        }

        /// <summary>
        /// Author: Amin Sharif
        /// Modified Date: 7/29/2017
        /// Grabs the file contents. Deserializes it into a a list and returns it back.
        /// </summary>
        /// <param name="filePath">The location of the file you wish to grab the objects from</param>
        /// <returns>A list of FileSystemMangement subclass object.</returns>
        private static List<T> GetAllObjectsFromFile(string filePath)
        {

            List<T> listOfObjects = null;

            if (File.Exists(filePath))
            {
                using (var sr = new StreamReader(filePath))
                {
                    string jsonString = sr.ReadToEnd();

                    if (!string.IsNullOrWhiteSpace(jsonString))
                        listOfObjects = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(jsonString);
                }

            }

            return listOfObjects;
        }


    }
}
