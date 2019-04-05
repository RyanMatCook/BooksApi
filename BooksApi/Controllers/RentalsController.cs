using System.Collections.Generic;
using BooksApi.Services;
using Microsoft.AspNetCore.Mvc;
using BooksApi.Models;

namespace BooksApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RentalsController : ControllerBase
	{
		private readonly RentalService _rentalService;

		public RentalsController(RentalService rentalService)
		{
			_rentalService = rentalService;
		}

		[HttpGet]
		public ActionResult<List<Rental>> Get()
		{
			return _rentalService.Get();
		}

		[HttpGet("{id:length(24)}", Name = "GetRental")]
		public ActionResult<Rental> Get(string id)
		{
			var rental = _rentalService.Get(id);

			if (rental == null)
			{
				return NotFound();
			}

			return rental;
		}

		[HttpPost]
		public ActionResult<Rental> Create(Rental rental)
		{
			_rentalService.Create(rental);

			return CreatedAtRoute("GetRental", new { id = rental.Id.ToString() }, rental);
		}

		[HttpPut("{id:length(24)}")]
		public IActionResult Update(string id, Rental rentalIn)
		{
			var rental = _rentalService.Get(id);

			if (rental == null)
			{
				return NotFound();
			}

            rentalIn.PreviousVersion = rental;

			_rentalService.Update(id, rentalIn);

			return NoContent();
		}

		[HttpDelete("{id:length(24)}")]
		public IActionResult Delete(string id)
		{
			var rental = _rentalService.Get(id);

			if (rental == null)
			{
				return NotFound();
			}

			_rentalService.Remove(rental.Id);

			return NoContent();
		}
	}
}