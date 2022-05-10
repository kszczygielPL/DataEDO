using System.Collections.Generic;
using DataEDO.Model.Todo;

namespace DataEDO.DataSave
{
    public interface IDataStore
    {
        void SaveToDoList(List<ToDo> todos, string connString);

        List<ToDo> LoadToDoList(string connString);

        List<ToDo> LoadToDoListWhereTitle(string searchInTitle, string connString);

        List<ToDo> LoadToDoListWhereDescritpion(string searchInDescription, string connString);

        bool CheckConnectionLink(string connString);
    }
}
