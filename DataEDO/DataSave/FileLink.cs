using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DataEDO.Model.Todo;
using DevExpress.XtraEditors;

namespace DataEDO.DataSave
{
    public class FileLink : IDataStore
    {
        public List<ToDo> LoadToDoList(string pathToFile)
        {
            List<ToDo> toDoList = new List<ToDo>();

            if (File.Exists(pathToFile))
            {
                try
                {
                    using (Stream stream = File.Open(pathToFile, FileMode.Open))
                    {
                        var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                        toDoList = (List<ToDo>)bformatter.Deserialize(stream);
                    }
                }catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.ToString(), "Error file reading !");
                }
            }
            else
            {
                XtraMessageBox.Show("File : " + pathToFile + " not found.", "Warning !");

            }

            return toDoList;
        }

        public List<ToDo> LoadToDoListWhereDescritpion(string searchInDescription, string pathToFile)
        {
            List<ToDo> toDoList = LoadToDoList(pathToFile);

            return toDoList.Where(x => x.Description.Contains(searchInDescription)).ToList();
        }

        public List<ToDo> LoadToDoListWhereTitle(string searchInTitle, string pathToFile)
        {
            List<ToDo> toDoList = LoadToDoList(pathToFile);

            return toDoList.Where(x => x.Title.Contains(searchInTitle)).ToList();
        }

        public void SaveToDoList(List<ToDo> todos, string pathToFile)
        {
            //serialize
            try
            {
                using (Stream stream = File.Open(pathToFile, FileMode.OpenOrCreate))
                {
                    var bformatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

                    bformatter.Serialize(stream, todos);
                }


            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), "Error file reading !");
            }
        }
    }
}
