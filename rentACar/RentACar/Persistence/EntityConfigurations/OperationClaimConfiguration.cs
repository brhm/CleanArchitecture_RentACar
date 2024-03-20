using Application;
using Core.Security.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.EntityConfigurations;

public class OperationClaimConfiguration : IEntityTypeConfiguration<OperationClaim>
{
    public void Configure(EntityTypeBuilder<OperationClaim> builder)
    {
        builder.ToTable("OperationClaims").HasKey(o=>o.Id);

        builder.Property(o => o.Id).HasColumnName("Id").IsRequired();
        builder.Property(o=>o.Name).HasColumnName("Name").IsRequired();
        builder.Property(o => o.CreatedDate).HasColumnName("CreatedDate").IsRequired();
        builder.Property(o => o.UpdatedDate).HasColumnName("UpdatedDate");
        builder.Property(o => o.DeletedDate).HasColumnName("DeletedDate");

        builder.HasQueryFilter(o => !o.DeletedDate.HasValue);

        builder.HasMany(oc => oc.UserOperationClaims);

        //builder.HasData(_seeds);
    }
    //private IEnumerable<OperationClaim> _seeds
    //{
    //    get
    //    {
    //        int id = 0;
    //        yield return new OperationClaim { Id = ++id,Name=GeneralOperationClaims.Admin };

    //        #region Feature Operation Claims

    //        IEnumerable<Type> featureOperationClaimsTypes = Assembly
    //            .GetAssembly(typeof(ApplicationServiceRegistration))!
    //            .GetTypes()
    //            .Where(type =>
    //                (type.Namespace?.Contains("Features") == true)
    //                && (type.Namespace?.Contains("Constants") == true)
    //                && (type.IsClass)
    //                && type.Name.EndsWith("OperationClaims"));

    //        foreach (var type in featureOperationClaimsTypes)
    //        {
    //            FieldInfo[] typeFields=type.GetFields(BindingFlags.Public | BindingFlags.Static);
    //            IEnumerable<string> typeFieldsValues=typeFields.Select(field=>field.GetValue(null)!.ToString()!);

    //            IEnumerable<OperationClaim> featureOperationClaimsToAdd = typeFieldsValues.Select(
    //                value=>new OperationClaim { Id=++id,Name=value}

    //                );

    //            foreach (OperationClaim featureOperationClaim in featureOperationClaimsToAdd)
    //                yield return featureOperationClaim;            }

    //        #endregion

    //    }
    //}
}
