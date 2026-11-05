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
    
    /// <summary>  
    /// Retrieves the specified entry from the database by name.  
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
        }    catch (Exception ex)  
        {   logger.LogError(ex ,"Error reading database: {ex}",ex.Message);  
            return StatusCode(StatusCodes.Status500InternalServerError, ex);  
        }}  
    
    /// <summary>  
    /// Creates a new entry in the database with the provided payload.  
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
                contactInfo);    }    catch (MongoWriteException e)  
        {        logger.LogError(e, "Failed to create contact info for {CompanyName}", contactInfo.Name);  
            return StatusCode(  
                StatusCodes.Status500InternalServerError,  
                "Failed to create contact information.");  
        }    }  
}