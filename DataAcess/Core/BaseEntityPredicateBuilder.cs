using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using  DataObjects;
using EntityModel;

namespace DataAccess
{
    public static  class BaseEntityPredicateBuilder
    {
        public  static ExpressionStarter<T> GetBasicPredicate<T>(DataObjects.DTOSearchByProprieta  dto) where  T:baseEntityTable
        {
            var predicate = PredicateBuilder.New<T>();
            predicate = predicate.And(x => !x.isDeleted);
            if (!string.IsNullOrEmpty(dto.PartialNomeProprieta) && string.IsNullOrEmpty(dto.PartialValoreProprieta))
                predicate = predicate.And(x => x.Properties.Any(y => y.PropertyName.Contains(dto.PartialNomeProprieta)));
            if (!string.IsNullOrEmpty(dto.PartialValoreProprieta) && string.IsNullOrEmpty(dto.PartialNomeProprieta))
                predicate =
                    predicate.And(x => x.Properties.Any(y => y.PropertyValue.Contains(dto.PartialValoreProprieta)));

            if (!string.IsNullOrEmpty(dto.PartialNomeProprieta) && !string.IsNullOrEmpty(dto.PartialValoreProprieta))
                predicate =
                    predicate.And(
                        x =>
                            x.Properties.Any(
                                y =>
                                    y.PropertyName.Contains(dto.PartialNomeProprieta) &&
                                    y.PropertyValue.Contains(dto.PartialValoreProprieta)));

            return predicate;
        }

    }
}
