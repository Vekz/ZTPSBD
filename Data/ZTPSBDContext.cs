using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZTPSBD.Data
{
    public class ZTPSBDContext : DbContext
    {

        public ZTPSBDContext(DbContextOptions options) : base(options)
        {
        
        }
        
        public DbSet<Adress> Adress  { get; set; }

        public DbSet<Customer>  Customer{ get; set; }

        public DbSet<Customer_Order> Customer_Order  { get; set; }

        public DbSet<Delivery_Service> Delivery_Service { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<Payment> Payment { get; set; }

        public DbSet<Product_Order> Product_Order { get; set; }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }

        public DbSet<Product_Category> ProductCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //TOCONSIDER: Add keys in one block before relation build
            
            //Address to customer and vice versa
            modelBuilder.Entity<Adress>().HasKey(A => new { A.id_adress });
            modelBuilder.Entity<Adress>().HasOne(C => C.customer).WithMany(Adress => Adress.Adress).HasForeignKey(A => A.Customer_Id_customer);

            // customer 1-1 user
            modelBuilder.Entity<Customer>().HasKey(Customer => Customer.id_customer);
            modelBuilder.Entity<User>().HasKey(u => u.id_user);
            modelBuilder.Entity<User>().HasOne(u => u.customer).WithOne(c => c.user).HasForeignKey<Customer>(c => c.User_id_user);
            //customer 1-* Customer_Order *-1 Order
            modelBuilder.Entity<Customer_Order>().HasKey(co => new { co.Customer_id_customer, co.Order_id_order });
            modelBuilder.Entity<Customer_Order>().HasOne(co => co.order).WithMany( o => o.Customer_Orders).HasForeignKey(co => co.Order_id_order);
            modelBuilder.Entity<Customer_Order>().HasOne(co => co.customer).WithMany(c => c.Customer_Orders).HasForeignKey(pc => pc.Customer_id_customer);
            // order 1-1 payment
            modelBuilder.Entity<Order>().HasKey(Order => Order.id_order);
            modelBuilder.Entity<Payment>().HasKey(p => p.id_payment);
            modelBuilder.Entity<Order>().HasOne(Order => Order.payment).WithOne(Payment => Payment.order).HasForeignKey<Payment>(payment => payment.Order_id_order);
            
            // order 1-1 delivery_service
          // modelBuilder.Entity<Order>().HasKey(Order => Order.id_order);
            modelBuilder.Entity<Delivery_Service>().HasKey(p => p.id_deliverman);
            modelBuilder.Entity<Order>().HasOne(Order => Order.deliveryService).WithOne(service => service.order).HasForeignKey<Delivery_Service>(var=> var.Order_id_order);

            //Order 1-* product_order *-1 product
            modelBuilder.Entity<Product_Order>().HasKey(po => new { po.Order_id_order, po.Product_id_product });
            modelBuilder.Entity<Product_Order>().HasOne(po => po.product).WithMany(p => p.Product_Orders).HasForeignKey(p => p.Product_id_product);
            modelBuilder.Entity<Product_Order>().HasOne(po => po.order).WithMany(p => p.Product_Orders).HasForeignKey(p => p.Order_id_order);

            modelBuilder.Entity<Product>().HasKey(prop => prop.id_product);
            modelBuilder.Entity<Product>().HasOne(p => p.product_Category).WithMany(pc => pc.products).HasForeignKey(prop => prop.id_category);

            modelBuilder.Entity<Product_Category>().HasKey(t => t.id_category);

        }


    }

    
}
