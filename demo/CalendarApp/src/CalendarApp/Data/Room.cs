using System.ComponentModel.DataAnnotations;

namespace CalendarApp.Data
{
    public class Room
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
