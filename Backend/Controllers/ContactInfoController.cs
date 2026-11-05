using System.Runtime.InteropServices;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Backend.Controllers;

/// <summary>
/// Controller for the SuportService class.
/// </summary>
/// <param name="supportService"></param>
[ApiController]
[Route("api/v1/contactinfo")]
public class ContactInfoController (SupportService supportService, ILogger<ContactInfoController> logger): ControllerBase
{
    
    /// <summary>
    /// Retrieves all the contact info entries in the database.
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
            logger.LogError(ex ,"Error reading database: {ex}",ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }
   
    }
}