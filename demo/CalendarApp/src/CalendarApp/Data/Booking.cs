using System;
using System.ComponentModel.DataAnnotations;

namespace CalendarApp.Data
{
    public class Booking
    {
        [Key] public int Id { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Room is required")]
        public int RoomId { get; set; }
        public Room Room { get; set; }

        public DateTime StartTime { get; set; }

        [Required] public string Name { get; set; }

        public string Notes { get; set; }

        public static string HourLabel(int hour)
            => $"{( hour > 12 ? hour - 12 : hour )}{( hour >= 12 ? "pm" : "am" )}";
    }
}
