using System;
using Elasticsearch;
using Nest;

namespace ElasticExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("people");
            var client = new ElasticClient(settings);

            var person = new Person
            {
                Id = 1,
                FirstName = "Martijn",
                LastName = "Laarman"
            };

            var indexResponse = client.IndexDocument(person);

            var searchResponse = client.Search<Person>(s => s.From(0).Size(10).Query(q => q.Match(m => m.Field(f => f.FirstName).Query("Martijn"))));
            var people = searchResponse.Documents;
        }
        public class Person
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }
    }
 }
