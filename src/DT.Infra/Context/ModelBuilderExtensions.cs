using DT.Domain.Entities;
using DT.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace DT.Infra.Context
{
    public static class ModelBuilderExtensions
    {
        public static Guid ATE_3_DIAS = new Guid("46e72492-c8b3-452f-ae8e-a1b32e58063b");
        public static Guid SUPERIOR_3_DIAS = new Guid("0545e7e2-1b70-4027-87c8-de6c05c8783c");
        public static Guid SUPERIOR_5_DIAS = new Guid("1d5d8b3b-86e5-4d42-8b40-09eca5bb8c48");


        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Interest>().HasData
                (
                    new Interest(DayOfDelayStatus.ATE3DIAS, 0.02, 0.001),
                    new Interest(DayOfDelayStatus.DE4A5DIAS, 0.03, 0.002),
                    new Interest(DayOfDelayStatus.SUPERIORA5DIAS, 0.05, 0.003)
                );
        }
    }
}
