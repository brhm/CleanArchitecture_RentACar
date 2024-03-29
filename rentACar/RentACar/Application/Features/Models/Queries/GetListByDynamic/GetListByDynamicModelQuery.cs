﻿using Application.Features.Models.Queries.GetList;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Application.Responses;
using Core.Persistence.Dynamic;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetListByDynamic;

public class GetListByDynamicModelQuery : IRequest<GetListResponse<GetListByDynamicListItemDto>>
{
    public PageRequest PageRequest { get; set; }
    public DynamicQuery DynamicQuery { get; set; }

    public class GetListByDynamicModelQueryHandler : IRequestHandler<GetListByDynamicModelQuery, GetListResponse<GetListByDynamicListItemDto>>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public GetListByDynamicModelQueryHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListByDynamicListItemDto>> Handle(GetListByDynamicModelQuery request, CancellationToken cancellationToken)
        {
            var models = await _modelRepository.GetListByDynamicAsync(
                request.DynamicQuery,
                include: m => m.Include(m => m.Brand)
                                .Include(m => m.Fuel)
                                .Include(m => m.Transmission),
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize
                );

            var response = _mapper.Map<GetListResponse<GetListByDynamicListItemDto>>(models);
            return response;
        }
    }
}
