using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Order_Taker.Core.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order_Taker.Repositoriy.Data.Config
{
    public class OrderConfigrations : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(P=>P.Status).HasConversion(new ValueConverter<OrderStatus , string>(V=>V.ToString(),V=>(OrderStatus)Enum.Parse(typeof(OrderStatus),V)));
            builder.Property(P => P.SubTotal).HasColumnType("decimal(18,2)");
            builder.OwnsOne(P => P.ShippingAddress, X => X.WithOwner());
            builder.HasOne(P=>P.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.NoAction);
        }
    }
}
