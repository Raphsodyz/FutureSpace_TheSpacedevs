﻿using System.ComponentModel.DataAnnotations;
using System.Net;
using Business.DTO.Entities;
using Business.DTO.Request;
using Business.Interface;
using Cross.Cutting.Helper;
using Data.Materializated.Views;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Services.Controllers
{
    [ApiController]
    [Route("api/launches")]
    public class LaunchController : ControllerBase
    {
        private readonly ILaunchApiBusiness _launchApiBusiness;
        public LaunchController(ILaunchApiBusiness launchApiBusiness)
        {
            _launchApiBusiness = launchApiBusiness;
        }

        [HttpGet]
        [Route("search")]
        [SwaggerOperation(Summary = "Method for fuzzy search of mission, location, pad, rocket and launch.")]
        public async Task<IActionResult>SearchByParams([FromQuery]SearchLaunchRequest search)
        {
            try
            {
                _ = search ?? throw new ArgumentNullException(ErrorMessages.NullArgument);
                var data = await _launchApiBusiness.SearchByParam(search);

                return Ok(data);
            }
            catch(ValidationException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}\n{ex.Message}");
            }
        }

        [HttpGet]
        [Route("{launchId}")]
        [SwaggerOperation(Summary = "Method for search a launch by his UUID. This UUID is proveniente from the database.")]
        public async Task<IActionResult> GetById(Guid? launchId)
        {
            try
            {
                var launchView = await _launchApiBusiness.GetOneLaunch(launchId);
                return Ok(launchView);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}\n{ex.Message}");
            }
        }

        [HttpGet]
        [Route("paged")]
        [SwaggerOperation(Summary = "Method for return launches paged")]
        public async Task<IActionResult> GetAllPaged([FromQuery]int page)
        {
            try
            {
                Pagination<LaunchView> pagedLaunchList = await _launchApiBusiness.GetAllLaunchPaged(page);
                return Ok(new { CurrentlyPage = pagedLaunchList.CurrentPage, TotalRegisters = pagedLaunchList.NumberOfEntities, Pages = pagedLaunchList.NumberOfPages, Data = pagedLaunchList.Entities });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}\n{ex.Message}");
            }
        }

        [HttpDelete]
        [Route("{launchId}")]
        [SwaggerOperation(Summary = "Method for delete a launch by his UUID.")]
        public async Task<IActionResult> Delete(Guid? launchId)
        {
            try
            {
                await _launchApiBusiness.SoftDeleteLaunch(launchId);
                return Ok(SuccessMessages.DeletedEntity);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}\n{ex.Message}");
            }
        }

        [HttpPut]
        [Route("{launchId}")]
        [SwaggerOperation(Summary = "Method to update a launch by synchronize his data with ll.thespacedevs API.")]
        public async Task<IActionResult> Update(Guid? launchId)
        {
            try
            {
                LaunchView updatedLaunch = await _launchApiBusiness.UpdateLaunch(launchId);
                return Ok(updatedLaunch);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status429TooManyRequests, ex.Message);
            }
            catch (Exception ex) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}\n{ex.Message}");
            }
        }

        [HttpPost]
        [Route("launchers")]
        [SwaggerOperation(Summary = "Method to synchronize data with ll.thespacedevs API.", Description = "The offset query string is launch count starting point. It will bring up 100 to 100 max of 1500 new launches per request.")]
        public async Task<IActionResult> BulkUpdateData([FromQuery]UpdateLaunchRequest request)
        {
            try
            {
                bool updated = await _launchApiBusiness.UpdateDataSet(request);
                if (updated)
                    return Ok(SuccessMessages.ImportedDataSuccess);
                else
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}");
            }
            catch(ValidationException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status429TooManyRequests, ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{ErrorMessages.InternalServerError}\n{ex.Message}");
            }
        }
    }
}
