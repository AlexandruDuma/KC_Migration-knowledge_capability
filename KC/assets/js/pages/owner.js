var back_loc = '/Home/ShowData?data=owners';

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
	//select2 - owner contacts list
	$('#owner_contacts').select2(
		{
			ajax: {
				url: '/Home/GetActiveDirectoryPersons?key=',
				dataType: 'json',
				processResults: function (data) { return unescapeData(data) }
			}
		});
	//add values
	$.each(js_cList.split(";"), function (i, e) {
		if (e != '') {
			var option = new Option(e, e, true, true);
			$("#owner_contacts").append(option).trigger('change');
		}
	});
	//refresh select2 control
	$("#owner_contacts").trigger('change');

	//approvers
	$.ajax({
		type: 'GET',
		url: '/Home/GetJSONData?key=approvers.all',
		dataType: 'json',
		success: function (data) {
			$("#owner_approvers").select2({ data: unescapeData(data) });
		}
	});
	//add values
	$.each(js_aList.split(";"), function (i, e) {
		if (e != '')
		{
			x = e.split("#");
			var option = new Option(x[1], x[0], true, true);
			$("#owner_approvers").append(option).trigger('change');
		}
	});
	//refresh select2 control
	$("#owner_approvers").trigger('change');

	//departments
	$.ajax({
		type: 'GET',
		url: '/Home/GetJSONData?key=departments.all',
		dataType: 'json',
		success: function (data) {
			$("#owner_departments").select2({ data: data });
		}
	});
	//add values
	$.each(js_dList.split(";"), function (i, e) {
		if (e != '')
		{
			x = e.split("#");
			var option = new Option(x[1], x[0], true, true);
			$("#owner_departments").append(option).trigger('change');
		}
	});
	//refresh select2 control
	$("#owner_departments").trigger('change');
}

