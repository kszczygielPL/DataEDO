using System;

namespace DataEDO.Model.Todo
{
    [Serializable]
    public class ToDo 
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }

        public bool IsNew { get; set; } = false;
    }
}
