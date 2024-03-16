using Application.Features.Brands.Commands.Create;
using Application.Features.Brands.Commands.Delete;
using Application.Features.Brands.Commands.Update;
using Application.Features.Brands.Queries.GetById;
using Application.Features.Brands.Queries.GetList;
using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : BaseController
    {
        //[HttpPost]
        //public async Task<IActionResult> Add([FromBody] CreateBrandCommand createBrandCommand)
        //{
        //    CreatedBrandResponse response = await Mediator.Send(createBrandCommand);

        //    return Ok(response);
        //}
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListModelQuery getListModelQuery = new() { PageRequest = pageRequest };

            GetListResponse<GetListModelListItemDto> response = await Mediator.Send(getListModelQuery);

            return Ok(response);
        }
        [HttpPost("GetList/ByDynamic")]
        public async Task<IActionResult> GetListByDynamic([FromQuery] PageRequest pageRequest, [FromBody] DynamicQuery? dynamicQuery=null)
        {
            GetListByDynamicModelQuery getListModelQuery = new() { PageRequest = pageRequest,DynamicQuery=dynamicQuery };

            GetListResponse<GetListByDynamicListItemDto> response = await Mediator.Send(getListModelQuery);

            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            GetByIdBrandQuery getByIdBrandQuery = new() { Id = id };

            GetByIdBrandResponse response = await Mediator.Send(getByIdBrandQuery);

            return Ok(response);
        }
        //[HttpPut]
        //public async Task<IActionResult> Update([FromBody] UpdateBrandCommand updateBrandCommand)
        //{
        //    UpdatedBrandResponse response = await Mediator.Send(updateBrandCommand);

        //    return Ok(response);
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete([FromRoute] Guid id)
        //{
        //    DeleteBrandCommand deleteBrandCommand = new() { Id = id };

        //    DeletedBrandResponse response = await Mediator.Send(deleteBrandCommand);

        //    return Ok(response);
        //}
    }
}
