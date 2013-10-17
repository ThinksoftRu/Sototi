namespace SototiData.DataProviders
{
	using System;

	using SimpleInjector;

	using SototiCore.DataProviders;

	using SototiCore.Data;
	

	/// <summary>
	/// Реализация фабрики поставщиков данных.
	/// </summary>
	public class DataProviderFactory : IDataProviderFactory
	{
		/// <summary>
		/// IoC контейнер.
		/// </summary>
		private readonly Container kernel;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataProviderFactory"/> class.
		/// </summary>
		/// <param name="kernel">IoC контейнер.</param>
		public DataProviderFactory(IServiceProvider kernel)
		{
			this.kernel =(Container) kernel;
		}

		#region Implementation of IDataProviderFactory

		/// <summary>
		/// Создает новый экземпляр поставщика данных.
		/// </summary>
		/// <typeparam name="TD">Тип поставщика данных.</typeparam>
		/// <typeparam name="TC">Тип контекста.</typeparam>
		/// <param name="context">Контекст данных.</param>
		/// <returns>Созданный поставщик данных.</returns>
		public TD Create<TD, TC>(TC context) where TD : IDataProvider where TC : IDataContext
		{
			return (TD)(this.kernel).GetInstance(typeof(TD));
		}

		#endregion
	}
}