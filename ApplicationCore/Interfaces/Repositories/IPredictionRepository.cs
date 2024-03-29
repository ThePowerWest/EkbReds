﻿using ApplicationCore.Models;
using ApplicationCore.Models.Achievement;

namespace ApplicationCore.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория сущности Прогнозы
    /// </summary>
    public interface IPredictionRepository
    {
        /// <summary>
        /// Обновить очки за прошедшие матчи
        /// </summary>
        /// <param name="id">Идентификатор прогноза</param>
        /// <param name="point">Количество полученных очков</param>
        Task UpdatePointAsync(int id, byte point);

        /// <summary>
        /// Получить пользователей набравших большее количество очков за этот сезон
        /// </summary>
        /// <param name="userNumbers">Количество пользователей</param>
        /// <returns>Топ пользователей с суммой очков</returns>
        Task<IEnumerable<TopUser>> TopUsersAsync(byte userNumbers);
        
        /// <summary>
        /// Получить пользователя набравший больше всего очков за всё время
        /// </summary>
        /// <returns>Пользователь и сумма его очков</returns>
        Task<TopUser?> TopUserAsync();

        /// <summary>
        /// Сформировать таблицу очков всех пользователей которые участвуют в прогнозах за месяц
        /// </summary>
        /// <param name="seasonId">Идентификатор сезона</param>
        /// <param name="month">Число месяца</param>
        /// <returns>Таблица со всеми очками за месяц</returns>
        Task<IEnumerable<PointsPerMonth>> PointsPerMonthAsync(int seasonId, int month);

        /// <summary>
        /// Создать или обновить прогноз
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="homeTeamPredict">Прогноз голов на домашнюю команду</param>
        /// <param name="awayTeamPredict">Прогноз голов на гостевую команду</param>
        Task CreateOrUpdateAsync(string userId, byte homeTeamPredict, byte awayTeamPredict);

        /// <summary>
        /// Получить пользователя c самыми точными прогнозами за всё время
        /// </summary>
        /// <returns>Пользователь с количеством точных прогнозов</returns>
        Task<MostAccurateUser?> MostAccuratePredictionsUser();
    }
}