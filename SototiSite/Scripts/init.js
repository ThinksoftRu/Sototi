function LayoutInit() {

	toastr.options = {
		"debug": false,
		"positionClass": "toast-bottom-right",
		"fadeIn": 100,
		"fadeOut": 1000,
		"timeOut": 4000,
		"extendedTimeOut": 1000
	};
	
	$(document).ajaxStart(function() { // Глобальное ajax событие начала
		$("#ajax-loader").css({ opacity: 1 });
	});

	$(document).ajaxError(function() { // Глобальное ajax событие ошибки
		$("#ajax-loader").css({ opacity: 0 });
		toastr.error("Ошибка запроса", "Ошибка");
	});

	$(document).ajaxComplete(function(event, xmlHttpRequest) { // Глобальное ajax событие окончания 
		$('#ajax-loader').css({ opacity: 0 });
		try { // Сервер может присылать иногда JSON ответ с результатом выполненной операции, пробуем вытащить ответ от сервера
			var resp = jQuery.parseJSON(xmlHttpRequest.responseText);
			if (resp.hasOwnProperty("ResponseSuccess")) {
				resp.ResponseSuccess ? toastr.success(resp.ResponseMessage, resp.ResponseTitle) : toastr.error(resp.ResponseMessage, resp.ResponseTitle);
			}
		} catch(e) {
		}
	});
	

	$(".lang").click(function () {
		setCookie("lang", $(this).attr("alt"), 7, '/', false);
		window.location.reload(true);
	});
}

function setCookie(cName, value, exdays) {
	var exdate = new Date();
	exdate.setDate(exdate.getDate() + exdays);
	var cValue = escape(value) + ((exdays == null) ? "" : "; expires=" + exdate.toUTCString());
	document.cookie = cName + "=" + cValue + "; path=/";
}