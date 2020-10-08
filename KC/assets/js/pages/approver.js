var back_loc = '/Home/ShowData?data=approvers';

$("#formLoading").hide();
$("#mainForm").show();

$('#hb_link').attr('href', back_loc);

$("#btnSubmit").click(function ()
{
	$("#mainForm").submit();
});

$("#btnCancel").click(function () {
	location = back_loc;
});

function startPage()
{
	//select2 - name
	$('#name').select2(
	{
		ajax: {
			url: '/Home/GetActiveDirectoryPersons?key=',
			dataType: 'json',
			processResults: function (data) { return unescapeData(data) }
		}
	});
	//add value
	if (js_aName != '')
	{
		var option = new Option(js_aName, js_aName, true, true);
		$("#name").append(option).trigger('change');
	}
	//refresh select2 control
	$("#name").trigger('change');


	//select2 - proxy approvers list
	$('#proxy_approvers').select2(
	{
		ajax: {
			url: '/Home/GetActiveDirectoryPersons?key=',
			dataType: 'json',
			processResults: function (data) { return unescapeData(data) }
		}
	});
	//add values
	$.each(js_paList.split(";"), function (i, e) {
		if (e != '') {
			var option = new Option(e, e, true, true);
			$("#proxy_approvers").append(option).trigger('change');
		}
	});
	//refresh select2 control
	$("#proxy_approvers").trigger('change');
}

