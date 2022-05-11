namespace FestivalManager.Tests
{
    using FestivalManager.Entities;
    using NUnit.Framework;
    using System;

    [TestFixture]
	public class StageTests
    {
		private Stage stage;

		[SetUp]
		public void TestInitialize()
        {
			this.stage = new Stage();
        }

		[Test]
	    public void Constructor_Should_Initialize_Stage_Collections()
	    {
			Assert.IsNotNull(stage.Performers);
		}

		[Test]
		public void AddPerformer_Positive()
        {
			Performer performer1 = new Performer("Gogo", "Nikoi", 18);
			Performer performer2 = new Performer("Pesho", "Dimitrov", 100);
			Performer performer3 = new Performer("Ivan", "Stoichkov", 35);
			stage.AddPerformer(performer1);
			stage.AddPerformer(performer2);
			stage.AddPerformer(performer3);

			Assert.AreEqual(3, stage.Performers.Count);
        }

		[Test]
		public void AddPerformer_ShouldThrowException_If_PerformerIsYoungerThan18()
		{
			Performer performer1 = new Performer("Ivan", "Nan", 17);

			Assert.Throws<ArgumentException>(() => stage.AddPerformer(performer1));
		}

		[Test]
		public void AddPerformer_ShouldThrowException_If_PerformerIsNull()
		{
			Performer performer2 = null;

			Assert.Throws<ArgumentNullException>(() => stage.AddPerformer(performer2));
		}

		[Test]
		public void AddSongToPerformer_Positive()
		{
			Performer performer1 = new Performer("Gogo", "Nikoi", 18);
			Performer performer2 = new Performer("Pesho", "Dimitrov", 100);
			Performer performer3 = new Performer("Ivan", "Stoichkov", 35);

			Song song1 = new Song("Love", new TimeSpan(0, 3, 30));
			Song song2 = new Song("Hate", new TimeSpan(0, 1, 00));
			Song song3 = new Song("I Don't Care", new TimeSpan(99, 1, 59));

			stage.AddSong(song1);
			stage.AddSong(song2);
			stage.AddSong(song3);

			stage.AddPerformer(performer1);
			stage.AddPerformer(performer2);
			stage.AddPerformer(performer3);

			string result1 = stage.AddSongToPerformer("Love", performer1.ToString());
			string result2 = stage.AddSongToPerformer("Hate", performer3.ToString());
			string result3 = stage.AddSongToPerformer("I Don't Care", performer2.ToString());

			Assert.AreEqual($"{song1} will be performed by {performer1}", result1);
			Assert.AreEqual($"{song2} will be performed by {performer3}", result2);
			Assert.AreEqual($"{song3} will be performed by {performer2}", result3);
		}

		[Test]
		public void AddSongToPerformer_ShouldThrowException_IffNullValueIsPassed()
		{
			Performer performer1 = new Performer("Gogo", "Nikoi", 18);
			string performerName = null;
			Song song1 = new Song("Love", new TimeSpan(0, 3, 30));
			string songName = null;

			stage.AddSong(song1);
			stage.AddPerformer(performer1);

			Assert.Throws<ArgumentNullException>(() => stage.AddSongToPerformer(songName, performer1.ToString()));
			Assert.Throws<ArgumentNullException>(() => stage.AddSongToPerformer("Love", performerName));
			Assert.Throws<ArgumentNullException>(() => stage.AddSongToPerformer(songName, performerName));
		}

		[Test]
		public void AddSongToPerformer_ShouldThrowException_IffSongOrPerformer_DoesNotExist()
		{
			Performer performer1 = new Performer("Gogo", "Nikoi", 18);
			Performer performer2 = new Performer("Pesho", "Dimitrov", 100);

			Song song1 = new Song("Love", new TimeSpan(0, 3, 30));
			Song song2 = new Song("Hate", new TimeSpan(0, 1, 00));

			stage.AddSong(song1);
			stage.AddPerformer(performer1);

			Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer("Hate", performer1.FullName));
			Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer("Love", performer2.FullName));
			Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer("Hate", performer2.FullName));
		}

		[Test]
		public void AddSong_ShouldThrowException_IfSongDurationIsLessThanAMinute()
        {
			Song song1 = new Song("Love", new TimeSpan(0, 0, 59));
			Song song2 = new Song("Hate", new TimeSpan(0, 0, 1));
			Song song3 = new Song("I Don't Care", new TimeSpan(0, 0, 0));

			Assert.Throws<ArgumentException>(() => stage.AddSong(song1));
			Assert.Throws<ArgumentException>(() => stage.AddSong(song2));
			Assert.Throws<ArgumentException>(() => stage.AddSong(song3));
        }

		[Test]
		public void AddSong_ShouldThrowException_If_SongIsNUll()
		{
			Song song1 = null;

			Assert.Throws<ArgumentNullException>(() => stage.AddSong(song1));
		}

		[Test]
		public void Play_ShouldReturn_Appropriate_Performers_And_Songs_Count()
        {
			Performer performer1 = new Performer("Gogo", "Nikoi", 18);
			Performer performer2 = new Performer("Pesho", "Dimitrov", 100);
			Performer performer3 = new Performer("Ivan", "Stoichkov", 35);

			Song song1 = new Song("Love", new TimeSpan(0, 3, 30));
			Song song2 = new Song("Hate", new TimeSpan(0, 1, 00));
			Song song3 = new Song("I Don't Care", new TimeSpan(99, 1, 59));

			stage.AddSong(song1);
			stage.AddSong(song2);
			stage.AddSong(song3);

			stage.AddPerformer(performer1);
			stage.AddPerformer(performer2);
			stage.AddPerformer(performer3);

			stage.AddSongToPerformer("Love", performer1.ToString());
			stage.AddSongToPerformer("Hate", performer3.ToString());
			stage.AddSongToPerformer("I Don't Care", performer2.ToString());

            string result = stage.Play();
            string expected = "3 performers played 3 songs";
            Assert.AreEqual(expected, result);
        }

	}
}