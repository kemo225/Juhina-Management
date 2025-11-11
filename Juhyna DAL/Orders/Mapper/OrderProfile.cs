using AutoMapper;
using Juhyna_DAL.ManageProducts.DTO;
using Juhyna_DAL.Models;
using Juhyna_DAL.Orders.DTO;
using Juhyna_DAL.ProductInventory.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Juhyna_DAL.Orders.Mapper
{
    public class OrderProfile:Profile
    {
        private string GetStatusOrder(int Status)
        {
            switch (Status)
            {
                case (int)enStatusOrder.Canceled:
                    return "Canceled";
                default:
                    return "Completed";


            }
        }
        public OrderProfile()
        {
            CreateMap<Models.Order, DtoOrderRead>().ForMember(to => to.Id, from => from.MapFrom(from => from.ID)).
               ForMember(to => to.CustomerName, from => from.MapFrom(from => from.Visit.Customer.Person.FirstName + " " + from.Visit.Customer.Person.LastName)).
               ForMember(to => to.SaleName, from => from.MapFrom(from => from.Visit.CreatedBySale.Person.FirstName + " " + from.Visit.CreatedBySale.Person.LastName)).
               ForMember(to => to.PhoneCustomer, from => from.MapFrom(from => from.Visit.Customer.Person.Phone)).
               ForMember(to => to.EmailCustomer, from => from.MapFrom(from => from.Visit.Customer.Person.Email)).
               ForMember(to => to.AddressPlace, from => from.MapFrom(from => from.Visit.Place.Address)).
               ForMember(to => to.Quantity, from => from.MapFrom(from => from.Quantity)).
                ForMember(to => to.CreateAt, from => from.MapFrom(from =>DateOnly.FromDateTime(from.CreateAt)))
                .ForMember(to => to.PaymentMethod, from => from.MapFrom(from => from.PaymentMethod.PaymentMethod1)).
                  ForMember(to => to.ProductPrice, from => from.MapFrom(from => from.Invoice.ProductinInventory.Product.Price)).
                ForMember(to => to.TotalPrice, from => from.MapFrom(from => from.Invoice.ProductinInventory.Product.Price * from.Quantity))
            .ForMember(to => to.ProductName, from => from.MapFrom(from => from.Invoice.ProductinInventory.Product.Description))
            .ForMember(to => to.Status, from => from.MapFrom(from => GetStatusOrder(from.Status)))
             .ForMember(to => to.InvoiceID, from => from.MapFrom(from => from.InvoiceID))

            ;

            CreateMap<Models.Order, DToOrderUpdate>().ForMember(to => to.ID, from => from.MapFrom(from => from.ID)).
           ForMember(to => to.InvoiceID, from => from.MapFrom(from => from.InvoiceID)).
           ForMember(to => to.Quantity, from => from.MapFrom(from => from.Quantity)).
           ForMember(to => to.PaymentmethodID, from => from.MapFrom(from => from.PaymentMethodID)).
           ForMember(to => to.VisitID, from => from.MapFrom(from => from.VisitID)).
            ForMember(to => to.CreatedBySaleID, from => from.MapFrom(from => from.CreatedBySaleID));
        }
    }
}
