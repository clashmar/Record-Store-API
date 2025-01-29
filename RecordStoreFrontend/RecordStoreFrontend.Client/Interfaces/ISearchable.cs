using RecordStoreFrontend.Client.Models;

namespace RecordStoreFrontend.Client.Interfaces
{
    public interface ISearchable
    {
        public int Id { get; set; }
        public SearchResultType ResultType { get; set; }
        public string Name { get; set; }
        public string ImageURL { get; set; }
        public string Description();
    }
}
