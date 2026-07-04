using AutoMapper;
using PizzaFlow.Api.Dtos;
using PizzaFlow.Api.Models;

namespace PizzaFlow.Api.Services;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Pizza, PizzaDto>();
        CreateMap<PizzaCreateDto, Pizza>();

        CreateMap<ItemPedido, ItemPedidoDto>()
            .ForMember(d => d.PizzaNome, opt => opt.MapFrom(s => s.Pizza != null ? s.Pizza.Nome : string.Empty));

        CreateMap<Pedido, PedidoDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

        CreateMap<Pedido, PedidoResumoDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()));

        CreateMap<Pedido, PedidoAdminDto>()
            .ForMember(d => d.Status, opt => opt.MapFrom(s => s.Status.ToString()))
            .ForMember(d => d.ClienteNome, opt => opt.MapFrom(s => s.Usuario != null ? s.Usuario.NomeCompleto : string.Empty))
            .ForMember(d => d.ClienteTelefone, opt => opt.MapFrom(s => s.Usuario != null ? s.Usuario.Telefone : string.Empty))
            .ForMember(d => d.ClienteEndereco, opt => opt.MapFrom(s => s.Usuario != null ? s.Usuario.Endereco : string.Empty));
    }
}
