var back_loc = '/Home/ShowData?data=roles';

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
	$("#btnCreateKI").click(function ()
	{
		$("#mdVal1").html("KI");
		var selectedRows = [];
		var selectedRowsNr = [];
		$.each($("input[id='KIStaffing']:checked"), function () {
			if ($(this).val() == '1') lbl = 'Practice resources'
			if ($(this).val() == '2') lbl = 'Office resources'
			if ($(this).val() == '3') lbl = '125 FTE'
			if ($(this).val() == '4') lbl = 'Other (beach time, part time staffing, etc.)'
			if ($(this).val() == '5') lbl = 'No staffing is needed'
			selectedRows.push(lbl);
			selectedRowsNr.push($(this).val());
		});
		if (selectedRows.length == 0) {
			$("#mdVal2").html('<span style="color:red">please select at least one type of staffing</span>');
			$("#confirmInitiative").prop("disabled", true)
		}
		else {
			$("#mdVal2").html(selectedRows.join(", "))
			$("#confirmInitiative").prop("disabled", false)
		}
		if ($('#KIFTE')[0].checked) $("#mdVal3").html('Yes')
		else $("#mdVal3").html('No')

		$("#confirmInitiative").click(function ()
		{
			$('#initiative_type').val('1');
			$('#staffing').val(selectedRowsNr.join(","));
			location = '/Initiative/Edit?id=0';
		});
	});

	$("#StaffingType-KI :checkbox").change(function (e)
	{
		if ($(this).val() == '3') $('#KIFTE').prop('checked', $(this).is(":checked"))
	});
	$('#KIFTE').change(function (e)
	{
		chk = $(this).is(":checked");
		$('input:checkbox[id^="KIStaffing"]').each(function () {
			if ($(this).val() == '3') $(this).prop('checked', chk)
		});
	})
	$("#StaffingType-PK :checkbox").change(function (e) {
		if ($(this).val() == '3') $('#PKFTE').prop('checked', $(this).is(":checked"))
	});
	$('#PKFTE').change(function (e) {
		chk = $(this).is(":checked");
		$('input:checkbox[id^="PKStaffing"]').each(function () {
			if ($(this).val() == '3') $(this).prop('checked', chk)
		});
	})

}