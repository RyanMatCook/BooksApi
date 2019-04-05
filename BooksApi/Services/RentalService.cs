using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using BooksApi.Models;

namespace BooksApi.Services
{
	public class RentalService
	{
		private readonly IMongoCollection<Rental> _rentals;

		public RentalService(IConfiguration config)
		{
			var client = new MongoClient(config.GetConnectionString("BookstoreDb"));
			var database = client.GetDatabase("BookstoreDb");
			_rentals = database.GetCollection<Rental>("Rentals");
		}

		public List<Rental> Get()
		{
			return _rentals.Find(rental => true).ToList();
		}

		public Rental Get(string id)
		{
			return _rentals.Find<Rental>(rental => rental.Id == id).FirstOrDefault();
		}

		public Rental Create(Rental rental)
		{
			_rentals.InsertOne(rental);
			return rental;
		}

		public void Update(string id, Rental rentalIn)
		{
			_rentals.ReplaceOne(rental => rental.Id == id, rentalIn);
		}

		public void Remove(Rental rentalIn)
		{
			_rentals.DeleteOne(rental => rental.Id == rentalIn.Id);
		}

		public void Remove(string id)
		{
			_rentals.DeleteOne(rental => rental.Id == id);
		}
	}
}