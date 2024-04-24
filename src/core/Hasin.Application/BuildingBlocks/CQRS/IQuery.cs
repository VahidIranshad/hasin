﻿using MediatR;

namespace Hasin.Application.BuildingBlocks.CQRS;
public interface IQuery<out TResponse> : IRequest<TResponse>  
    where TResponse : notnull
{
}