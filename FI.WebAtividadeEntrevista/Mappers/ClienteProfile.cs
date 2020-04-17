using AutoMapper;
using FI.AtividadeEntrevista.DML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAtividadeEntrevista.Models;

namespace WebAtividadeEntrevista.Mappers
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            CreateMap<Cliente, ClienteModel>().ReverseMap();
            CreateMap<Beneficiario, BeneficiarioModel>().ReverseMap();
        }
    }
}