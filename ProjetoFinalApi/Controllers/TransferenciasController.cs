using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinalApi.Repository.Interfaces;
using ProjetoFinalApi.DTOs;
using ProjetoFinalApi.Pagination;
using Newtonsoft.Json;
using FluentValidation;
using ProjetoFinalApi.Models.Data;
using ProjetoFinalApi.Extensions;

namespace ProjetoFinalApi.Controllers;

[ApiController]
[Route("api/v1/times")]
[Produces("application/json")]
public class TransferenciasController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public TransferenciasController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    // CRUD
}
