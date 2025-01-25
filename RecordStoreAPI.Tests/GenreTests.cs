using RecordStoreAPI.Entities;

namespace RecordStoreAPI.Tests
{
    public class GenreTests
    {
        [TestCase(Genre.Folk, "Folk")]
        [TestCase(Genre.Hip_Hop, "Hip-Hop")]
        [TestCase(Genre.Art_Rock, "Art-Rock")]
        [TestCase(Genre.Alternative, "Alternative")]
        public void ToString_Test(Genre genre, string expectedOutput)
        {
            var result = Genre.ToFriendlyString(genre);

            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}
