﻿using Application.Reviews.Commands;
using Application.Reviews.Dtos;
using Application.Reviews.Queries;
using Application.Reviews.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Bookify_Library_mgnt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<IActionResult> GetReviews()
    {
        List<ReviewDto> result = await _sender.Send(new GetReviewsQuery());
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetReviewById(string id)
    {
        ReviewDto result = await _sender.Send(new GetReviewByIdQuery(id));
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewCommand command)
    {
        ReviewDto result = await _sender.Send(command);
        return CreatedAtAction(nameof(GetReviewById), new { id = result.Id }, result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateReview([FromRoute] string id, [FromBody] UpdateReviewCommand command)
    {
        command.id = id;
        ReviewDto result = await _sender.Send(command);
        return Ok(result);

    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> DeleteReview([FromRoute] string id)
    {
        ReviewDto result = await _sender.Send(new DeleteReviewCommand { Id = id });
        return Ok(result);
    }
}
