using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace DatatablesQueryable.DataTables
{
    public interface IDataTablesQueryable<TModel, TResult>
    {
        DataTablesModel Create(DTParameterModel parameters, IQueryable<TModel> queryable, Expression<Func<TModel, TResult>> transform);
    }

    public class DataTablesQueryable<TModel, TResult> : IDataTablesQueryable<TModel, TResult>
    {
        public DataTablesModel Create(DTParameterModel parameters, IQueryable<TModel> queryable, Expression<Func<TModel, TResult>> transform)
        {
            var model = new DataTablesModel();

            var filteredData = Filter(parameters, queryable.Select(transform));

            var page = filteredData.Skip(parameters.Start);
            if (parameters.Length > -1)
            {
                page = page.Take(parameters.Length);
            }

            model.draw = parameters.Draw;
            model.recordsTotal = queryable.Count();
            model.recordsFiltered = filteredData.Count();
            model.data = ((IQueryable<object>)page).ToArray();

            return model;
        }

        public IQueryable<T> Filter<T>(DTParameterModel dtParameters, IQueryable<T> data)
        {
            var columns = typeof(T).GetProperties().ToArray();

            if (!string.IsNullOrEmpty(dtParameters.Search.Value))
            {
                var filterParts = new List<string>();
                foreach (var column in dtParameters.Columns.Where(x => !string.IsNullOrEmpty(x.Data)))
                {
                    if (column.Searchable)
                    {
                        var filter = string.Format("{0}.ToString().ToLower().Contains(\"{1}\")", 
                            columns.First(x => x.Name == column.Data).Name, 
                            dtParameters.Search.Value.ToLower().Replace("\"", "\"\""));

                        filterParts.Add(filter);
                    }
                }
                data = data.Where(string.Join(" or ", filterParts));
            }

            string sortString = dtParameters.Order.Count() > 0 ? 
                string.Join(", ", dtParameters.Order.Select(x => string.Format("{0} {1}", columns[x.Column].Name, x.Dir))) : 
                columns[0].Name;

            data = data.OrderBy(sortString);

            return data;
        }
    }
}
