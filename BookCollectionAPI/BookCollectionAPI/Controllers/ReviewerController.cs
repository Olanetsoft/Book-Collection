using BookCollectionAPI.Dtos;
using BookCollectionAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookCollectionAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ReviewerController : Controller
    {
        private IReviewerRepository _reviewerRepository;

        // Constructor
        public ReviewerController(IReviewerRepository reviewerRepository)
        {
            _reviewerRepository = reviewerRepository;
        }


        // api/reviewer
        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        public IActionResult GetReviewers()
        {
            // get reviewers
            var reviewers = _reviewerRepository.GetReviewers().ToList();

            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // using reviewers dto to return only the needed value
            var reviewerDto = new List<ReviewerDto>();
            foreach (var reviewer in reviewers)
            {
                reviewerDto.Add(new ReviewerDto
                {
                    Id = reviewer.Id,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }

            return Ok(reviewerDto);
        }




        // api/reviewer
        [HttpGet("{reviewerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewerDto>))]
        public IActionResult GetReviewer(int reviewerId)
        {
            // get reviewer
            var reviewer = _reviewerRepository.GetReviewer(reviewerId);

            //Validate if the model state is valid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //// using reviewer dto to return only the needed value
            //var reviewerDto = new List<ReviewerDto>();
            //foreach (var reviewer in reviewers)
            //{
            //    reviewerDto.Add(new ReviewerDto
            //    {
            //        Id = reviewer.Id,
            //        FirstName = reviewer.FirstName,
            //        LastName = reviewer.LastName
            //    });
            //}

            // using reviewer dto to return only the needed value
            var reviewerDto = new ReviewerDto()
            {
                Id = reviewer.Id,
                FirstName = reviewer.FirstName,
                LastName = reviewer.LastName
            };


            return Ok(reviewerDto);
        }
    }
}
