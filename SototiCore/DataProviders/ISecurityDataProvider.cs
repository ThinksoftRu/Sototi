namespace SototiCore.DataProviders
{
	using System;
	using System.Threading.Tasks;

	using SototiCore.Data.Common;

	using SototiCore.DataProviders;

	/// <summary>
	/// Интерфейс поставщика данных дял работы с пакетом безопасности.
	/// </summary>
	public interface ISecurityDataProvider : IDataProvider
	{
		/// <summary>
		/// Возвращает серверное время.
		/// </summary>
		/// <returns>Дата и время на сервере базы данных.</returns>
		Task<DateTime> ServerDateTimeAsync();

		/// <summary>
		/// The set user state.
		/// </summary>
		/// <param name="login">
		/// Логин пользователя.
		/// </param>
		/// <param name="password">
		/// Пароль пользователя.
		/// </param>
		/// <returns>
		/// Состояние пользователя.
		/// </returns>
		Task<UserState> GetUserState(string login, string password);

		/// <summary>
		/// The get user id by login.
		/// </summary>
		/// <param name="login">
		/// Логин пользователя.
		/// </param>
		/// <returns>
		/// Идентификатор пользователя.
		/// </returns>
		Task<int> GetUserIdByLogin(string login);


	    void RegisterSid(Guid sid);
	}
}