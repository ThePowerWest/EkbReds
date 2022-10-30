using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace Infrastructure.Helpers
{
    /// <summary>
    /// Хелпер для базы данны 
    /// </summary>
    internal static class DbHelper
    {
        /// <summary>
        /// Отправить запрос в БД и сформировать модель для возвращения
        /// </summary>
        /// <typeparam name="T">Уникальная модель</typeparam>
        /// <param name="context">Контекст</param>
        /// <param name="query">Запрос</param>
        /// <param name="map">Модель формирующая таблицу</param>
        /// <returns>Уникальная модель</returns>
        public static async Task<List<T>> RawSqlQueryAsync<T>(this MainContext context, string query, Func<DbDataReader, T> map)
        {
            using (context)
            {
                using (DbCommand command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    await context.Database.OpenConnectionAsync();

                    using (DbDataReader result = await command.ExecuteReaderAsync())
                    {
                        List<T> entities = new List<T>();

                        while (result.Read())
                        {
                            entities.Add(map(result));
                        }

                        await context.Database.CloseConnectionAsync();

                        return entities;
                    }                   
                }
            }
        }

        /// <summary>
        /// Отправить запрос в БД и сформировать одну модель для возвращения
        /// </summary>
        /// <typeparam name="T">Уникальная модель</typeparam>
        /// <param name="context">Контекст</param>
        /// <param name="query">Запрос</param>
        /// <param name="map">Модель формирующая таблицу</param>
        /// <returns>Уникальная модель</returns>
        public static async Task<T?> RawSqlFirstOrDefaultAsync<T>(this MainContext context, string query, Func<DbDataReader, T> map)
        {
            using (context)
            {
                using (DbCommand command = context.Database.GetDbConnection().CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;

                    await context.Database.OpenConnectionAsync();

                    using (DbDataReader result = await command.ExecuteReaderAsync())
                    {
                        while (result.Read())
                        {
                            return map(result);
                        }

                        await context.Database.CloseConnectionAsync();
                    }
                }
            }
            return default;
        }
    }
}