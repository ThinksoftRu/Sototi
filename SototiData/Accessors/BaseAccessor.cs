#region Usings

using BLToolkit.DataAccess;

#endregion

namespace SototiData.Accessors
{
	using SototiCore.Data.Common;

	/// <summary>
	/// Базовый класс модулей доступа к данным.
	/// </summary>
	public class BaseAccessor : DataAccessor
	{
		/// <summary>
		/// Gets or sets the uid.
		/// </summary>
		public ICurrentUser User { get; set; }

		/// <summary>
		/// Возвращает текущую модель данных.
		/// </summary>
		internal SototiDbManager Model
		{
			get { return (SototiDbManager)DbManager; }
		}
	}
}