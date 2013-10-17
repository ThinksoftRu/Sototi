namespace SototiData.DataProviders
{
	using System;

	using SimpleInjector;

	using SototiCore.DataProviders;

	using SototiCore.Data;
	

	/// <summary>
	/// ���������� ������� ����������� ������.
	/// </summary>
	public class DataProviderFactory : IDataProviderFactory
	{
		/// <summary>
		/// IoC ���������.
		/// </summary>
		private readonly Container kernel;

		/// <summary>
		/// Initializes a new instance of the <see cref="DataProviderFactory"/> class.
		/// </summary>
		/// <param name="kernel">IoC ���������.</param>
		public DataProviderFactory(IServiceProvider kernel)
		{
			this.kernel =(Container) kernel;
		}

		#region Implementation of IDataProviderFactory

		/// <summary>
		/// ������� ����� ��������� ���������� ������.
		/// </summary>
		/// <typeparam name="TD">��� ���������� ������.</typeparam>
		/// <typeparam name="TC">��� ���������.</typeparam>
		/// <param name="context">�������� ������.</param>
		/// <returns>��������� ��������� ������.</returns>
		public TD Create<TD, TC>(TC context) where TD : IDataProvider where TC : IDataContext
		{
			return (TD)(this.kernel).GetInstance(typeof(TD));
		}

		#endregion
	}
}