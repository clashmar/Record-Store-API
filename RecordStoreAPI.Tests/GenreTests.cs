using RecordStoreAPI.Entities;

namespace RecordStoreAPI.Tests
{
    public class GenreTests
    {
        [TestCase(Genres.Folk, "Folk")]
        [TestCase(Genres.Hip_Hop, "Hip-Hop")]
        [TestCase(Genres.Art_Rock, "Art-Rock")]
        [TestCase(Genres.Alternative, "Alternative")]
        public void ToString_Test(Genres genre, string expectedOutput)
        {
            var result = Genre.ToString(genre);

            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}
