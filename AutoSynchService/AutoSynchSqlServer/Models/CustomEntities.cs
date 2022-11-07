
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using AutoSynchSqlServer.CustomModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSynchService.Models
{
    public partial class Entities
    {

        public virtual DbSet<Sequence> Sequence { get; set; }
        public virtual DbSet<Timestamp> Timestamp { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sequence>().HasNoKey();
            modelBuilder.Entity<Timestamp>().HasNoKey();



            modelBuilder.Entity<Sequence>(entity =>
            {
                entity.Property(e => e.nextval)
                       .HasColumnType("NUMBER")
                       .HasColumnName("nextval");
            });

            modelBuilder.Entity<Timestamp>(entity =>
            {
                entity.Property(e => e.current_timestamp)
                       .HasColumnName("current_timestamp");
            });


        }


        public decimal GetSequence(string Column,string Table,decimal Branch)
        {
            var SeqValue = Sequence.FromSqlRaw($"select ISNULL(max({Column}), 0) + 1 as nextval from {Table} where BranchId = '{Branch}'").AsEnumerable().FirstOrDefault().nextval;
            return SeqValue;
        }
        public int GetSequence(string Column, string Table)
        {
            var SeqValue = Sequence.FromSqlRaw($"select ISNULL(max({Column}), 0) + 1 as nextval from {Table}").AsEnumerable().FirstOrDefault().nextval;
            return SeqValue;
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {

        //        IConfigurationRoot configuration = new ConfigurationBuilder()
        //      .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //      .AddJsonFile("appsettings.json")
        //      .Build();
        //        optionsBuilder.UseOracle(configuration.GetConnectionString("DefaultConnection"));
        //        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        //        ///optionsBuilder.UseOracle("Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.10.130.30)(PORT=1522))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=ora12c)));User ID=POA;Password=POA;Persist Security Info=True");
        //    }
        //}
    }


}