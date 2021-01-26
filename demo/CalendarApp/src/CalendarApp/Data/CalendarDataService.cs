using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CalendarApp.Data
{
    public class CalendarDataService
    {
        private readonly IDbContextFactory<ApplicationDbContext> dbFactory;

        public CalendarDataService(IDbContextFactory<ApplicationDbContext> dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public async Task<Room[]> GetRoomsAsync()
        {
            using var db = dbFactory.CreateDbContext();
            return await db.Rooms.ToArrayAsync();
        }

        public async Task<Booking[]> GetBookingsAsync(DateTime date)
        {
            using var db = dbFactory.CreateDbContext();
            return await db.Bookings
                .Where(e => e.StartTime >= date && e.StartTime < date.AddDays(1))
                .ToArrayAsync();
        }

        public async Task MoveBookingAsync(int entryId, int toRoomId, DateTime newDate)
        {
            using var db = dbFactory.CreateDbContext();
            var entry = await db.Bookings.FirstAsync(e => e.Id == entryId);
            entry.RoomId = toRoomId;
            entry.StartTime = newDate;
            await db.SaveChangesAsync();
        }

        public async Task SaveBookingAsync(Booking entry)
        {
            using var db = dbFactory.CreateDbContext();
            db.Update(entry);
            await db.SaveChangesAsync();
        }

        public async Task<(Room, int)[]> GetAvailableSlots(DateTime date, Booking forBooking)
        {
            var allRooms = await GetRoomsAsync();
            var otherEntriesOnDate = (await GetBookingsAsync(date)).Where(e => e.Id != forBooking.Id);
            var slotsUsed = otherEntriesOnDate.Select(e => (e.RoomId, e.StartTime.Hour)).ToHashSet();
            var results = new List<(Room, int)>();
            for (var hour = 9; hour < 17; hour++)
            {
                foreach (var room in allRooms)
                {
                    if (!slotsUsed.Contains((room.Id, hour)))
                    {
                        results.Add((room, hour));
                    }
                }
            }

            return results.ToArray();
        }
    }
}
