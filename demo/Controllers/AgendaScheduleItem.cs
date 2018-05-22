using System.ComponentModel.DataAnnotations.Schema;

namespace demo
{
    [Table("AgendaScheduleItem")]
    public class AgendaScheduleItem
    {
        public string name {get;set;}
    }
}