using FrontDeskApp;
using NUnit.Framework;
using System;
using System.Linq;

namespace BookigApp.Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void RoomConstructor_ShouldInitializeProperly()
        {
            int bedCapacity = 5;
            double pricePerNight = 100.99;

            Room room = new Room(bedCapacity, pricePerNight);

            Assert.AreEqual(bedCapacity, room.BedCapacity);
            Assert.AreEqual(pricePerNight, room.PricePerNight);
        }

        [TestCase(0, 100)]
        [TestCase(-1, 100)]
        public void RoomConstructor_ShouldThrowException_IfBedCapacityIsNegavativeOrZero(int bedCapacity, double pricePerNight)
        {
            Assert.Throws<ArgumentException>(() => new Room(bedCapacity, pricePerNight));
        }

        [TestCase(5, 0)]
        [TestCase(1, -1)]
        public void RoomConstructor_ShouldThrowException_IfPricePerNightIsNegavativeOrZero(int bedCapacity, double pricePerNight)
        {
            Assert.Throws<ArgumentException>(() => new Room(bedCapacity, pricePerNight));
        }

        [Test]
        public void BookingConstructor_ShouldInitializeProperly()
        {
            int bedCapacity = 5;
            double pricePerNight = 100.99;
            Room room = new Room(bedCapacity, pricePerNight);

            int bookingNumber = 1;
            int residenceDuration = 1;
            Booking booking = new Booking(bookingNumber, room, residenceDuration);

            Assert.AreEqual(bookingNumber, booking.BookingNumber);
            Assert.AreEqual(residenceDuration, booking.ResidenceDuration);
            Assert.AreEqual(room, booking.Room);
        }

        [Test]
        public void HotelConstructor_ShouldInitializeProperly()
        {
            string fullName = "Nova";
            int category = 4;

            Hotel hotel = new Hotel(fullName, category);

            Assert.AreEqual(fullName, hotel.FullName);
            Assert.AreEqual(category, hotel.Category);
            Assert.AreEqual(0, hotel.Turnover);
            Assert.IsNotNull(hotel.Rooms);
            Assert.AreEqual(0, hotel.Rooms.Count);
            Assert.IsNotNull(hotel.Bookings);
            Assert.AreEqual(0, hotel.Bookings.Count);
        }

        [Test]
        public void HotelConstructor_ShouldThrowException_IfFullNameIsNullOrWhiteSpace()
        {
            Assert.Throws<ArgumentNullException>(() => new Hotel(null, 3));
            Assert.Throws<ArgumentNullException>(() => new Hotel("", 3));
            Assert.Throws<ArgumentNullException>(() => new Hotel(" ", 3));
            Assert.Throws<ArgumentNullException>(() => new Hotel("  ", 3));
        }

        [Test]
        public void HotelConstructor_ShouldThrowException_IfCategoryIsInvalid()
        {
            Assert.Throws<ArgumentException>(() => new Hotel("Hilton", 0));
            Assert.Throws<ArgumentException>(() => new Hotel("Dunav", 6));
        }

        [Test]
        public void AddRoom_ShouldWorkProperly()
        {
            string fullName = "Nova";
            int category = 4;
            Hotel hotel = new Hotel(fullName, category);

            int bedCapacity = 5;
            double pricePerNight = 100.99;
            Room room = new Room(bedCapacity, pricePerNight);

            hotel.AddRoom(room);

            Assert.AreEqual(1, hotel.Rooms.Count);
        }

        [Test]
        public void BookRoom_ShouldThrowException_IfAdultsAreLessOrEqualtToZero()
        {
            string fullName = "Nova";
            int category = 4;
            Hotel hotel = new Hotel(fullName, category);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(0,1,2,50));
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(-1,1,2,50));
        }

        [Test]
        public void BookRoom_ShouldThrowException_IfChildrenAreLessThanZero()
        {
            string fullName = "Nova";
            int category = 4;
            Hotel hotel = new Hotel(fullName, category);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(1, -1, 2, 50));
        }

        [Test]
        public void BookRoom_ShouldThrowException_IfResidenceDurationIsLessThanZero()
        {
            string fullName = "Nova";
            int category = 4;
            Hotel hotel = new Hotel(fullName, category);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(1, 0,-1, 50));
        }

        [TestCase(2, 0, 3, 90, 90)]
        public void BookRoom_ShouldWorkProperlyWithValidData(int adults, int children, int residenceDuration, double budget, double expectedTurnover)
        {
            string fullName = "Nova";
            int category = 4;
            Hotel hotel = new Hotel(fullName, category);

            int room1BedCapacity = 5;
            double room1PricePerNight = 100.99;
            Room room1 = new Room(room1BedCapacity, room1PricePerNight);

            int room2BedCapacity = 2;
            double room2PricePerNight = 30;
            Room room2 = new Room(room2BedCapacity, room2PricePerNight);

            int room3BedCapacity = 6;
            double room3PricePerNight = 120;
            Room room3 = new Room(room3BedCapacity, room3PricePerNight);

            hotel.AddRoom(room1);
            hotel.AddRoom(room2);
            hotel.AddRoom(room3);

            hotel.BookRoom(adults, children, residenceDuration, budget);

            Assert.AreEqual(1, hotel.Bookings.Count);
            Assert.AreEqual(1, hotel.Bookings.First().BookingNumber);
            Assert.AreEqual(expectedTurnover, hotel.Turnover);
        }
    }
}