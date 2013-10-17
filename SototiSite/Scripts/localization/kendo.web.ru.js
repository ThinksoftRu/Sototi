/// <reference path="jquery-1.10.1.intellisense.js" />
/// <reference path="kendo.web.js" />

(function () {
	"use strict";

	kendo.ui.Pager.prototype.options.messages = $.extend(kendo.ui.Pager.prototype.options.messages, {
		display: "{0} - {1} из {2} записей",
		empty: "Нет данных для отображения",
		page: "Страница",
		of: "из {0}",
		itemsPerPage: "&larr;&nbsp;на&nbsp;странице",
		first: "Перейти на первую страницу",
		previous: "Перейти на предыдущую страницу",
		next: "Перейти на следующую страницу",
		last: "Перейти на последнюю страницу",
		refresh: "Обновить"
	});

	kendo.ui.FilterMenu.prototype.options.messages = $.extend(kendo.ui.FilterMenu.prototype.options.messages, {
		and: "И",
		clear: "Очистить",
		filter: "Фильтрация",
		info: "Показать элементы со значениями, которые:",
		isFalse: "ложно",
		isTrue: "истинно",
		or: "Или",
		selectValue: "- Выберите значение -"
	});
	

	kendo.ui.FilterMenu.prototype.options.operators.string = $.extend(kendo.ui.FilterMenu.prototype.options.operators.string, {
		eq: "Равно",
		neq: "Не равно",
		startswith: "Начинается с",
		contains: "Содержит",
		doesnotcontain: "Не содержит",
		endswith: "Заканчивается на"
	});
	

	kendo.ui.Groupable.prototype.options.messages = $.extend(kendo.ui.Groupable.prototype.options.messages, {
		empty: "Для группирования по столбцу перетащите сюда его заголовок"
	});

	kendo.ui.ColumnMenu.prototype.options.messages = $.extend(kendo.ui.ColumnMenu.prototype.options.messages, {
		sortAscending: "Сортировка по возрастанию",
		sortDescending: "Сортировка по убыванию",
		filter: "Фильтр",
		columns: "Столбцы"
	});

	kendo.ui.ImageBrowser.prototype.options.messages = $.extend(kendo.ui.ImageBrowser.prototype.options.messages, {
		uploadFile: "Загрузить",
		orderBy: "Сортировать по",
		orderByName: "имени",
		orderBySize: "размеру",
		directoryNotFound: "Путь не найден.",
		emptyFolder: "Директория пуста",
		deleteFile: 'Действительно удалить файл "{0}"?',
		invalidFileType: "Файл \"{0}\" некорректен. Поддерживаемые типы файлов: {1}.",
		overwriteFile: "Файл с именем \"{0}\" уже существует в данной директории. Перезаписать существующий файл?",
		dropFilesHere: "перетащите файлы для загрузки сюда"
	});

	kendo.ui.Upload.prototype.options.localization = $.extend(kendo.ui.Upload.prototype.options.localization, {
		"select": "Выбрать...",
		"cancel": "Отмена",
		"retry": "Повторить",
		"remove": "Удалить",
		"uploadSelectedFiles": "Загрузить файлы",
		"dropFilesHere": "перетащите файлы для загрузки сюда",
		"statusUploading": "загрузка",
		"statusUploaded": "загружено",
		"statusFailed": "не удалось"
	});

	kendo.ui.Validator.prototype.options.messages = $.extend(kendo.ui.Validator.prototype.options.messages, {
		required: "{0} - обязательно для заполнения",
		pattern: "{0} - значение некорректно",
		min: "{0} - значение должно быть не менее {1}",
		max: "{0} - значение должно быть не более {1}",
		step: "{0} - значение некорректно",
		email: "{0} - некорректный email",
		url: "{0} - некорректный веб-адрес",
		date: "{0} - некорректная дата"
	});
})();