using RecordStoreAPI.Entities;
using RecordStoreFrontend.Client.Models;

namespace RecordStoreAPI.Tests
{
    public class GenreTests
    {
        [TestCase(GenreEnum.Folk, "Folk")]
        [TestCase(GenreEnum.Hip_Hop, "Hip-Hop")]
        [TestCase(GenreEnum.Art_Rock, "Art-Rock")]
        [TestCase(GenreEnum.Alternative, "Alternative")]
        public void ToString_Test(GenreEnum genre, string expectedOutput)
        {
            var result = Genres.ToFriendlyString(genre);

            Assert.That(result, Is.EqualTo(expectedOutput));
        }
    }
}
