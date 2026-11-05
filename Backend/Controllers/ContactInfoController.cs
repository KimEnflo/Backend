using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Backend.Controllers;

/// <summary>
///     Controller for the SuportService class.
/// </summary>
/// <param name="supportService"></param>
[ApiController]
[Route("api/v1/contactinfo")]
public class ContactInfoController(SupportService supportService, ILogger<ContactInfoController> logger)
    : ControllerBase
{
    /// <summary>
    ///     Retrieves all the contact info entries in the database.
    /// </summary>
    /// <returns>JSON object of All entries in the database</returns>
    [HttpGet]
    [ProducesResponseType<ContactInfo>(StatusCodes.Status200OK)]
    [ProducesResponseType<ContactInfo>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetContactInfo()
    {
        try
        {
            var contactInfo = await supportService.GetAsync();
            return Ok(contactInfo);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error reading database: {ex}", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }

    /// <summary>
    ///     Retrieves the specified entry from the database by name.
    /// </summary>
    /// <param name="companyName">The name of the company to collect</param>
    /// <returns>JSON object of the retrieved entry if it exists.</returns>
    [HttpGet("{companyName}")]
    [ProducesResponseType<ContactInfo>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ContactInfo>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetContactInfo(string companyName)
    {
        try
        {
            var contactInfo = await supportService.GetAsync(companyName);

            return contactInfo is null
                ? NotFound()
                : Ok(contactInfo);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error reading database: {ex}", ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
    }

    /// <summary>
    ///     Creates a new entry in the database with the provided payload.
    /// </summary>
    /// <param name="contactInfo">JSON payload with contact information</param>
    /// <returns>JSON object of the created object</returns>
    [HttpPost]
    [ProducesResponseType<ContactInfo>(StatusCodes.Status201Created)]
    [ProducesResponseType<ContactInfo>(StatusCodes.Status400BadRequest)]
    [ProducesResponseType<ContactInfo>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostContactInfo([FromBody] ContactInfo contactInfo)
    {
        try
        {
            await supportService.CreateAsync(contactInfo);
            return CreatedAtAction(
                nameof(GetContactInfo),
                new { companyName = contactInfo.Name },
                contactInfo);
        }
        catch (MongoWriteException e)
        {
            logger.LogError(e, "Failed to create contact info for {CompanyName}", contactInfo.Name);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "Failed to create contact information.");
        }
    }


    /// <summary>
    ///     Updates an entry in the database with the payload information.
    /// </summary>
    /// <param name="id">ID of the company entry to update</param>
    /// <param name="contactInfo">The new information payload</param>
    /// <returns>JSON object with the updated entry</returns>
    [HttpPut("{id}")]
    [ProducesResponseType<ContactInfo>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ContactInfo>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PutContactInfo(string id, [FromBody] ContactInfo contactInfo)
    {
        try
        {
            var updatedContactInfo = await supportService.UpdateAsync(id, contactInfo);
            return updatedContactInfo?.MatchedCount == 0
                ? NotFound()
                : Ok(new ContactInfo
                {
                    Id = id,
                    Name = contactInfo.Name,
                    PhoneNumber = contactInfo.PhoneNumber,
                    Email = contactInfo.Email,
                    Website = contactInfo.Website,
                    SupportHours = contactInfo.SupportHours
                });
        }
        catch (MongoWriteException e)
        {
            logger.LogError(e, "Failed to update contact info for {CompanyName}", contactInfo.Name);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "Failed to update contact information.");
        }
    }

    /// <summary>
    ///     Removes an entry from the database.
    /// </summary>
    /// <param name="id">ID of the entry to remove</param>
    /// <returns>JSON of the deleted object</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType<ContactInfo>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType<ContactInfo>(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteContactInfo(string id)
    {
        try
        {
            var deletedContactInfo = await supportService.RemoveAsync(id);
            return deletedContactInfo.DeletedCount == 0
                ? NotFound()
                : Ok(deletedContactInfo);
        }
        catch (MongoWriteException e)
        {
            logger.LogError(e, "Failed to delete contact info for {CompanyName}", id);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "Failed to delete contact information.");
        }
    }
}