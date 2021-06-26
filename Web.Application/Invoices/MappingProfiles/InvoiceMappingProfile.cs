using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Application.Invoices.Commands;
using Web.Application.Invoices.ViewModels;
using Web.Domain.Entities;

namespace Web.Application.Invoices.MappingProfiles
{
    public class InvoiceMappingProfile : Profile
    {
        public InvoiceMappingProfile()
        {
            CreateMap<Invoice, InvoiceVm>();
            CreateMap<InvoiceItem, InvoiceItemVm>().ConstructUsing(i => new InvoiceItemVm
            {
                Id = i.Id,
                Item = i.Item,
                Quantity = i.Quantity,
                Rate = i.Rate
            });

            CreateMap<InvoiceVm, Invoice>();
            CreateMap<InvoiceItemVm, InvoiceItem>();

            CreateMap<CreateInvoiceCommand, Invoice>();
        }
    }
}
